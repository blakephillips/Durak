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
        //Unused pile of cards
        Deck cardPile = new Deck();

        Deck player1Hand = new Deck("Player 1");
        Deck player2Hand = new Deck("Player 2");

        public MainForm()
        {
            InitializeComponent();

            CardBox DeckCard = new CardBox();

            DeckCard.FaceUp = false;
            pbDeck.Controls.Add(DeckCard);


            InitializeDeck(ref cardPile);
            cardPile.Shuffle();

            DrawCards(ref player1Hand, ref cardPile, 6);
            DrawCards(ref player2Hand, ref cardPile, 6);

            CardBox trumpCard = new CardBox(cardPile.DrawCard());
            trumpCard.FaceUp = true;
            GameplayLog.Log("Trump Card: " + trumpCard.ToString());
            pbTrump.Controls.Add(trumpCard);
            
            lblCardCount.Text = cardPile.Count().ToString();

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
                        deck.Add(card);
                    }
                }
            }
        }

        private void DrawCards(ref Deck hand, ref Deck deck, int amount)
        {
            if (deck.Count >= amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    PlayingCard card = deck.DrawCard();
                    hand.Add(card);
                    GameplayLog.Log(card.ToString() + " has been drawn to " + hand.ToString());
                }
            }
            lblCardCount.Text = deck.Count().ToString();
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

    }
}
