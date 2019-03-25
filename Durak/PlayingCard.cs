using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    class PlayingCard
    {
        #region "Get/Set Methods"
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
    }
}
