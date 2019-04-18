/* Authors: Blake, Clayton, Dylan
 * File Name: Player.cs
 * 
 * Description: Human player class
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace DurakXtreme
{
    public class Player : IPlayer
    {
        //Properties
        public string Name { get; set; }
        public List<PlayingCard> Cards { get; set; } = new List<PlayingCard>();
        public TurnStatus TurnStatus { get; set; } = TurnStatus.Defending;
        public bool PuttingDown { get; set; }
        
        private static int count = 1;
        public Player()
        {
            Name = "Player " + count;
            count++;
        }
        public virtual PlayingCard Attack()
        {
            return null;
        }
        public virtual PlayingCard Attack(PlayingCard card)
        {
            Cards.Remove(card);
            return card;
        }
        public virtual PlayingCard Defend()
        {
            return new PlayingCard(CardRank.Ace, CardSuit.Spades);
        }
    }
    public enum TurnStatus : byte
    {
        Attacking,
        Defending
    }
}
