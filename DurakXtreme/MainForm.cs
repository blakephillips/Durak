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
        

        private const int POP = 25;

        #endregion

        //Unused pile of cards
        Deck cardPile = new Deck();

        Player player1 = new Player("Player 1");
        Player player2 = new Player("Player 2");

        public MainForm()
        {
            InitializeComponent();

            //Add event listeners for attacking/defending 
            player1.Attacking += Player_Attacking;
            player1.Defending += Player_Defending;

            //Initialize DeckCard image/cardbox
            CardBox BackDeckCard = new CardBox();

            //make FaceDown/Add to proper panel
            BackDeckCard.FaceUp = false;
            pbDeck.Controls.Add(BackDeckCard);

            //Initialize and shuffle standard deck
            InitializeDeck(ref cardPile);
            cardPile.Shuffle();

            //Draw the first 6 cards to each player
            DrawCards(ref player1, ref cardPile, handAmount);
            DrawCards(ref player2, ref cardPile, handAmount);

            //Draw and initialize trumpCard control
            CardBox trumpCard = cardPile.DrawCard().CardControl;
            trumpCard.FaceUp = true;
            GameplayLog.Log("Trump Card: " + trumpCard.ToString());
            
            pbTrump.Controls.Add(trumpCard);


            //Get P1's lowest trump card
            PlayingCard p1TrumpCard = new PlayingCard();
            bool p1TrumpCardExist = false;
            if (LowestTrumpCard(ref player1, trumpCard.Card, ref p1TrumpCard))
            {
               GameplayLog.Log ("P1 Trump Card: "+p1TrumpCard.ToString());
                p1TrumpCardExist = true;
            }

            //Get P2's lowest trump card
            PlayingCard p2TrumpCard = new PlayingCard();
            bool p2TrumpCardExist = false;
            if (LowestTrumpCard(ref player2, trumpCard.Card, ref p2TrumpCard))
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
                }
            } else
            {
                if (p1TrumpCardExist == true)
                {
                    player1.CurrentTurnStatus = TurnStatus.Attacking;
                } else
                {
                    player1.CurrentTurnStatus = TurnStatus.Defending;
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

        private void Card_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Click: " + sender.ToString());
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
        private void DrawCards(ref Player player, ref Deck deck, int amount)
        {

            if (deck.Count >= amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    PlayingCard card = deck.DrawCard();
                    player.Add(card);
                    GameplayLog.Log(card.ToString() + " has been drawn to " + player.ToString());
                }
            }
            lblCardCount.Text = deck.Count().ToString();
        }

        /// <summary>
        /// Returns the lowest trump card in a players hand
        /// </summary>
        /// <param name="player">Player object that has PlayingCards in its list</param>
        /// <param name="trumpCard">TrumpCard for the game</param>
        /// <param name="outCard">Reference to the card the lowest trumpCard will be stored in</param>
        /// <returns>Returns true if there is any trump cards in the hand, false otherwise</returns>
        private bool LowestTrumpCard(ref Player player, PlayingCard trumpCard, ref PlayingCard outCard)
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


        private void AlignCards(Panel playerPanel)
        {
            // Storing the number of cards/CardBoxes in the panel
            int cardCount = playerPanel.Controls.Count;

            // Check if card boxes are in the panel
            if (cardCount > 0)
            {
                // Determining the width of an existing card box object in the panel
                int cardBoxWidth = playerPanel.Controls[0].Width;

                int startPoint = (playerPanel.Width - cardBoxWidth) / 2;

                int offset = 0;

                if (cardCount > 1)
                {
                    offset = (playerPanel.Width - cardBoxWidth - 2 * POP) / (cardCount - 1);

                    if (offset > cardBoxWidth)
                        offset = cardBoxWidth;

                    int totalCardBoxWidth = (cardCount - 1) * offset + cardBoxWidth;

                    startPoint = (playerPanel.Width - totalCardBoxWidth) / 2;
                }

                playerPanel.Controls[cardCount - 1].Top = POP;
                System.Diagnostics.Debug.Write(playerPanel.Controls[cardCount - 1].Top.ToString() + "\n");
                playerPanel.Controls[cardCount - 1].Left = startPoint;

                for (int index = cardCount - 2; index >= 0; index--)
                {
                    playerPanel.Controls[index].Top = POP;
                    playerPanel.Controls[index].Left = playerPanel.Controls[index + 1].Left + offset;
                }
            }

        }






        #endregion
    }
}
