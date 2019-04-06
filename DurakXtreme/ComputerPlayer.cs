﻿using System;
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
        /// <param name="playedCards">Cards in river</param>
        private void Defend(ref Deck playedCards)
        {
            
        }

        /// <summary>
        /// Logic for AI attacking the player.
        /// </summary>
        /// <param name="playedCards"></param>
        private void Attack(ref Deck playedCards)
        {

            
            if (playedCards.Count == 0)
            {
               
                int lowestCardIndex = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].CardValue < this[lowestCardIndex].CardValue)
                    {
                        lowestCardIndex = i;
                    }
                }
                PlayCard(lowestCardIndex);
           
            }
        }

        private void PlayCard(int cardIndex)
        {
            GameplayLog.Log("P1 " + this.CurrentTurnStatus + " with " + this[cardIndex].ToString());
        }

    }
}
