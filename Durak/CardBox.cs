using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardLibrary
{
    public partial class CardBox : UserControl
    {

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

        private void UpdateCardImage()
        {
            pbCard.Image = myCard.GetCardImage();

            if (orientation == Orientation.Horizontal)
            {
                pbCard.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

        }

        public CardBox()
        {
            InitializeComponent();
            Card = new PlayingCard();
            myOrientation = Orientation.Vertical;
        }


        public CardBox(PlayingCard card, Orientation orientation = Orientation.Vertical)
        {
            InitializeComponent();
            myOrientation = orientation;
            Card = card;
        }

        public CardBox(CardRank rank, CardSuit suit)
        {
            InitializeComponent();
            Card = new PlayingCard(rank, suit);
            myOrientation = Orientation.Vertical;
        }

        private void CardBox_Load(object sender, EventArgs e)
        {
            UpdateCardImage();
        }

        public override string ToString()
        {
            return Card.ToString();
        }

    }
}
