using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        //Path to resources folder
        public const string resourcesPath = "../../Resources/";

        //Create new DurakGame
        public DurakGame durak;
        public IPlayer Player;
        public IPlayer ComputerPlayer;

        //GUI config
        readonly Size cardSize = new Size(75, 110);
        //Get players from game instance

        //Base Constructor
        public frmGameGUI()
        {
            InitializeComponent();

            durak = new DurakGame(this);
            Player = durak.Players[0];
            ComputerPlayer = durak.Players[1];

            this.Show();
            this.InitiateGame();
        }
        
        public void InitiateGame()
        {
            this.pbTrump.Image = Image.FromFile(CardBox.GetImagePathFromCard(durak.TrumpCard));
            this.pbDeck.Load(CardBox.GetImagePathFromCard());
            //this.pbDeck.Refresh();
            //this.pbTrump.Refresh();
            Refresh();

            lblDeckCount.Text = durak.deck.Count.ToString();

            SoundPlayer dealSound = new System.Media.SoundPlayer(resourcesPath + "deal.wav");
            dealSound.Play();
            for (int i = 0; i < DurakGame.STARTING_CARD_COUNT; i++)
            {
                this.DealCardToPanel(this.pnlPlayerBottom, durak.Players[0].Cards[i]);
                this.DealCardToPanel(this.pnlPlayerTop, durak.Players[1].Cards[i], true);
            }
            this.txtMessages.Update();
            durak.TurnAttack();
        }
        public void GetHumanResponse()
        {
            //// Get call stack
            //StackTrace stackTrace = new StackTrace();
            //// Get calling method name
            //Console.WriteLine(stackTrace.GetFrame(1).GetMethod().Name + " - GetHumanResponse()");
            if (Player.TurnStatus == TurnStatus.Attacking) btnTakePass.Text = "Pass";
            if (Player.TurnStatus == TurnStatus.Defending) btnTakePass.Text = "Take";
            EnableCardClick();
            EvaluateHand();
        }
        public void EndHumanResponse()
        {
            //// get call stack
            //stacktrace stacktrace = new stacktrace();
            //// get calling method name
            //console.writeline(stacktrace.getframe(1).getmethod().name + " - endhumanresponse()");
            DisableCardClick();
            foreach (Control card in pnlPlayerBottom.Controls)
            {
                card.MouseEnter -= CardPop;
                card.MouseLeave -= CardPop;
            }
            ResetColors(pnlPlayerBottom);
        }
        //Dealer Methods
        public void DealCardToPanel(Panel panel, PlayingCard card, bool faceDown = false)
        {
            CardBox pbCard = new CardBox(card, faceDown);
            panel.Controls.Add(pbCard);
            AlignCards(panel);
        }
        void ResetColors(Panel panel)
        {
            foreach (CardBox cb in panel.Controls)
            {
                cb.SetCard(cb.Card);
            }
        }
        //GUI Methods
        void AlignCards(Panel panel)
        {
            //panel.Update();
            int panelWidth = panel.Width;
            int panelHeight = panel.Height;
            int cardSpacing = cardSize.Width;
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
                    expectedWidth = (panel.Controls.Count - 1) * cardSpacing + cardSize.Width;
                }
                // Apply new locations to cards
                for (int i = 0; i < panel.Controls.Count; i++)
                {
                    // Initial x-position is half the remaining panel width
                    int leftMargin = (panel.Width - expectedWidth) / 2;
                    panel.Controls[i].Location = new Point(leftMargin + (i * cardSpacing), isBottomPanel ? panelHeight - cardSize.Height : 0);
                }
                panel.Update();
            }
            else if (panel == pnlPlayArea)
            {
                cardSpacing = cardSize.Width / 2;
                int duoSpacing = 50;
                int duoCount = ((panel.Controls.Count - 1) / 2 > 0) ? (panel.Controls.Count - 1) / 2 : 0;

                int expectedWidth = (panel.Controls.Count - 1) * cardSpacing + (duoCount * duoSpacing) + cardSize.Width;
                int leftMargin = (panelWidth - expectedWidth) / 2;
                int alignBottom = panel.Height - cardSize.Height;
                for (int i = 0; i < panel.Controls.Count; i++)
                {
                    if (i > 0 && i % 2 == 0)
                    {
                        Console.WriteLine("Margin Boost");
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
            //panel.Update();
        }
        void CardPop(object sender, EventArgs e)
        {
            float popRatio = 1.2f;
            int newWidth = (int)(cardSize.Width * popRatio);
            int newHeight = (int)(cardSize.Height * popRatio);
            PictureBox card = sender as PictureBox;
            if (card.Size == cardSize)
            {
                card.Size = new Size(newWidth, newHeight);
                card.Location = new Point(card.Location.X - (newWidth - cardSize.Width) / 2, card.Location.Y - (newHeight - cardSize.Height));
            }
            else
            {
                card.Size = new Size(cardSize.Width, cardSize.Height);
                card.Location = new Point(card.Location.X + (newWidth - cardSize.Width) / 2, card.Location.Y + (newHeight - cardSize.Height));
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
                    List<PlayingCard> cards = Player.Cards;
                    bool isValid = (Player.TurnStatus == TurnStatus.Attacking ? durak.IsValidAttack(cards[i]) : durak.IsValidDefence(cards[i]));
                    if (!isValid)
                    {
                        pb.ToMonochrome2();
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
            if (Player.TurnStatus == TurnStatus.Attacking && durak.IsValidAttack(cb.Card)
                || Player.TurnStatus == TurnStatus.Defending && durak.IsValidDefence(cb.Card))
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
            Panel toPanel = (player.GetType() == typeof(Player)) ? pnlPlayerBottom : pnlPlayerTop;

            for (int i = pnlPlayArea.Controls.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(i);
                toPanel.Controls.Add(pnlPlayArea.Controls[i]);
            }
            AlignCards(toPanel);
            if (player.TurnStatus == TurnStatus.Attacking) durak.NextAttacker();
            //durak.TurnAttack();
        }
        public void DiscardCards()
        {
            Console.WriteLine(pbDiscard.Image);
            for (int i = pnlPlayArea.Controls.Count - 1; i >= 0; i--)
            {
                CardBox cb = (CardBox)pnlPlayArea.Controls[i];
                pnlPlayArea.Controls.Remove(cb);
            }
        }

        public void PlayCardAt(int cardIndex, int playerIndex)
        {
            if (playerIndex == 0) EndHumanResponse();

            Panel panel = (playerIndex == 0 ? pnlPlayerBottom : pnlPlayerTop);
            Control card = panel.Controls[cardIndex];
            
            card.Size = cardSize;
            pnlPlayArea.Controls.Add(card);
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
            durak.TakeRiver(Player);
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
            return returnValue;
        }
        private void frmGameGUI_Load(object sender, EventArgs e)
        {

        }
    }
}
