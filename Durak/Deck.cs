using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
   public class Deck : List<PlayingCard>
    {
        private static Random rng = new Random();
        public Deck(int cards = 52, bool shuffle = false, bool jokersEnabled = false)
        {
            int index = 0;
            for (int suit = 1; suit < Enum.GetValues(typeof(CardSuit)).Length; suit++)
            {
                for (int rank = 1; rank < Enum.GetValues(typeof(CardRank)).Length && index < cards; rank++)
                {
                    if (jokersEnabled && index >= cards - 2)
                    {
                        this.Add(new PlayingCard(CardRank.Joker, CardSuit.Joker));
                    } else
                    {
                        this.Add(new PlayingCard((CardRank)rank, (CardSuit)suit));
                    }
                    index++;
                }
            }
        }

        public void Shuffle()
        {
            int cardIndex = this.Count;
            while (cardIndex > 1)
            {
                cardIndex--;
                int randomCardIndex = rng.Next(cardIndex + 1);
                PlayingCard card = this[randomCardIndex];
                this[randomCardIndex] = this[cardIndex];
                this[cardIndex] = card;
            }
            Console.WriteLine("Deck Shuffled!");
        }
        public PlayingCard DrawTopCard()
        {
            PlayingCard topCard = null;
            if (this.Count > 0)
            {
                topCard = this.Last();
                this.Remove(this.Last());
            }
            return topCard;
        }

    }

}
