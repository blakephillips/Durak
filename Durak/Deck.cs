using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
   public class Deck : List<PlayingCard>
    {
        public Deck() { }



        private static Random rand = new Random();
        //TODO add IClonable
        /// <summary>
        /// Shuffles the deck using Fisher-Yates shuffle algorithm
        /// </summary>
        /// <see cref="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle"/>
        public void Shuffle()
        {

            int cardIndex = this.Count;
            while (cardIndex > 1 )
            {
                cardIndex--;
                int randomCardIndex = rand.Next(cardIndex + 1);
                PlayingCard card = this[randomCardIndex];
                this[randomCardIndex] = this[cardIndex];
                this[cardIndex] = card;
            }
        }

        public PlayingCard DrawCard()
        {
            PlayingCard card = this.First();
            this.Remove(card);
            return card;
        }






    }
}
