using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace DurakXtreme
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string pName) : base(pName)
        {   }


        public void PlayMove(ref Deck playedCards)
        {
            if (base.CurrentTurnStatus == TurnStatus.Defending)
                Defend(ref playedCards);
            if (base.CurrentTurnStatus == TurnStatus.Attacking)
                Attack(ref playedCards);
        }

        /// <summary>
        /// Logic for AI defending against the player
        /// </summary>
        /// <param name="river">Cards in river</param>
        private void Defend(ref Deck river)
        {
            int playedCardIndex = -1;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Suit == river.LastCardInputted.Suit && this[i].CardValue > river.LastCardInputted.CardValue)
                {
                    if (playedCardIndex == -1)
                    {
                        playedCardIndex = i;
                    }
                    else if (this[playedCardIndex].CardValue > this[i].CardValue)
                    {
                        playedCardIndex = i;
                    }
                }
            }
            if (playedCardIndex != -1)
            {
                PlayCard(playedCardIndex, ref river);
            } else
            {
                this.Take(ref river);
            }
            

        }

        /// <summary>
        /// Logic for AI attacking the player.
        /// </summary>
        /// <param name="river"></param>
        private void Attack(ref Deck river)
        {
            bool foundAttackCard = false;
            //index of the card to be played
            int playedCardIndex = 0;
            //if there is 0 cards in t
            if (river.Count == 0)
            {
                
               //Get lowest card in hand and set the played card to it 
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].CardValue <= this[playedCardIndex].CardValue)
                    {
                        foundAttackCard = true;
                        playedCardIndex = i;
                    }
                }
                
           //if there are cards on the table
            } else if (river.Count > 0)
            {
                //play the first cardValue that is equal to one on the table
                for (int i = 0; i > river.Count; i++)
                {
                    for (int j = 0; j > this.Count; j++)
                    {
                        if (river[i].CardValue == this[j].CardValue)
                        {
                            foundAttackCard = true;
                            playedCardIndex = j;
                        }
                            
                    }
                }
            }
            //if a card was found to play
            if (playedCardIndex != -1 && foundAttackCard == true)
            {
                //play the card
                PlayCard(playedCardIndex, ref river);
            } else
            {
                //called from base class(Player)
                base.Pass(ref river);
            }
                
        }

        private void PlayCard(int cardIndex, ref Deck river)
        {
            
            GameplayLog.Log(this.ToString() + " " + this.CurrentTurnStatus + " with " + this[cardIndex].ToString());
            river.RiverInsert(this[cardIndex]);
            this.Remove(this[cardIndex]);
        }

    }
}
