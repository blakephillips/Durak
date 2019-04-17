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
        const bool AI_CARDS_VISIBLE = true;
        const int AI_ATTACK_DELAY = 300;
        const int AI_YIELD_DELAY = 1000;
        // Path to resources folder
        public const string RESOURCES_PATH = "../../Resources/";

        // DurakGame object
        private DurakGame durakGame;

        //Get players from game instance
        public IPlayer HumanPlayer;
        public IPlayer ComputerPlayer;

        // GUI configuration variables
        private Color defenseColor = Color.FromArgb(70, 50, 50);
        private Color attackColor = Color.FromArgb(50, 70, 50);

        // frmGameGUI
        public frmGameGUI()
        {
            InitializeComponent();
            this.Show();

            // The DurakGame constructor creates a game instance
            // with one human and one AI player, and deals hands out to
            // both of them.

            // By passing this form as a parameter, it can receive data from
            // the game and have GUI events triggered by it
            durakGame = new DurakGame(this);

            // Capture players from DurakGame
            HumanPlayer = durakGame.Players[0];
            ComputerPlayer = durakGame.Players[1];

            this.InitiateGame();
        }

        public void InitiateGame()
        {
            pbTrump.Image = Image.FromFile(CardBox.GetImagePathFromCard(durakGame.TrumpCard));
            //
            pbDeck.Load(CardBox.GetImagePathFromCard());
            Refresh();
            lblDeckCount.Text = durakGame.deck.Count.ToString();

            SoundPlayer dealSound = new System.Media.SoundPlayer(RESOURCES_PATH + "deal.wav");
            dealSound.Play();
            for (int i = 0; i < DurakGame.STARTING_CARD_COUNT; i++)
            {
                DealCardToPanel(this.pnlPlayerBottom, durakGame.Players[0].Cards[i]);
                DealCardToPanel(this.pnlPlayerTop, durakGame.Players[1].Cards[i]);
                Wait(50);
            }
            this.txtMessages.Update();
            durakGame.TurnAttack();
        }



        public void GetHumanResponse()
        {
            durakGame.CheckForWinner();
            if (HumanPlayer.TurnStatus == TurnStatus.Defending)
            {
                btnTakePass.Text = "Take";
                lblStatus.Text = "You are defending!";
                pnlPlayerBottom.BackColor = defenseColor;
                pnlPlayerTop.BackColor = attackColor;
            }
            if (HumanPlayer.TurnStatus == TurnStatus.Attacking)
            {
                btnTakePass.Text = "Pass";
                lblStatus.Text = "You are attacking!";
                pnlPlayerBottom.BackColor = attackColor;
                pnlPlayerTop.BackColor = defenseColor;
            }
            if (durakGame.Winner != null) btnTakePass.Text = "End";
            EnableCardClick();
            EvaluateHand();
        }
        public void EndHumanResponse()
        {
            DisableCardClick();
            foreach (Control card in pnlPlayerBottom.Controls)
            {
                card.MouseEnter -= CardPop;
                card.MouseLeave -= CardPop;
            }
            ResetColors(pnlPlayerBottom);
        }
        //Dealer Methods
        public void DealCardToPanel(Panel panel, PlayingCard card)
        {
            CardBox pbCard = new CardBox(card);
            if (panel == pnlPlayerTop && !AI_CARDS_VISIBLE) pbCard.FaceDown();
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
        //GUI Methods
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
        //Event Methods
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

        public void TakeRiver(IPlayer player)
        {
            Panel toPanel = null;
            bool faceDown = false;
            lblStatus.Text = player.Name + " takes the river!";
            Wait(200);
            if (player.GetType() == typeof(Player))
            {
                toPanel = pnlPlayerBottom;
            }
            else if (player.GetType() == typeof(ComputerPlayer))
            {
                toPanel = pnlPlayerTop;
                faceDown = true;
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
                if (!faceDown || AI_CARDS_VISIBLE) cb.FaceUp();
                else cb.FaceDown();
                toPanel.Controls.Add(cb);
                AlignCards(toPanel);
                Wait(100);
            }

            if (player.TurnStatus == TurnStatus.Attacking) durakGame.NextAttacker();
        }
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
        public void Wait(int ms)
        {
            Update();
            Task.Delay(ms).Wait();
        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            EndHumanResponse();
            durakGame.TakeRiver(HumanPlayer);
        }

        public void UpdateMessages(string message)
        {
            this.txtMessages.AppendText(message + "\r\n"); //Print game messages
        }

        void EnableCardClick()
        {
            foreach (Control control in pnlPlayerBottom.Controls)
            {
                control.MouseClick += CardClick;
            }
        }
        void DisableCardClick()
        {
            foreach (Control control in pnlPlayerBottom.Controls)
            {
                control.MouseClick -= CardClick;
            }
        }

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
        public void End(IPlayer winner, IPlayer loser)
        {
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
    }
}
