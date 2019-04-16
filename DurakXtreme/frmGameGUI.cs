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
    public partial class frmGameGUI : Form
    {
        const bool AI_CARDS_VISIBLE = false;
        const int AI_ATTACK_DELAY = 300;
        const int AI_YIELD_DELAY = 1000;
        //Path to resources folder
        public const string resourcesPath = "../../Resources/";

        //Create new DurakGame
        public DurakGame durak;
        //Get players from game instance
        public IPlayer HumanPlayer;
        public IPlayer ComputerPlayer;

        //GUI config

        Color defenseColor = Color.FromArgb(70, 50, 50);
        Color attackColor = Color.FromArgb(50, 70, 50);

        //Base Constructor
        public frmGameGUI()
        {
            InitializeComponent();

            durak = new DurakGame(this);
            HumanPlayer = durak.Players[0];
            ComputerPlayer = durak.Players[1];

            this.Show();
            this.InitiateGame();
        }
        
        public void InitiateGame()
        {
            pbTrump.Image = Image.FromFile(CardBox.GetImagePathFromCard(durak.TrumpCard));
            //
            pbDeck.Load(CardBox.GetImagePathFromCard());
            Refresh();
            lblDeckCount.Text = durak.deck.Count.ToString();

            SoundPlayer dealSound = new System.Media.SoundPlayer(resourcesPath + "deal.wav");
            dealSound.Play();
            for (int i = 0; i < DurakGame.STARTING_CARD_COUNT; i++)
            {
                DealCardToPanel(this.pnlPlayerBottom, durak.Players[0].Cards[i]);
                DealCardToPanel(this.pnlPlayerTop, durak.Players[1].Cards[i]);
            }
            this.txtMessages.Update();
            durak.TurnAttack();
        }



        public void GetHumanResponse()
        {
            if (HumanPlayer.TurnStatus == TurnStatus.Defending) {
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
            if (durak.Winner != null) btnTakePass.Text = "End";
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
                    cardSpacing -= (expectedWidth - panelWidth + (padding*2)) / panel.Controls.Count;

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
                    int aiSide = Convert.ToInt32(durak.GetAttacker().GetType() == typeof(Player));
                    if (i % 2 == aiSide)
                    {
                        panel.Controls[i].Location = new Point(leftMargin + (i* cardSpacing), 0);
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
            if (durak.River.Count > 0)
            {
                for (int i = 0; i < durak.Players[0].Cards.Count; i++)
                {
                    CardBox pb = pnlPlayerBottom.Controls[i] as CardBox;
                    List<PlayingCard> cards = HumanPlayer.Cards;
                    bool isValid = (HumanPlayer.TurnStatus == TurnStatus.Attacking ? durak.IsValidAttack(cards[i]) : durak.IsValidDefence(cards[i]));
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
            if (HumanPlayer.TurnStatus == TurnStatus.Attacking && durak.IsValidAttack(cb.Card)
                || HumanPlayer.TurnStatus == TurnStatus.Defending && durak.IsValidDefence(cb.Card))
            {
                durak.RecieveCard(cb.Card);
            }
            else
            {
                durak.Print("\r" + cb.Card.ToString() + " is not a valid play!");
            }
        }
        
        public void TakeRiver(IPlayer player)
        {
            Panel toPanel = null;
            bool faceDown = false;
            lblStatus.Text = player.Name + " takes the river!";
            Wait(200);
            if (player.GetType() == typeof(Player)) {
                toPanel = pnlPlayerBottom;
            }
            else if (player.GetType() == typeof(ComputerPlayer))
            {
                toPanel = pnlPlayerTop;
                faceDown = true;
            }

            for (int i = pnlPlayArea.Controls.Count - 1; i >= 0; i--) {
                CardBox cb = pnlPlayArea.Controls[i] as CardBox;
                if (!faceDown || AI_CARDS_VISIBLE) cb.FaceUp();
                    else cb.FaceDown();
                Console.WriteLine(cb.Card.ToString());
                toPanel.Controls.Add(cb);
                AlignCards(toPanel);
                Wait(100);
            }
            if (player.TurnStatus == TurnStatus.Attacking) durak.NextAttacker();
        }
        public void DiscardCards()
        {
            lblStatus.Text = durak.GetAttacker().Name + " gives up attacking!";
            Wait(200);
            if (pbDiscard.Image == null) pbDiscard.Load(CardBox.GetImagePathFromCard());
            for (int slide = 0; slide < 20; slide++)
            {
                for (int i = pnlPlayArea.Controls.Count - 1; i >= 0; i--)
                {
                    CardBox cb = (CardBox)pnlPlayArea.Controls[i];
                    cb.Left += slide;
                }
                Wait(5);
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
            string path = resourcesPath;
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
            durak.TakeRiver(HumanPlayer);
        }

        public void UpdateMessages(string message)
        {
            this.txtMessages.AppendText( message + "\r\n"); //Print game messages
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
                    if (((CardBox)c).Card != durak.Players[0].Cards[i]) returnValue = false;
                }
            }
            for (int i = 0; i < pnlPlayerTop.Controls.Count; i++)
            {
                Control c = (Control)pnlPlayerTop.Controls[i];
                if (c is CardBox)
                {
                    if (((CardBox)c).Card != durak.Players[1].Cards[i]) returnValue = false;
                }
            }
            return returnValue;
        }
        public void End(IPlayer winner, IPlayer loser)
        {
            foreach (Control c in this.Controls)
            {
                c.Hide();
            }
            Label endPrompt = new Label();
            endPrompt.Text = winner.Name + "won!\n" + loser.Name + " is the Durak!";
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
