using System;
using System.Drawing;
using System.Windows.Forms;

namespace CardLibrary
{
    public partial class CardBox : UserControl
    {
        #region "Get/Set Methods"
        private PlayingCard myCard;
        
        public PlayingCard Card
        {
            set
            {
                myCard = value;
                UpdateCardImage();
            }
            get { return myCard; }
        }

        public CardSuit Suit
        {
            set
            {
                Card.Suit = value;
                UpdateCardImage();
            }
            get { return Card.Suit; }
        }

        public bool FaceUp
        {
            set
            {
                if (myCard.FaceUp != value)
                {
                    myCard.FaceUp = value;
                    UpdateCardImage();
                    
                }
            }
            get { return Card.FaceUp; }
        }


        public CardRank Rank
        {
            set
            {
                Card.Rank = value;
                UpdateCardImage();
            }
            get { return Card.Rank; }
        }

        private Orientation myOrientation;

        public Orientation orientation
        {
            set
            {
                if (myOrientation != value)
                {
                    myOrientation = value;
                    this.Size = new Size(Size.Height, Size.Width);
                    UpdateCardImage();
                }
            }
            get { return myOrientation; }
        }
        #endregion

        private void UpdateCardImage()
        {
            pbCard.Image = myCard.GetCardImage();

            if (orientation == Orientation.Horizontal)
            {
                pbCard.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

        }
        #region "Constructors"
        public CardBox()
        {
            InitializeComponent();
            Card = new PlayingCard();
            myOrientation = Orientation.Vertical;
        }


        public CardBox(PlayingCard card, Orientation orientation = Orientation.Vertical, bool isFaceUp = true)
        {
            InitializeComponent();
            myOrientation = orientation;
            Card = card;
            FaceUp = isFaceUp;
        }


        public CardBox(CardRank rank, CardSuit suit)
        {
            InitializeComponent();
            Card = new PlayingCard(rank, suit);
            myOrientation = Orientation.Vertical;
        }
        #endregion


        /// <summary>
        /// Initialize card image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardBox_Load(object sender, EventArgs e)
        {
            UpdateCardImage();
        }

        /// <summary>
        /// Overrides ToString() to return the card lib's toString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Card.ToString();
        }

        /// <summary>
        /// Click event handler
        /// </summary>
        new public event EventHandler Click;

        /// <summary>
        /// Mouse over event handler
        /// </summary>
        new public event EventHandler MouseEnter;

        /// <summary>
        /// Mouse leave event handler
        /// </summary>
        new public event EventHandler MouseLeave;


        /// <summary>
        /// On click of card, if event handler is set, trigger event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCard_MouseClick(object sender, MouseEventArgs e)
        {
            if (Click != null)
                Click(this,e);
        }

        private void pbCard_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// When the mouse hovers over a card it will trigger an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCard_MouseEnter(object sender, EventArgs e)
        {
            if (MouseEnter != null)
                MouseEnter(this, e);
        }

        /// <summary>
        /// When the mouse leaves over a card it will trigger an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCard_MouseLeave(object sender, EventArgs e)
        {
            if (MouseLeave != null)
                MouseLeave(this, e);
        }
    }
}
