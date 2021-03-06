﻿/* Authors: Blake, Clayton, Dylan
 * File Name: DurakGame.cs
 * 
 * Description: Controls the game-flow and game-state
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardLibrary;

namespace DurakXtreme
{
    /// <summary>
    /// Contains the information and methods for a human vs AI Durak Game.
    /// </summary>
    public class DurakGame
    {
        /// <summary>
        /// If a putting down event is triggered
        /// </summary>
        new public EventHandler OnPuttingDown;
        new public EventHandler PuttingDownComplete;
        new public EventHandler OnDealingComplete;
        //Durak card values
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
        //Constants
        public const int NUMBER_OF_PLAYERS = 2;     // # of players
        public const int STARTING_CARD_COUNT = 6;   // # of cards players start with
        public const int MINIMUM_CARD_COUNT = 6;    // # of cards players must have before the start of each turn
        public const int DECK_SIZE = 36;            // # of cards in the deck

        private bool outOfCards;    // Toggles on once a winner is detected
        public Deck deck = new Deck(DECK_SIZE); // Durak deck
        public List<PlayingCard> River = new List<PlayingCard>(); // River

        // Player list
        public List<IPlayer> Players = new List<IPlayer>();
        
        // Trump card
        private PlayingCard trumpCard;
        public PlayingCard TrumpCard { get { return trumpCard; } }

        public IPlayer Winner { get; set; }
        public IPlayer Loser { get; set; }
        
        // A form GUI to be attatched to the game
        frmGameGUI gui;

        public DurakGame(frmGameGUI guiSubscriber = null)
        {
            // Assign form passed in constructor to class-scope variable
            if (guiSubscriber is frmGameGUI)
            {
                gui = guiSubscriber;
            }

            // Set PlayingCard values and shuffle deck
            InitializeDeck();
            // Add players to player list
            InitializePlayers();
            // Deal cards from the deck to the player hands,
            // sorting them once by rank before sending to the GUI
            DealCards();
            // TODO - add hand sorting by Durak rank, syncing with the GUI

            // Reveal and assign the trump card
            RevealTrump();
            // Set first attacker to the player with the lowest trump card
            // (Defaults to Player 1 if no player has a trump card)
            SetFirstAttacker();

            // Log
            Console.WriteLine("DURAK INITIALIZATION COMPLETE");
            Console.WriteLine(" Deck Contents:");
            PrintListOfCards(deck, "    ");
            Console.WriteLine(" " + Players[0].Name + "'s Hand:");
            PrintListOfCards(Players[0].Cards, "    ");
            Console.WriteLine(" " + Players[1].Name + "'s Hand:");
            PrintListOfCards(Players[1].Cards, "    ");
        }

        /// <summary>
        /// TurnAttack() - The attack phase. A Durak round begins here, and 
        ///             after cards are either discarded or taken from the river,
        ///             this function is called to begin a new turn.
        /// </summary>
        public void TurnAttack()
        {
            // Verify that the GUI is synced
            if (gui is frmGameGUI) gui.CheckSync();

            CheckForWinner();
            IPlayer attacker = GetAttacker();
            IPlayer defender = GetDefender();
            if (Winner == null)
            {
                if (attacker.GetType() == typeof(ComputerPlayer))
                {
                    ComputerPlayer ai = attacker as ComputerPlayer;
                    Tuple<PlayingCard, int> attack = ai.Attack(River, deck);
                    PlayingCard attackCard = attack.Item1;
                    int attackCardIndex = attack.Item2;
                    if (attackCardIndex == -1)
                    {
                        TakeRiver(ai);
                        gui.gameStats.defensesRepelled++;
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
        }
        /// <summary>
        /// TurnDefence() - The defence phase, called after an attack card
        ///             has been submitted. Depending on if a defence is made,
        ///             the river will either be taken or discarded, and
        ///             TurnAttack() will be called to start a new round.
        /// </summary>
        public void TurnDefence()
        {
            // Verify that the GUI is synced
            if (gui is frmGameGUI) gui.CheckSync();

            CheckForWinner();
            IPlayer attacker = GetAttacker();
            IPlayer defender = GetDefender();
            if (Winner == null && attacker.PuttingDown == false)
            {
                if (defender.GetType() == typeof(ComputerPlayer))
                    {
                    ComputerPlayer ai = defender as ComputerPlayer;
                    Tuple<PlayingCard, int> defence = ai.Defend(River, deck);
                    PlayingCard defendCard = defence.Item1;
                    int defendCardIndex = defence.Item2;
                    if (defendCardIndex == -1)
                    {
                        PutDown();
                    }
                    else
                    {
                        if (River.Count == 12 || defender.Cards.Count == 0)
                        {
                            NextAttacker();
                        } else
                        {
                            Print(defender.Name + " is defending against " + attacker.Name + "'s " + River.Last().ToString() + " with a " + defendCard.ToString() + "!");
                            River.Add(defendCard);
                            gui.PlayCardAt(defendCardIndex, Players.IndexOf(defender));
                            gui.GetHumanResponse();
                        }
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
        }
        /// <summary>
        /// RecieveCard(PlayingCard card)
        ///         Public function to which the GUI can pass
        ///         user card selections.
        /// </summary>
        /// <param name="card">The PlayingCard selected from the GUI.</param>
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
                if (Players[0].PuttingDown == true)
                {
                    TurnAttack();
                } else
                {
                    TurnDefence();
                }
            }
            else
            {
                TurnAttack();
            }
        }
        /// <summary>
        /// TakeRiver(IPlayer loser)
        ///         Called when the defender yields and
        ///         must collect the cards in the center. 
        /// </summary>
        /// <param name="loser">Loser of the last round</param>
        public void TakeRiver(IPlayer loser)
        {

            if (loser.TurnStatus == TurnStatus.Defending)
            {
                for (int i = River.Count - 1; i >= 0; i--)
                {
                    loser.Cards.Add(River[i]);
                }
                Print(loser.Name + " is taking the river, raising them to " + loser.Cards.Count + " cards!");
                gui.GiveRiver(loser);
            }
            else
            {
                Print(loser.Name + " yields. River is discarded and " + GetDefender().Name + " is now attacker.");
                gui.DiscardCards();
                NextAttacker();
            }
            River.Clear();
            if (!outOfCards) ReplenishCards();
            
            TurnAttack();
        }
        /// <summary>
        /// PutDown()
        ///         Allows the winner to put down more cards
        ///         before the river is taken and another round is started.
        /// </summary>
        public void PutDown()
        {
            IPlayer attacker = GetAttacker();
            IPlayer defender = GetDefender();

            if (OnPuttingDown != null)
                OnPuttingDown(attacker, new EventArgs());

            if (attacker.GetType() == typeof(ComputerPlayer))
            {
                if (attacker.PuttingDown)
                {
                    ComputerPlayer ai = attacker as ComputerPlayer;
                    Tuple<PlayingCard, int> attack = ai.Attack(River, deck);
                    PlayingCard attackCard = attack.Item1;
                    int attackCardIndex = attack.Item2;
                    if (attackCardIndex == -1)
                    {
                        if (PuttingDownComplete != null)
                            PuttingDownComplete(attacker, new EventArgs());
                    }
                    else
                    {
                        Print(attacker.Name + " is attacking " + defender.Name + " with a " + attackCard.ToString() + "!");
                        River.Add(attackCard);
                        gui.PlayCardAt(attackCardIndex, Players.IndexOf(attacker));
                        PutDown();
                    }
                } else
                {
                    TakeRiver(defender);
                    if (defender.GetType() == typeof(Player))
                    {
                        gui.GetHumanResponse();
                    }
                }
            }
            else if (attacker.GetType() == typeof(Player))
            {
                if (attacker.PuttingDown)
                {
                    gui.GetHumanResponse();
                } else
                {
                    if (PuttingDownComplete != null)
                        PuttingDownComplete(attacker, new EventArgs());
                }
            }
        }
        /// <summary>
        /// IsValidAttack(PlayingCard card)
        ///         Tests a card against the river to check that
        ///         it is a valid Attack card.
        /// </summary>
        /// /// <param name="card">The PlayingCard being tested.</param>
        /// /// <returns>Returns true if the attack is valid</returns>
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
        /// <summary>
        /// IsValidDefence(PlayingCard card)
        ///         Tests a card against the river to check that
        ///         it is a valid Defence card.
        /// </summary>
        /// /// <param name="card">The PlayingCard being tested.</param>
        /// /// <returns>Returns true if the defense is valid</returns>
        public bool IsValidDefence(PlayingCard card)
        {
            bool valid = false;
            if (River.Last().Suit != TrumpCard.Suit && card.Suit == TrumpCard.Suit)
            {
                valid = true;
            }
            if ((card.Suit == River.Last().Suit) && GetDurakRank(card.Rank) > GetDurakRank(River.Last().Rank))
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
        /// <summary>
        /// Logs gameplay messages to the console and log text file if applicable
        /// </summary>
        /// <param name="message">Message to write to Console</param>
        /// <param name="log">True if the message is to be written to the log file</param>
        public void Print(string message, bool log = true)
        {
            if (log == true)
            {
                GameplayLog.Log(message);
            } else
            {
                Console.WriteLine(message);
            }
            
            if (gui is frmGameGUI) gui.PrintMessage(message);
        }
        // Returns the player currently attacking
        public IPlayer GetAttacker()
        {
            IPlayer attacker = null;
            foreach (IPlayer player in Players)
            {
                if (player.TurnStatus == TurnStatus.Attacking) attacker = player;
            }
            return attacker;
        }
        // Returns the player currently defending
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
        /// <summary>
        /// NextAttacker()
        ///         Moves the attacker on position to the left
        /// </summary>
        public void NextAttacker()
        {
            IPlayer attacker = GetAttacker();
            IPlayer defender = GetDefender();
            defender.TurnStatus = TurnStatus.Attacking;
            attacker.TurnStatus = TurnStatus.Defending;
            Console.WriteLine(attacker.Name + " is now " + attacker.TurnStatus);
            Console.WriteLine(defender.Name + " is now " + defender.TurnStatus);
        }

        /// <summary>
        /// ReplenishCards()
        ///         Called before a new round, deals cards to players who
        ///         have less than the minimum required cards
        /// </summary>
        bool trumpMovedtoDeck = false;
        public void ReplenishCards()
        {
            //Each player takes turns replenishing cards
            foreach (IPlayer player in Players)
            {
                // While the player still reuqires cards and the outOfCards flag is not set
                while (player.Cards.Count < MINIMUM_CARD_COUNT && !outOfCards)
                {
                    // Deck contains cards
                    if (deck.Count > 0)
                    {
                        // Remove the image from the deck CardBox when the last card is taken
                        if (deck.Count == 1) gui.Controls.Remove(gui.pbDeck);
                        player.Cards.Add(deck.DrawTopCard());

                        // Deal card to appropriate plater
                        if (player.GetType() == typeof(Player))
                        {
                            gui.DealCardToPanel(gui.pnlPlayerBottom, player.Cards.Last());
                            gui.gameStats.cardsDrawn++;
                        }
                           
                        if (player.GetType() == typeof(ComputerPlayer))
                            gui.DealCardToPanel(gui.pnlPlayerTop, player.Cards.Last());

                        // Update deck count label
                        gui.lblDeckCount.Text = deck.Count.ToString();
                        gui.Wait(150);
                        
                    }
                    else // Deck does not contain cards
                    {
                        // If, trump card has not yet been moved to the deck, move it
                        if (!trumpMovedtoDeck)
                        {
                            deck.Add(TrumpCard);
                            gui.Controls.Remove(gui.pbTrump);
                            trumpMovedtoDeck = true;
                        }
                        else
                        {
                            outOfCards = true; // Set outOfCards bool to true
                        }
                    }
                    
                }
            }
        }

        /// <summary>
        /// GetDurakRank(CardRank rank)
        ///         Gets an integer value representing a cards Durak rank
        /// </summary>
        /// <returns>An integer representing the card's Durak rank</returns>
        static public int GetDurakRank(CardRank rank)
        {
            return Array.IndexOf(DurakGame.ranks, rank);
        }

        // Initialization Methods
        void InitializeDeck()
        {
            Console.WriteLine("Initializing Deck...");

            int index = 0;
            // Iterate through each suit
            for (int suit = (int)CardSuit.Spades; suit < Enum.GetValues(typeof(CardSuit)).Length; suit++)
            {
                // Iterate through each rank used in Durak
                foreach (CardRank rank in ranks)
                {
                    // Assign each card to a deck index
                    if (index < DECK_SIZE) deck[index].Set(rank, (CardSuit)suit);
                    index++;
                }
            }
            // Shuffle the Deck
            deck.Shuffle();
            Console.WriteLine("Deck of " + index + " Cards Initialized!");
        }
        /// <summary>
        /// InitializePlayers()
        ///         Create the Human and AI players and set their names
        /// </summary>
        void InitializePlayers()
        {
            Players.Add(new Player());
            Players.Add(new ComputerPlayer());
            Players[0].Name = gui.HumanName;
            Players[0].Name = gui.AiName;
        }
        /// <summary>
        /// DealCards()
        ///         Deal cards from the deck to the player hands
        /// </summary>
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
        /// <summary>
        /// RevealTrump()
        ///         Reveal the trump card
        /// </summary>
        void RevealTrump()
        {
            trumpCard = deck.DrawTopCard();
            ((ComputerPlayer)Players[1]).TrumpSuit = TrumpCard.Suit;
            Print("Cards dealt! The trump card is the " + trumpCard.ToString() + "!");
        }
        /// <summary>
        /// SetFirstAttacker()
        ///         Sets the games' first attacker based on which
        ///         player has the lowest trump card.
        /// </summary>
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
        /// <summary>
        /// CheckForWinner()
        ///         Checks if the game has a winner
        /// </summary>
        public void CheckForWinner()
        {
            if (outOfCards)
            {
                if (Players[0].Cards.Count == 0)
                {
                    Winner = Players[0];
                    Loser = Players[1];
                }
                else if (Players[1].Cards.Count == 0)
                {
                    Winner = Players[1];
                    Loser = Players[0];
                }
                if (Winner != null)
                {
                    if (Winner.GetType() == typeof(Player))
                    {
                        gui.gameStats.gamesWon++;
                    }
                    else
                    {
                        gui.gameStats.gamesLost++;
                    }

                    gui.End(Winner, Loser);
                }
            }
        }
    }
}
