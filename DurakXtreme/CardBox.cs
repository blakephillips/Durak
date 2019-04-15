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
        private const string cardsFolder = frmGameGUI.resourcesPath + "cards/resize/";
        private PlayingCard card;
        private Size cardSize;

        public PlayingCard Card { get { return card; } set { card = value; } }

        public CardBox(PlayingCard baseCard = null, bool faceDown = false)
        {
            this.card = (baseCard == null ? baseCard = new PlayingCard() : baseCard);
            this.Size = cardSize = new Size(75, 110); ;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            SetCard(card);
            
        }
        public static string GetImagePathFromCard(PlayingCard card = null, string folder = cardsFolder, string extension = ".jpg")
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
        public void SetCard(PlayingCard card = null)
        {
            if (card == null || card.IsFaceDown)
            {
                this.Image = Image.FromFile(GetImagePathFromCard());
            }
            else
            {
                this.Image = Image.FromFile(GetImagePathFromCard(card, cardsFolder));
            }
        }


        public void ToMonochrome2()
        {
            this.Image = Image.FromFile(GetImagePathFromCard(Card, cardsFolder + "bw/"));
            Update();

        }
        public void ToMonochrome()
        {
            Bitmap cbImage = (Bitmap)this.Image;
            for (int x = 0; x < cbImage.Width; x++)
            {

                for (int y = 0; y < cbImage.Height; y++)
                {
                    Color pixel = cbImage.GetPixel(x, y);
                    int colorAverage = (pixel.R + pixel.G + pixel.B) / 3;
                    if (colorAverage > 180) colorAverage -= 100;
                    Color newColor = Color.FromArgb(255, colorAverage, colorAverage, colorAverage);
                    cbImage.SetPixel(x, y, newColor); // Now greyscale
                }
            }
            this.Image = (Image)cbImage;
        }
    }
}
