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
        /// <summary>
        /// Constructor for deck
        /// </summary>
        /// <param name="cards">Amount of cards</param>
        /// <param name="shuffle">Should a shuffle be performed</param>
        /// <param name="jokersEnabled">Are there jokers in the deck</param>
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

        /// <summary>
        /// Shuffles the deck of cards
        /// </summary>
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

        /// <summary>
        /// Draws the top most card from the deck
        /// </summary>
        /// <returns>PlayingCard at the top of the deck</returns>
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
