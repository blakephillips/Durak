using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardLibrary;

namespace DurakXtreme
{
    public partial class MainForm : Form
    {
        #region FIELDS AND PROPERTIES 

        /// <summary>
        /// Amount of cards to start with, and refill to if lower then threshold
        /// </summary>
        private int handAmount = 6;

        CardBox trumpCard = new CardBox();
        private const int POP = 25;

        #endregion

        //Unused pile of cards
        Deck cardPile = new Deck();
        Deck river = new Deck();

        Player player1 = new Player("Player 1");
        ComputerPlayer player2 = new ComputerPlayer("Player 2");

        public MainForm()
        {
            InitializeComponent();

            //Add event listeners for attacking/defending 
            player1.Attacking += Player_Attacking;
            player1.Defending += Player_Defending;

            player2.TakeEvent += Computer_Take;
            player2.PassEvent += Computer_Pass;

            //Initialize DeckCard image/cardbox
            CardBox BackDeckCard = new CardBox();

            //make FaceDown/Add to proper panel
            BackDeckCard.FaceUp = false;
            pbDeck.Controls.Add(BackDeckCard);

            //Initialize and shuffle standard deck
            InitializeDeck(ref cardPile);
            cardPile.Shuffle();

            //Draw the first 6 cards to each player
            player1.AddRange(DrawCards(player1, ref cardPile, handAmount));
            player2.AddRange(DrawCards(player2, ref cardPile, handAmount));
            

            //Draw and initialize trumpCard control
            trumpCard = cardPile.DrawCard().CardControl;
            trumpCard.FaceUp = true;
            GameplayLog.Log("Trump Card: " + trumpCard.ToString());
            
            pbTrump.Controls.Add(trumpCard);


            //Get P1's lowest trump card
            PlayingCard p1TrumpCard = new PlayingCard();
            bool p1TrumpCardExist = false;
            if (LowestTrumpCard(player1, trumpCard.Card, ref p1TrumpCard))
            {
               GameplayLog.Log ("P1 Trump Card: "+p1TrumpCard.ToString());
                p1TrumpCardExist = true;
            }

            //Get P2's lowest trump card
            PlayingCard p2TrumpCard = new PlayingCard();
            bool p2TrumpCardExist = false;
            if (LowestTrumpCard(player2, trumpCard.Card, ref p2TrumpCard))
            {
                GameplayLog.Log("P2 Trump Card: " + p2TrumpCard.ToString());
                p2TrumpCardExist = true;
            }
            //Decide who is attacking/defending
            if (p1TrumpCardExist == true && p2TrumpCardExist == true)
            {
                if (p1TrumpCard > p2TrumpCard)
                {
                    player1.CurrentTurnStatus = TurnStatus.Attacking;
                } else
                {
                    player1.CurrentTurnStatus = TurnStatus.Defending;
                    player2.PlayMove(ref river);
                    ReloadAllPanels();
                }
            } else
            {
                if (p1TrumpCardExist == true)
                {
                    player1.CurrentTurnStatus = TurnStatus.Attacking;
                } else
                {
                    player1.CurrentTurnStatus = TurnStatus.Defending;
                    player2.PlayMove(ref river);
                    ReloadAllPanels();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < player1.Count; i++)
            {
                
                pnlPlayerOne.Controls.Add(player1[i].CardControl);
                AlignCards(pnlPlayerOne);
                player1[i].CardControl.Click += Card_Clicked;
            }

            for (int i = 0; i < player2.Count; i++)
            {
                pnlOpponent.Controls.Add(player2[i].CardControl);

                AlignCards(pnlOpponent);
            }
        }

        //TODO: Change colours of attack/defend
        /// <summary>
        /// Event listener if the player begins attacking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_Attacking(object sender, EventArgs e)
        {
            GameplayLog.Log(sender.ToString() + " is attacking.");
            player2.CurrentTurnStatus = TurnStatus.Defending;
            btnTake.Hide();
            btnPass.Show();
        }
        
        /// <summary>
        /// Event listener if the player begins defending
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_Defending(object sender, EventArgs e)
        {
            GameplayLog.Log(sender.ToString() + " is defending.");
            player2.CurrentTurnStatus = TurnStatus.Attacking;
            btnPass.Hide();
            btnTake.Show();
        }


        private void Computer_Take(object sender, EventArgs e)
        {
            GameplayLog.Log("Computer has taken the river.");
            ReplenishCards();
            ReloadAllPanels();
        }

        private void Computer_Pass(object sender, EventArgs e)
        {
            GameplayLog.Log("Computer has passed.");
            player1.CurrentTurnStatus = TurnStatus.Attacking;
            ReplenishCards();
            ReloadAllPanels();
        }


        private void Card_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Click: " + sender.ToString());
            
            if (player1.CurrentTurnStatus == TurnStatus.Attacking)
            {
                if (river.Count == 0)
                {
                    river.RiverInsert(sender as CardBox);
                    player1.Remove((sender as CardBox).Card);
                    GameplayLog.Log(player1.ToString() + " is playing " + sender.ToString());
                    player2.PlayMove(ref river);
                    ReloadAllPanels();
                } else
                {
                    bool isPlayable = false; 
                    for (int i = 0; i < river.Count; i++)
                    {
                        if ((sender as CardBox).Card.Rank == river[i].Rank)
                        {
                            isPlayable = true;
                        } 
                    }
                    if (isPlayable == true)
                    {
                        river.RiverInsert(sender as CardBox);
                        player1.Remove((sender as CardBox).Card);
                        player2.PlayMove(ref river);
                        ReloadAllPanels();
                    } else
                    {
                        Console.WriteLine("That card is unplayable.");
                    }
                }

            } else if (player1.CurrentTurnStatus == TurnStatus.Defending)
            {
                if ((sender as CardBox).Suit == trumpCard.Suit)
                {
                    river.RiverInsert(sender as CardBox);
                    player1.Remove((sender as CardBox).Card);
                    player2.PlayMove(ref river);
                    ReloadAllPanels();
                } else
                {
                    if ((sender as CardBox).Suit == river.LastCardInputted.Suit && (sender as CardBox).Card.CardValue > river.LastCardInputted.CardValue)
                    {
                        river.RiverInsert(sender as CardBox);
                        player1.Remove((sender as CardBox).Card);
                        player2.PlayMove(ref river);
                        ReloadAllPanels();
                    } else
                    {
                        Console.WriteLine("That card is unplayable.");
                    }
                }
            }
           
        }

