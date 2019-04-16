﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardLibrary;

namespace DurakXtreme
{
    public class DurakGame
    {
        new EventArgs test(object sender, EventArgs e)
        {
            return null;
        }
        new public EventHandler OnDealingComplete;
        public static CardRank[] ranks = {
            CardRank.Six,
            CardRank.Seven,
            CardRank.Eight,
            CardRank.Nine,
            CardRank.Ten,
            CardRank.Jack,
            CardRank.Queen,
            CardRank.King,
            CardRank.Ace
        };
        //Holds the messages generated by the game
        private List<string> messages = new List<string>();

        public const int STARTING_CARD_COUNT = 6;
        public const int MINIMUM_CARD_COUNT = 6;
        public const int NUMBER_OF_PLAYERS = 2;
        public const int DECK_SIZE = 36;

        //Players
        public List<IPlayer> Players = new List<IPlayer>();

        //Class events
        //new public EventHandler PassEvent;

        public Deck deck = new Deck(DECK_SIZE);

        //Trump card
        private PlayingCard trumpCard;
        public PlayingCard TrumpCard { get { return trumpCard; } }

        public bool GameOver { get; set; }
        private bool outOfCards;

        //Turn variables
        public List<PlayingCard> River = new List<PlayingCard>();

        frmGameGUI gui = null;

        public DurakGame(frmGameGUI guiSubscriber = null)
        {
            if (guiSubscriber is frmGameGUI)
            {
                gui = guiSubscriber;
            }
            InitializeDeck();
            InitializePlayers();
            DealCards();
            RevealTrump();
            SetFirstAttacker();
            Console.WriteLine("DURAK INITIALIZATION COMPLETE");
            Console.WriteLine(" Deck Contents:");
            PrintListOfCards(deck, "    ");
            Console.WriteLine(" " + Players[0].Name + "'s Hand:");
            PrintListOfCards(Players[0].Cards, "    ");
            Console.WriteLine(" " + Players[1].Name + "'s Hand:");
            PrintListOfCards(Players[1].Cards, "    ");
            
        }
        void InitializeDeck()
        {
            Console.WriteLine("Initializing Deck...");

            int index = 0;
            for (int suit = (int)CardSuit.Spades; suit < Enum.GetValues(typeof(CardSuit)).Length; suit++)
            {
                foreach (CardRank rank in ranks)
                {
                    if (index < DECK_SIZE) deck[index].Set(rank, (CardSuit)suit);
                    index++;
                }
            }
            this.deck.Shuffle();
            Console.WriteLine("Deck of " + index + " Cards Initialized!");
        }
        void InitializePlayers()
        {
            Players.Add(new Player());
            Players.Add(new ComputerPlayer());
        }
        void DealCards()
        {
            for (int cardsToDeal = STARTING_CARD_COUNT; cardsToDeal > 0; cardsToDeal--)
            {
                for (int player = 0; player < NUMBER_OF_PLAYERS; player++)
                {
                    Players[player].Cards.Add(deck.DrawTopCard());
                }
            }
            Players[0].Cards.Sort();
            Players[1].Cards.Sort();
        }
        void RevealTrump()
        {
            trumpCard = deck.DrawTopCard();
            Print("Cards dealt! The trump card is the " + trumpCard.ToString() + "!");
        }
        void SetFirstAttacker()
        {
            int currentLowestTrump = -1;
            IPlayer startingPlayer = null;
            foreach (IPlayer player in Players)
            {
                foreach (PlayingCard card in player.Cards)
                {
                    int realRank = Array.IndexOf(ranks, card.Rank);
                    if (card.Suit == trumpCard.Suit)
                    {
                        if (currentLowestTrump == -1 || realRank < currentLowestTrump)
                        {
                            currentLowestTrump = realRank;
                            startingPlayer = player;
                        }
                    }
                }
            }
            if (startingPlayer == null)
            {
                startingPlayer = Players[0];
                Print("Neither player has a trump card." + Players[0].Name + " will attack first.");
            }
            else
            {
                Print(startingPlayer.Name + " has the lowest trump card, and will make the first attack.");
            }
            startingPlayer.TurnStatus = TurnStatus.Attacking;

        }
        public void RecieveCard(PlayingCard card)
        {
            int attackCardIndex = Players[0].Cards.IndexOf(card);
            string msg = Players[0].Name + " is " + (Players[0].TurnStatus == TurnStatus.Attacking ? "attacking" : "defending against");
            msg += " " + Players[1].Name + (Players[0].TurnStatus == TurnStatus.Attacking ? "" : "'s " + River.Last().ToString()) + " with a " + card.ToString() + "!";
            Print(msg);
            River.Add(((Player)Players[0]).Attack(card));

            gui.PlayCardAt(attackCardIndex, 0);

            if (Players[0].TurnStatus == TurnStatus.Attacking)
            {
                TurnDefence();
            }
            else
            {
                TurnAttack();
            }
        }
        public void TurnAttack()
        {
            if (!GameOver)
            {
                IPlayer attacker = GetAttacker();
                IPlayer defender = GetDefender();
                if (attacker.GetType() == typeof(ComputerPlayer))
                {
                    ComputerPlayer ai = attacker as ComputerPlayer;
                    Tuple<PlayingCard, int> attack = ai.Attack(River);
                    PlayingCard attackCard = attack.Item1;
                    int attackCardIndex = attack.Item2;
                    if (attackCardIndex == -1)
                    {
                        TakeRiver(ai);
                    }
                    else
                    {
                        Print(attacker.Name + " is attacking " + defender.Name + " with a " + attackCard.ToString() + "!");
                        River.Add(attackCard);
                        gui.PlayCardAt(attackCardIndex, Players.IndexOf(attacker));
                        gui.GetHumanResponse();
                    }
                }
                if (attacker.GetType() == typeof(Player))
                {
                    gui.GetHumanResponse();
                }
            }
            else
            {
                gui.End();
            }
            
        }
        public void TurnDefence()
        {
            Console.WriteLine("GUI Synced " + gui.CheckSync());
            IPlayer attacker = GetAttacker();
            IPlayer defender = GetDefender();
            if (defender.GetType() == typeof(ComputerPlayer))
            {
                ComputerPlayer ai = defender as ComputerPlayer;
                Tuple<PlayingCard, int> defence = ai.Defend(River);
                PlayingCard defendCard = defence.Item1;
                int defendCardIndex = defence.Item2;
                if (defendCardIndex == -1)
                {
                    TakeRiver(ai);
                }
                else
                {
                    Print(defender.Name + " is defending against " + attacker.Name + "'s " + River.Last().ToString() + " with a " + defendCard.ToString() + "!");
                    River.Add(defendCard);
                    gui.PlayCardAt(defendCardIndex, Players.IndexOf(defender));
                    
                    gui.GetHumanResponse();
                }
            }
            if (defender.GetType() == typeof(Player))
            {
                if (gui is frmGameGUI)
                {
                    Console.WriteLine("Awaiting user defence response");
                    gui.GetHumanResponse();
                }
            }
        }

