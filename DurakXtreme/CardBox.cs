using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CardLibrary;

namespace DurakXtreme
{
    public class CardBox : PictureBox
    {
        private const string CARDS_FOLDER = frmGameGUI.RESOURCES_PATH + "cards/";
        public static Size CARD_SIZE = new Size(75, 110);
        private PlayingCard card;

        public PlayingCard Card { get { return card; } set { card = value; } }


        public CardBox(PlayingCard baseCard = null, bool faceDown = false)
        {
            this.card = (baseCard == null ? baseCard = new PlayingCard() : baseCard);
            this.Size =  CARD_SIZE;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            SetCardImage((!faceDown) ? card : null);
        }
        public void FaceUp()
        {
            SetCardImage(Card);
        }
        public void FaceDown()
        {
            SetCardImage();
        }

        public static string GetImagePathFromCard(PlayingCard card = null, string folder = CARDS_FOLDER, string extension = ".png")
        {
            string path = folder;
            if (card == null)
            {
                path += "back";
            }
            else
            {
                path += card.ToString().ToLower().Replace(" ", "_");
            }
            return path + extension;
        }

        public void SetCardImage(PlayingCard card = null)
        {
            if (card == null || card.FaceDown == true)
            {
                this.Image = Image.FromFile(GetImagePathFromCard());
            }
            else
            {
                this.Image = Image.FromFile(GetImagePathFromCard(card, CARDS_FOLDER));
            }
        }

        public void SetToMonochrome()
        {
            this.Image = Image.FromFile(GetImagePathFromCard(Card, CARDS_FOLDER + "bw/"));
            Update();
        }
    }
}
