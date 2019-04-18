/* Authors: Blake, Clayton, Dylan
 * File Name: frmGameGUI.cs
 * 
 * Description: Reflects changes in the gamestate to the user in a visual manner
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using CardLibrary;
using System.Diagnostics;
using System.IO;

namespace DurakXtreme
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchKey"></param>
    /// <returns></returns>
    /// 

    /// <summary>
    /// Class frmGameGUI - Serves as the GUI for the DurakGame.
    /// 
    /// </summary>
    public partial class frmGameGUI : Form
    {
        // Game data is not altered from this class.
        // This class displays game data received from the DurakGame object,
        // then captures user input, which it sends back to the DurakGame,
        // triggering new events.

        // Constants
        const int AI_ATTACK_DELAY = 300;
        const int AI_YIELD_DELAY = 1000;
        bool aiCardsVisible = false;
        // Path to resources folder
        public const string RESOURCES_PATH = "../../Resources/";

        // DurakGame object
        private DurakGame durakGame;

        //Get players from game instance
        public IPlayer HumanPlayer;
        public IPlayer AiPlayer;

        public string HumanName;
        public string AiName;

        public GameStatistics gameStats = new GameStatistics();

        // GUI configuration variables
        private Color defenseColor = Color.FromArgb(70, 50, 50);
        private Color attackColor = Color.FromArgb(50, 70, 50);

        private frmMainMenu menuForm = new frmMainMenu();

        // frmGameGUI
        public frmGameGUI(frmMainMenu mainMenu)
        {
            menuForm = mainMenu;
            InitializeComponent();
            this.Show();

            // Read config file
            if (!File.Exists("./DurakConfiguration"))
            {
              File.Create("./DurakConfiguration").Dispose();
            }
            TextReader tr = new StreamReader("./DurakConfiguration");
            HumanName = tr.ReadLine();
            AiName = tr.ReadLine();
            String isCardVisible = tr.ReadLine();
            if (!String.IsNullOrEmpty(isCardVisible))
            {
                aiCardsVisible = bool.Parse(isCardVisible);
            }
            
            tr.Close();

            // The DurakGame constructor creates a game instance
            // with one human and one AI player, and deals hands out to
            // both of them.
            // By passing this form as a parameter, it can receive data from
            // the game and have GUI events triggered by it
            durakGame = new DurakGame(this);
            // Capture players from DurakGame
            HumanPlayer = durakGame.Players[0];
            AiPlayer = durakGame.Players[1];
            // Set Names
            if (!String.IsNullOrEmpty(HumanName)) HumanPlayer.Name = HumanName;
            if (!String.IsNullOrEmpty(AiName)) AiPlayer.Name = AiName;
            lblPlayerName.Text = HumanPlayer.Name;
            lblAIName.Text = AiPlayer.Name;
            //Throw In events
            durakGame.OnPuttingDown += PuttingDown;
            durakGame.PuttingDownComplete += PuttingDownComplete;

            gameStats.InitializeStatistics();

            this.InitiateGame();
        }

        /// <summary>
        /// Initializes game, draws cards
        /// </summary>
        public void InitiateGame()
        {
            pbTrump.Image = Image.FromFile(CardBox.GetImagePathFromCard(durakGame.TrumpCard));
            //Load card image
            pbDeck.Load(CardBox.GetImagePathFromCard());

            Refresh();
            //Initialize card deck count amount
            lblDeckCount.Text = durakGame.deck.Count.ToString();

            //Play super cool sound
            //source : https://freesound.org/people/mickmon/sounds/176862/
            SoundPlayer dealSound = new System.Media.SoundPlayer(RESOURCES_PATH + "deal.wav");
            dealSound.Play();

            //Deal cards
            gameStats.cardsDrawn = gameStats.cardsDrawn + DurakGame.STARTING_CARD_COUNT;
            for (int i = 0; i < DurakGame.STARTING_CARD_COUNT; i++)
            {
                DealCardToPanel(this.pnlPlayerBottom, durakGame.Players[0].Cards[i]);
                DealCardToPanel(this.pnlPlayerTop, durakGame.Players[1].Cards[i]);
                Wait(50);
            }
            this.txtMessages.Update();
            durakGame.TurnAttack();
        }

        /// <summary>
        /// When a player starts a 'put down' event
        /// set respective players PuttingDown attribute to true
        /// </summary>
        /// <param name="sender">Whichever player triggered the event</param>
        /// <param name="e"></param>
        public void PuttingDown(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Player))
            {
                HumanPlayer.PuttingDown = true;
            }
            else if (sender.GetType() == typeof(ComputerPlayer))
            {
                AiPlayer.PuttingDown = true;
            }
        }

        /// <summary>
        /// On completion of puttingDown state, take the river, turn off PuttingDown value in senders player class
        /// </summary>
        /// <param name="sender">Senders player object</param>
        /// <param name="e"></param>
        public void PuttingDownComplete(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ComputerPlayer))
            {
                AiPlayer.PuttingDown = false;
                durakGame.TakeRiver(HumanPlayer);
            } else if (sender.GetType() == typeof(Player))
            {
                HumanPlayer.PuttingDown = false;
                durakGame.TakeRiver(AiPlayer);
            }
        }

        /// <summary>
        /// Allow for user to make their response, and update related form controls
        /// </summary>
        public void GetHumanResponse()
        {
            durakGame.CheckForWinner();
            UpdateUIStyles();
            if (durakGame.Winner != null) btnTakePass.Text = "End";
            EnableCardClick();
            EvaluateHand();
        }

        /// <summary>
        /// Diable card controls, reset the players panelColor
        /// </summary>
        public void EndHumanResponse()
        {
            // Remove all events from cards
            DisableCardClick();
            foreach (Control card in pnlPlayerBottom.Controls)
            {
                card.MouseEnter -= CardPop;
                card.MouseLeave -= CardPop;
            }
            ResetColors(pnlPlayerBottom);
        }
        /// <summary>
        /// Creates a new CardBox and deals it to the specified panel
        /// </summary>
        /// <param name="panel"></param>
        public void DealCardToPanel(Panel panel, PlayingCard card)
        {
            CardBox pbCard = new CardBox(card);
            pbCard.FaceDown();
            if (panel == pnlPlayerBottom || aiCardsVisible) pbCard.FaceUp();
            panel.Controls.Add(pbCard);
            AlignCards(panel);
        }
        void ResetColors(Panel panel)
        {
            foreach (CardBox cb in panel.Controls)
            {
                cb.SetCardImage(cb.Card);
            }
        }
        /// <summary>
        /// Aligns the cards in the panel depending on which game panel is passed to it
        /// </summary>
        /// <param name="panel"></param>
        void AlignCards(Panel panel)
        {
            //panel.Update();
            int panelWidth = panel.Width;
            int panelHeight = panel.Height;
            int cardSpacing = CardBox.CARD_SIZE.Width;
            int padding = 25;
            if (panel == pnlPlayerBottom || panel == pnlPlayerTop)
            {
                bool isBottomPanel = panel == pnlPlayerBottom;
                int expectedWidth = panel.Controls.Count * cardSpacing;
                if (expectedWidth > panelWidth) //Cards will not fit in panel using default spacing
                {
                    // Total spacing adjustment equal to difference between panel width and required width (including padding)
                    cardSpacing -= (expectedWidth - panelWidth + (padding * 2)) / panel.Controls.Count;

                    // Calculate expected width using card count and spacing
                    // Remember, last card is on top therefor full-width
                    expectedWidth = (panel.Controls.Count - 1) * cardSpacing + CardBox.CARD_SIZE.Width;
                }
                // Apply new locations to cards
                for (int i = 0; i < panel.Controls.Count; i++)
                {
                    // Initial x-position is half the remaining panel width
                    int leftMargin = (panel.Width - expectedWidth) / 2;
                    panel.Controls[i].Location = new Point(leftMargin + (i * cardSpacing), isBottomPanel ? panelHeight - CardBox.CARD_SIZE.Height : 0);
                }
                panel.Update();
            }
            else if (panel == pnlPlayArea)
            {
                cardSpacing = CardBox.CARD_SIZE.Width / 2;
                int duoSpacing = 50;
                int duoCount = ((panel.Controls.Count - 1) / 2 > 0) ? (panel.Controls.Count - 1) / 2 : 0;

                int expectedWidth = (panel.Controls.Count - 1) * cardSpacing + (duoCount * duoSpacing) + CardBox.CARD_SIZE.Width;
                int leftMargin = (panelWidth - expectedWidth) / 2;
                int alignBottom = panel.Height - CardBox.CARD_SIZE.Height;
                for (int i = 0; i < panel.Controls.Count; i++)
                {
                    if (i > 0 && i % 2 == 0)
                    {
                        leftMargin += duoSpacing;
                    }
                    int aiSide = Convert.ToInt32(durakGame.GetAttacker().GetType() == typeof(Player));
                    if (i % 2 == aiSide)
                    {
                        panel.Controls[i].Location = new Point(leftMargin + (i * cardSpacing), 0);
                    }
                    else
                    {
                        panel.Controls[i].Location = new Point(leftMargin + (i * cardSpacing), alignBottom);

                    }
                    panel.Controls[i].Update();
                }
            }
        }
        /// <summary>
        /// Hover effect enlarging a card by a specified factor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CardPop(object sender, EventArgs e)
        {
            float popRatio = 1.2f;
            int newWidth = (int)(CardBox.CARD_SIZE.Width * popRatio);
            int newHeight = (int)(CardBox.CARD_SIZE.Height * popRatio);
            PictureBox card = sender as PictureBox;
            if (card.Size == CardBox.CARD_SIZE)
            {
                card.Size = new Size(newWidth, newHeight);
                card.Location = new Point(card.Location.X - (newWidth - CardBox.CARD_SIZE.Width) / 2, card.Location.Y - (newHeight - CardBox.CARD_SIZE.Height));
            }
            else
            {
                card.Size = new Size(CardBox.CARD_SIZE.Width, CardBox.CARD_SIZE.Height);
                card.Location = new Point(card.Location.X + (newWidth - CardBox.CARD_SIZE.Width) / 2, card.Location.Y + (newHeight - CardBox.CARD_SIZE.Height));
            }
        }
        /// <summary>
        /// Checks the players hand for valid plays, applying
        /// events and styles to cards based on whether or not
        /// they are playable
        /// </summary>
        void EvaluateHand()
        {
            if (durakGame.River.Count > 0)
            {
                for (int i = 0; i < durakGame.Players[0].Cards.Count; i++)
                {
                    CardBox pb = pnlPlayerBottom.Controls[i] as CardBox;
                    List<PlayingCard> cards = HumanPlayer.Cards;
                    bool isValid = (HumanPlayer.TurnStatus == TurnStatus.Attacking ? durakGame.IsValidAttack(cards[i]) : durakGame.IsValidDefence(cards[i]));
                    if (!isValid)
                    {
                        pb.SetToMonochrome();
                    }
                    else
                    {
                        Console.WriteLine("Adding hover event to " + cards[i].ToString());
                        pb.MouseEnter += CardPop;
                        pb.MouseLeave += CardPop;
                    }
                }
            }
            else
            {
                Console.WriteLine("Fresh attack. Adding all hover event");
                foreach (Control control in pnlPlayerBottom.Controls)
                {
                    control.MouseEnter += CardPop;
                    control.MouseLeave += CardPop;
                }
            }
        }
        /// <summary>
        /// CardClick(object sender, EventArgs e)
        ///         Occurs every time a user clicks a card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CardClick(object sender, EventArgs e)
        {

            CardBox cb = (CardBox)sender;
            if (HumanPlayer.TurnStatus == TurnStatus.Attacking && durakGame.IsValidAttack(cb.Card)
                || HumanPlayer.TurnStatus == TurnStatus.Defending && durakGame.IsValidDefence(cb.Card))
            {
                durakGame.RecieveCard(cb.Card);
            }
            else
            {
                durakGame.Print("\r" + cb.Card.ToString() + " is not a valid play!");
            }
        }
        /// <summary>
        /// Give river to a players hand
        /// </summary>
        /// <param name="player">Player to give river to</param>
        public void GiveRiver(IPlayer player)
        {
            Panel toPanel = null;
            bool putFaceDown = false;
            lblStatus.Text = player.Name + " takes the river!";
            Wait(200);
            if (player.GetType() == typeof(Player))
            {
                toPanel = pnlPlayerBottom;
            }
            else if (player.GetType() == typeof(ComputerPlayer))
            {
                toPanel = pnlPlayerTop;
                putFaceDown = true;
            }

            for (int slide = 0; slide < 20; slide++)
            {
                for (int i = 0; i < pnlPlayArea.Controls.Count; i++)
                {
                    pnlPlayArea.Controls[i].Top += (toPanel == pnlPlayerBottom ? slide : -slide);
                }
                Wait(3);
            }

            for (int i = pnlPlayArea.Controls.Count - 1; i >= 0; i--)
            {
                CardBox cb = (CardBox)pnlPlayArea.Controls[i];
                if (!putFaceDown || aiCardsVisible) cb.FaceUp();
                else cb.FaceDown();
                toPanel.Controls.Add(cb);
                AlignCards(toPanel);
                Wait(100);
            }

            if (player.TurnStatus == TurnStatus.Attacking) durakGame.NextAttacker();
        }
        /// <summary>
        /// Discard the river
        /// </summary>
        public void DiscardCards()
        {
            lblStatus.Text = durakGame.GetAttacker().Name + " gives up attacking!";
            Wait(200);
            if (pbDiscard.Image == null) pbDiscard.Load(CardBox.GetImagePathFromCard());
            for (int slide = 0; slide < 80; slide += 2)
            {
                for (int i = 0; i < pnlPlayArea.Controls.Count; i++)
                {
                    CardBox cb = (CardBox)pnlPlayArea.Controls[i];
                    cb.Left += slide;
                    cb.Top -= cb.Location.Y / 10;
                }
                Wait(3);
            }
            for (int i = pnlPlayArea.Controls.Count - 1; i >= 0; i--)
            {
                CardBox cb = (CardBox)pnlPlayArea.Controls[i];
                pnlPlayArea.Controls.Remove(cb);
            }
        }
        /// <summary>
        /// Play a players card
        /// </summary>
        /// <param name="cardIndex">Index of card to play</param>
        /// <param name="playerIndex">Index of player</param>
        public void PlayCardAt(int cardIndex, int playerIndex)
        {
            Panel panel = (playerIndex == 0 ? pnlPlayerBottom : pnlPlayerTop);
            CardBox cb = panel.Controls[cardIndex] as CardBox;
            if (playerIndex == 0)
            {
                EndHumanResponse();
            }
            else
            {
                Wait(AI_ATTACK_DELAY);
                cb.FaceUp();
            }
            cb.Size = CardBox.CARD_SIZE;
            pnlPlayArea.Controls.Add(cb);
            AlignCards(panel);
            AlignCards(pnlPlayArea);
        }
        //Utility Methods
        /// <summary>
        /// Get image to populate card
        /// </summary>
        /// <param name="card">Card to get image of</param>
        /// <param name="extension">Extension of images</param>
        /// <returns></returns>
        string GetImagePathFromCard(PlayingCard card = null, string extension = ".png")
        {
            string path = RESOURCES_PATH;
            if (card == null)
            {
                path += "back.png";
            }
            else
            {
                path += card.ToString().ToLower().Replace(" ", "_") + extension;
            }
            return path;
        }
        /// <summary>
        /// Pause for a time in ms
        /// </summary>
        /// <param name="ms">Time to pause</param>
        public void Wait(int ms)
        {
            Update();
            Task.Delay(ms).Wait();
        }

        /// <summary>
        /// When the contextual pass/take/finish button is pressed
        /// do something depending on what state the game is in.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTake_Click(object sender, EventArgs e)
        {
            EndHumanResponse();
            if (HumanPlayer.TurnStatus == TurnStatus.Defending)
            {
                durakGame.PutDown();
            } else
            {
                if (HumanPlayer.PuttingDown == true)
                {
                    gameStats.attacksWon++;
                    HumanPlayer.PuttingDown = false;
                    durakGame.TakeRiver(AiPlayer);
                } else
                {
                    durakGame.TakeRiver(HumanPlayer);
                }
                
            }
        }
        /// <summary>
        /// Prints a message to the text box
        /// </summary>
        /// <param name="message">The PlayingCard selected from the GUI.</param>
        public void PrintMessage(string message)
        {
            this.txtMessages.AppendText(message + "\r\n"); //Print game messages
        }
        /// <summary>
        /// Change the style of the UI based on whether the player is attacking or defending
        /// </summary>
        public void UpdateUIStyles()
        {
            if (HumanPlayer.TurnStatus == TurnStatus.Defending)
            {
                btnTakePass.Text = "Take";
                lblStatus.Text = "You are defending!";
                pnlPlayerBottom.BackColor = defenseColor;
                pnlPlayerTop.BackColor = attackColor;
            }
            if (HumanPlayer.TurnStatus == TurnStatus.Attacking && HumanPlayer.PuttingDown == false)
            {
                btnTakePass.Text = "Pass";
                lblStatus.Text = "You are attacking!";
                pnlPlayerBottom.BackColor = attackColor;
                pnlPlayerTop.BackColor = defenseColor;
            }
            else if (HumanPlayer.TurnStatus == TurnStatus.Attacking && HumanPlayer.PuttingDown == true)
            {
                btnTakePass.Text = "Finish";
                lblStatus.Text = "You are putting down extra cards!";
                pnlPlayerBottom.BackColor = attackColor;
                pnlPlayerTop.BackColor = defenseColor;
            }
        }

        /// <summary>
        /// Enable card clickability
        /// </summary>
        void EnableCardClick()
        {
            foreach (Control control in pnlPlayerBottom.Controls)
            {
                control.MouseClick += CardClick;
            }
        }
        /// <summary>
        /// Disable card clickability
        /// </summary>
        void DisableCardClick()
        {
            foreach (Control control in pnlPlayerBottom.Controls)
            {
                control.MouseClick -= CardClick;
            }
        }
        /// <summary>
        /// Make sure UI and memory are in sync
        /// </summary>
        public bool CheckSync()
        {
            bool returnValue = true;
            for (int i = 0; i < pnlPlayerBottom.Controls.Count; i++)
            {
                Control c = (Control)pnlPlayerBottom.Controls[i];
                if (c is CardBox)
                {
                    if (((CardBox)c).Card != durakGame.Players[0].Cards[i]) returnValue = false;
                }
            }
            for (int i = 0; i < pnlPlayerTop.Controls.Count; i++)
            {
                Control c = (Control)pnlPlayerTop.Controls[i];
                if (c is CardBox)
                {
                    if (((CardBox)c).Card != durakGame.Players[1].Cards[i]) returnValue = false;
                }
            }
            Console.WriteLine("GUI Sync: " + returnValue);
            return returnValue;
        }
        /// <summary>
        /// End game
        /// </summary>
        /// <param name="winner"></param>
        /// <param name="loser"></param>
        public void End(IPlayer winner, IPlayer loser)
        {
            Wait(1000);
            foreach (Control c in this.Controls)
            {
                c.Hide();
            }
            Label endPrompt = new Label();
            endPrompt.Text = winner.Name + " won!\n" + loser.Name + " is the Durak!";
            endPrompt.AutoSize = false;
            endPrompt.Width = this.Width;
            endPrompt.Height = this.Height;
            endPrompt.TextAlign = ContentAlignment.MiddleCenter;
            endPrompt.Left = 0;
            endPrompt.Font = new Font("Arial", 30f);
            this.Controls.Add(endPrompt);
            this.Update();
        }

        /// <summary>
        /// On form closing, serialize(save) statistic data.
        /// Show the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGameGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameStats.SerializeFile();
            menuForm.Show();
            
        }

    }
}
