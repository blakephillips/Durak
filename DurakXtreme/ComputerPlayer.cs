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
        public CardSuit TrumpSuit { get; set; }
        public Tuple<PlayingCard, int> Attack(List<PlayingCard> river = null)
        {
            PlayingCard bestViableCard = null;
            PlayingCard lowestTrumpCard = null;
            foreach (PlayingCard card in Cards)
            {
                if (river.Count == 0)
                {
                    if (card.Suit == TrumpSuit) {
                        if (lowestTrumpCard == null || DurakGame.GetDurakRank(card.Rank) < DurakGame.GetDurakRank(lowestTrumpCard.Rank)) lowestTrumpCard = card;
                    }
                    else if (card.Suit != TrumpSuit) { 
                        if (bestViableCard == null || DurakGame.GetDurakRank(card.Rank) < DurakGame.GetDurakRank(bestViableCard.Rank)) bestViableCard = card;
                    }
                }
                else
                {
                    foreach (PlayingCard riverCard in river)
                    {
                        if (riverCard.Rank == card.Rank)
                        {
                            if (card.Suit == TrumpSuit)
                            {
                                if (lowestTrumpCard == null || DurakGame.GetDurakRank(card.Rank) < DurakGame.GetDurakRank(lowestTrumpCard.Rank)) lowestTrumpCard = card;
                            }
                            else if (card.Suit != TrumpSuit)
                            {
                                if (bestViableCard == null || DurakGame.GetDurakRank(card.Rank) < DurakGame.GetDurakRank(bestViableCard.Rank)) bestViableCard = card;
                            }
                        }
                    }
                }
            }
            if (bestViableCard == null && lowestTrumpCard != null && river.Count > 3)
            {
                bestViableCard = lowestTrumpCard;
            }
            int cardIndex = Cards.IndexOf(bestViableCard);
            if (bestViableCard != null)
            {
                Cards.Remove(bestViableCard);
            }
            return Tuple.Create(bestViableCard, cardIndex);
        }
        public Tuple<PlayingCard, int> Defend(List<PlayingCard> river)
        {
            PlayingCard bestViableCard = null;
            PlayingCard lowestTrumpCard = null;
            foreach (PlayingCard card in Cards)
            {
                if (river.Last().Suit != TrumpSuit && card.Suit == TrumpSuit)
                {
                    if (lowestTrumpCard == null || DurakGame.GetDurakRank(card.Rank) < DurakGame.GetDurakRank(lowestTrumpCard.Rank))
                    {
                        lowestTrumpCard = card;
                    }
                }
                else if ((card.Suit == river.Last().Suit) && DurakGame.GetDurakRank(card.Rank) > DurakGame.GetDurakRank(river.Last().Rank))
                {
                    if (bestViableCard == null || DurakGame.GetDurakRank(card.Rank) < DurakGame.GetDurakRank(bestViableCard.Rank))
                    {
                        bestViableCard = card;
                    }
                }
            }
            if (bestViableCard == null && lowestTrumpCard != null)
            {
                bestViableCard = lowestTrumpCard;
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