        /// <summary>
        /// Initialize Standard Durak Deck
        /// </summary>
        /// <param name="deck"></param>
        private void InitializeDeck(ref Deck deck)
        {
            for (int cardSuit = 0; cardSuit < 4; cardSuit++)
            {
                for (int cardRank = 1; cardRank < 14; cardRank ++)
                {
                    if (cardRank < 2 || cardRank > 5)
                    {
                        PlayingCard card = new PlayingCard((CardRank)cardRank, (CardSuit)cardSuit);
                        if (card.Rank == CardRank.Ace)
                            card.CardValue = 15;
                        deck.Add(card);
                    }
                }
            }
        }



      
        /// <summary>
        /// Draws a specified amount of cards from the deck and puts them into the players hand
        /// </summary>
        /// <param name="player">Player object the cards are to be placed in</param>
        /// <param name="deck">Deck to withdraw the cards from</param>
        /// <param name="amount">Amount to take and put in players hand</param>
        private Deck DrawCards(Player player, ref Deck deck, int amount)
        {
            Deck cards = new Deck();
            if (deck.Count >= amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    PlayingCard card = deck.DrawCard();
                    cards.Add(card);
                    GameplayLog.Log(card.ToString() + " has been drawn to " + player.ToString());
                }
            } else if (deck.Count != 0)
            {
                for (int i = 0; i < deck.Count; i++)
                {
                    PlayingCard card = deck.DrawCard();
                    cards.Add(card);
                    GameplayLog.Log(card.ToString() + " has been drawn to " + player.ToString());
                }
            }
            lblCardCount.Text = deck.Count().ToString();
            return cards;
        }

