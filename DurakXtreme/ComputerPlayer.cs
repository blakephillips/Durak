using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;
using DurakXtreme;

namespace DurakXtreme
{
    
    public class ComputerPlayer : Player
    {
        public ComputerPlayer() : base()
        {
            Name += "(AI)";
        }
        public Tuple<PlayingCard, int> Attack(List<PlayingCard> river = null)
        {
            PlayingCard bestViableCard = null;
            int cardIndex;
            foreach (PlayingCard card in Cards)
            {
                if (river == null || river.Count == 0)
                {
                    if (bestViableCard == null || DurakGame.GetDurakRank(card.Rank) < DurakGame.GetDurakRank(bestViableCard.Rank))
                    {
                        bestViableCard = card;
                    }
                }
                else
                {
                    foreach (PlayingCard riverCard in river)
                    {
                        if (riverCard.Rank == card.Rank)
                        {
                            bestViableCard = card;
                        }
                    }
                }
            }
            cardIndex = Cards.IndexOf(bestViableCard);
            
            if (bestViableCard != null)
            {
                Cards.Remove(bestViableCard);
            }
            return Tuple.Create(bestViableCard, cardIndex);
        }
        public Tuple<PlayingCard, int> Defend(List<PlayingCard> river)
        {
            PlayingCard bestViableCard = null;
            foreach (PlayingCard card in Cards)
            {
                if (card.Suit == river.Last().Suit && DurakGame.GetDurakRank(card.Rank) > DurakGame.GetDurakRank(river.Last().Rank))
                {
                    if (bestViableCard == null || DurakGame.GetDurakRank(card.Rank) < DurakGame.GetDurakRank(bestViableCard.Rank))
                    {
                        bestViableCard = card;
                    }
                }
            }
            int cardIndex = Cards.IndexOf(bestViableCard);
            if (bestViableCard != null)
            {
                Cards.Remove(bestViableCard);
            }

            return Tuple.Create(bestViableCard, cardIndex);
        }
    }
}
