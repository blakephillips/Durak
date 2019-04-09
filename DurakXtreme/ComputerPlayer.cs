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

        /// <summary>
        /// Computer plays an applicable move depending if it is attacking or defending
        /// </summary>
        /// <param name="playedCards"></param>
        public void PlayMove(ref Deck playedCards, PlayingCard trumpCard)
        {
            if (base.CurrentTurnStatus == TurnStatus.Defending)
                Defend(ref playedCards, trumpCard);
            if (base.CurrentTurnStatus == TurnStatus.Attacking)
                Attack(ref playedCards);
        }

        /// <summary>
        /// Logic for AI defending against the player
        /// </summary>
        /// <param name="river">Cards in river</param>
        private void Defend(ref Deck river, PlayingCard trumpCard)
        {
            //Index of card the computer wants to play
            int playedCardIndex = -1;
            
            int trumpIndex = -1;
            
            for (int i = 0; i < this.Count; i++)
            {
                //If the suit matched the last card
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
                } else if(this[i].Suit == trumpCard.Suit)
                {
                    trumpIndex = i;
                }
            }
            if (playedCardIndex != -1)
            {
                PlayCard(playedCardIndex, ref river);
            } else if (trumpIndex != -1)
            {
                PlayCard(trumpIndex, ref river);
            }
            else
            {
                base.Take(ref river);
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

        /// <summary>
        /// AI plays a selected card to the river
        /// </summary>
        /// <param name="cardIndex">Index of card to be played</param>
        /// <param name="river">River to play the card to</param>
        private void PlayCard(int cardIndex, ref Deck river)
        {
            GameplayLog.Log(this.ToString() + " " + this.CurrentTurnStatus + " with " + this[cardIndex].ToString());
            river.RiverInsert(this[cardIndex]);
            this.Remove(this[cardIndex]);
        }

    }
}
