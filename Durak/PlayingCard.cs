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
        private bool _faceDown = false;
        public PlayingCard(CardRank rank = CardRank.Ace, CardSuit suit = CardSuit.Spades, bool faceDown = false)
        {
            Rank = rank;
            Suit = suit;
            if (faceDown) FlipCard();
        }
        public void Set(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }
        public CardSuit Suit { get; set; }
        public CardRank Rank { get; set; }
        public bool IsFaceDown { get { return _faceDown; } }
        public bool IsFaceUp { get { return !_faceDown; } }

        public void FlipCard()
        {
            _faceDown = !_faceDown;
        }
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
