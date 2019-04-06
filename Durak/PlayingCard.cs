using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace CardLibrary
{
    public class PlayingCard
    {
        #region "Get/Set Methods"

        private CardBox myCardBox;
        public CardBox CardControl
        {
            get
            {
                if (myCardBox == null)
                    IntializeCardBox();
                return myCardBox;
            }
            set { myCardBox = value; }
        }

        protected CardSuit mySuit;
        public CardSuit Suit
        {
            get { return mySuit; }
            set { mySuit = value; }
        }
        protected CardRank myRank;
        public CardRank Rank
        {
            get { return myRank; }
            set { myRank = value; }
        }

        protected bool myFaceUp = false;
        public bool FaceUp
        {
            get { return myFaceUp; }
            set { myFaceUp = value; }
        }

        protected int myValue;
        public int CardValue
        {
            get { return myValue; }
            set { myValue = value; }
        }
        #endregion
        #region "Constructors"

        public PlayingCard(CardRank rank = CardRank.Ace, CardSuit suit = CardSuit.Hearts)
        {
            Rank = rank;
            Suit = suit;
            CardValue = (int)Rank;
        }

        #endregion
        #region "Relational Operators"

        public static bool operator ==(PlayingCard left, PlayingCard right)
        {
            return (left.CardValue == right.CardValue);
        }

        public static bool operator !=(PlayingCard left, PlayingCard right)
        {
            return (left.CardValue != right.CardValue);
        }

        public static bool operator <(PlayingCard left, PlayingCard right)
        {
            return (left.CardValue < right.CardValue);
        }

        public static bool operator >(PlayingCard left, PlayingCard right)
        {
            return (left.CardValue > right.CardValue);
        }

        public static bool operator <=(PlayingCard left, PlayingCard right)
        {
            return (left.CardValue <= right.CardValue);
        }

        public static bool operator >=(PlayingCard left, PlayingCard right)
        {
            return (left.CardValue >= right.CardValue);
        }



        #endregion

        public override bool Equals(object obj)
        {
            return (this.CardValue == ((PlayingCard)obj).CardValue);
        }

        public override int GetHashCode()
        {
            return this.myValue * 100 + (int)this.mySuit * 10 + ((this.FaceUp) ? 1 : 0);
        }

        public override string ToString()
        {
            return Rank.ToString() + " of " + Suit.ToString();
        }

        public Image GetCardImage()
        {
            string imageName;
            Image cardImage;

            if (!FaceUp)
            {
                imageName = "back";
            }
            else if (myRank == CardRank.Joker)
            {
                if (mySuit == CardSuit.Clubs || mySuit == CardSuit.Spades)
                {
                    imageName = "black_joker";
                }
                else
                {
                    imageName = "red_joker";
                }
            }
            else
            {
                imageName = myRank.ToString().ToLower() + "_of_" + mySuit.ToString().ToLower();
            }

            cardImage = Properties.Resources.ResourceManager.GetObject(imageName) as Image;


            return cardImage;

        }

        private void IntializeCardBox()
        {
            myCardBox = new CardBox(this);
        }


    }
}