        /// <summary>
        /// Returns the lowest trump card in a players hand
        /// </summary>
        /// <param name="player">Player object that has PlayingCards in its list</param>
        /// <param name="trumpCard">TrumpCard for the game</param>
        /// <param name="outCard">Reference to the card the lowest trumpCard will be stored in</param>
        /// <returns>Returns true if there is any trump cards in the hand, false otherwise</returns>
        private bool LowestTrumpCard(Player player, PlayingCard trumpCard, ref PlayingCard outCard)
        {
            bool cardFound = false;

            for (int i = 0; i < player.Count; i++)
            {
                if (player[i].Suit == trumpCard.Suit)
                {
                    if (cardFound == false)
                    {
                        outCard = player[i];
                    }
                    else if (outCard.Rank > player[i].Rank)
                    {
                        outCard = player[i];
                    }
                    cardFound = true;
                }
            }
            return cardFound;
        }

       
        private void btnReset_Click(object sender, EventArgs e)
        {

        }
        #region "UI Effects"
        private void btnTake_MouseEnter(object sender, EventArgs e)
        {
            UIEffects.btnPop(sender as Button);
        }

        private void btnTake_MouseLeave(object sender, EventArgs e)
        {
            UIEffects.btnPop(sender as Button, false);
        }

        private void btnPass_MouseEnter(object sender, EventArgs e)
        {
            UIEffects.btnPop(sender as Button);
        }

        private void btnPass_MouseLeave(object sender, EventArgs e)
        {
            UIEffects.btnPop(sender as Button, false);
        }
        #endregion

        #region Helper Methods

        public void ReloadAllPanels()
        {
            AddAlignCards(player1, pnlPlayerOne);
            AddAlignCards(player2, pnlOpponent);
            AddAlignCards(river, pnlPlayArea);
        }

        private void ReplenishCards()
        {
            if (player1.Count < handAmount && cardPile.Count != 0)
            {
                player1.AddRange(DrawCards(player1, ref cardPile, (handAmount - player1.Count)));

            }
            if (player2.Count < handAmount && cardPile.Count != 0)
            {
                player2.AddRange(DrawCards(player2, ref cardPile, (handAmount - player2.Count)));
            }
        }

        private void AddAlignCards(Deck deck, Panel panel)
        {
            panel.Controls.Clear();
            for (int i = 0; i < deck.Count; i++)
            {
                if (panel == pnlPlayerOne)
                {
                    //make sure each card has click event listener.
                    //just in case it is a new card
                    deck[i].CardControl.Click -= Card_Clicked;
                    deck[i].CardControl.Click += Card_Clicked;
                } else
                {
                    //if control isn't in players hand, it isn't clickable
                    deck[i].CardControl.Click -= Card_Clicked;
                }
                    
                panel.Controls.Add(deck[i].CardControl);
            }
            AlignCards(panel);

        }


        private void AlignCards(Panel panel)
        {
            // Storing the number of cards/CardBoxes in the panel
            int cardCount = panel.Controls.Count;

            // Check if card boxes are in the panel
            if (cardCount > 0)
            {
                // Determining the width of an existing card box object in the panel
                int cardBoxWidth = panel.Controls[0].Width;

                int startPoint = (panel.Width - cardBoxWidth) / 2;

                int offset = 0;

                if (cardCount > 1)
                {
                    offset = (panel.Width - cardBoxWidth - 2 * POP) / (cardCount - 1);

                    if (offset > cardBoxWidth)
                        offset = cardBoxWidth;

                    int totalCardBoxWidth = (cardCount - 1) * offset + cardBoxWidth;

                    startPoint = (panel.Width - totalCardBoxWidth) / 2;
                }

                panel.Controls[cardCount - 1].Top = POP;
                System.Diagnostics.Debug.Write(panel.Controls[cardCount - 1].Top.ToString() + "\n");
                panel.Controls[cardCount - 1].Left = startPoint;

                for (int index = cardCount - 2; index >= 0; index--)
                {
                    panel.Controls[index].Top = POP;
                    panel.Controls[index].Left = panel.Controls[index + 1].Left + offset;
                }
            }

        }






        #endregion

        private void btnTake_Click(object sender, EventArgs e)
        {
            player1.Take(ref river);
            player2.PlayMove(ref river);
            ReplenishCards();
            ReloadAllPanels();
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            player1.Pass(ref river);
            player1.CurrentTurnStatus = TurnStatus.Defending;
            player2.PlayMove(ref river);
            ReplenishCards();
            ReloadAllPanels();
        }
    }
}
