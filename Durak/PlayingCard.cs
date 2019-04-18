using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace CardLibrary
{
    public class PlayingCard : IComparable<PlayingCard>
    {
        public PlayingCard(CardRank rank = CardRank.Ace, CardSuit suit = CardSuit.Hearts)
        {
            Rank = rank;
            Suit = suit;
            CardValue = (int)Rank;
        }
        public void Set(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }
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
        #endregion "Get/Set Methods"
        public override string ToString()
        {
            return Rank == CardRank.Joker ? CardRank.Joker.ToString() : Rank + " of " + Suit;
        }

        public int CompareTo(PlayingCard that)
        {
            int returnValue = ((int)this.Rank > (int)that.Rank) ? 1 : -1;
            return returnValue;
        }
    }
}