        public void TakeRiver(IPlayer player)
        {
            if (player.TurnStatus == TurnStatus.Defending)
            {
                for (int i = River.Count - 1; i >= 0; i--)
                {
                    player.Cards.Add(River[i]);
                }
                Print(player.Name + " is taking the river, raising them to " + player.Cards.Count + " cards!");
                gui.TakeRiver(player);
            }
            else
            {
                Print(player.Name + " Yields. River is discarded and " + GetDefender().Name + " is now attacker.");
                gui.DiscardCards();
                NextAttacker();
            }
            River.Clear();
            if (!outOfCards) ReplenishCards();
            else if (Players[0].Cards.Count == 0 || Players[1].Cards.Count == 0) GameOver = true;
            TurnAttack();
        }
        static public int GetDurakRank(CardRank rank)
        {
            return Array.IndexOf(DurakGame.ranks, rank);
        }
        public bool IsValidAttack(PlayingCard card)
        {
            bool valid = false;
            if (River.Count == 0)
            {
                
                valid = true;
            }
            else
            {
                foreach (PlayingCard riverCard in River)
                {
                    if (riverCard.Rank == card.Rank)
                    {
                        valid = true;
                    }
                }
            }
            return valid;
        }
        public bool IsValidDefence(PlayingCard card)
        {
            bool valid = false;
            if (card.Suit == River.Last().Suit && GetDurakRank(card.Rank) > GetDurakRank(River.Last().Rank))
            {
                valid = true;
            }
            return valid;
        }
        public static void PrintListOfCards(List<PlayingCard> list, string tabbing = "")
        {
            foreach (PlayingCard card in list)
            {
                Console.WriteLine(tabbing + card.ToString());
            }
        }
        public void Print(string message, bool toConsole = true, bool toGame = true)
        {
            Console.WriteLine(message);
            messages.Add(message);
            gui.UpdateMessages(GetMessages());
        }
        public string GetMessages()
        {
            string str = string.Join("\r\n", messages);
            messages.Clear();
            return str;
        }
        public void NextAttacker()
        {
            IPlayer attacker = GetAttacker();
            IPlayer defender = GetDefender();
            defender.TurnStatus = TurnStatus.Attacking;
            attacker.TurnStatus = TurnStatus.Defending;
            Console.WriteLine(attacker.Name + " is now " + attacker.TurnStatus);
            Console.WriteLine(defender.Name + " is now " + defender.TurnStatus);
        }
        public IPlayer GetAttacker()
        {
            IPlayer attacker = null;
            foreach (IPlayer player in Players)
            {
                if (player.TurnStatus == TurnStatus.Attacking) attacker = player;
            }
            return attacker;
        }
        public IPlayer GetDefender()
        {
            IPlayer defender = null;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].TurnStatus == TurnStatus.Attacking)
                {
                    if (i == Players.Count - 1)
                    {
                        defender = Players[0];
                    }
                    else
                    {
                        defender = Players[i + 1];
                    }
                }
            }
            return defender;
        }

        public void Sort()
        {
            //Players[0].Cards.Sort()
        }

        public void ReplenishCards()
        {
            foreach (IPlayer player in Players)
            {
                while (player.Cards.Count < MINIMUM_CARD_COUNT && deck.Count >= 0)
                {
                    if (deck.Count >= 0)
                    {
                        if (deck.Count == 0 && !deck.Contains(TrumpCard)) {
                            deck.Add(TrumpCard);
                            gui.Controls.Remove(gui.pbDeck);}
                        if (deck.Contains(TrumpCard))
                        {
                            outOfCards = true;
                            gui.Controls.Remove(gui.pbTrump);
                        }
                        player.Cards.Add(deck.DrawTopCard());

                        if (player.GetType() == typeof(Player))
                            gui.DealCardToPanel(gui.pnlPlayerBottom, player.Cards.Last());
                        if (player.GetType() == typeof(ComputerPlayer))
                            gui.DealCardToPanel(gui.pnlPlayerTop, player.Cards.Last());
                        gui.lblDeckCount.Text = deck.Count.ToString();
                        gui.Wait(150);
                    }
                }
            }
        }
    }
}
