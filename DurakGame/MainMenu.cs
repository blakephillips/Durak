using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DurakGame
{
    public partial class frmMainMenu : Form
    {
        //Size of the button pop on hover (added to both width/height)
        int buttonPop = 20;
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// When mouse is over, make button larger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnPop(sender as Button);
        }
        /// <summary>
        /// When the mouse leaves, make button normal size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseLeave(object sender, EventArgs e)
        {

            btnPop(sender as Button, false);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAbout_MouseEnter(object sender, EventArgs e)
        {
            btnPop(sender as Button);
           
        }

        private void btnAbout_MouseLeave(object sender, EventArgs e)
        {
            btnPop(sender as Button, false);
        }


        private void btnPlay_MouseEnter(object sender, EventArgs e)
        {
            btnPop(sender as Button);
        }

        private void btnPlay_MouseLeave(object sender, EventArgs e)
        {
            btnPop(sender as Button, false);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By: Blake, Dylan, Clayton\n Attribution:\n MainMenu: https://pixabay.com/illustrations/gambling-pattern-casino-play-1228728/ \nAccessed March 24 2019");
        }
        /// <summary>
        /// Pops a button. 
        /// </summary>
        /// <param name="btn">Button to pop</param>
        /// <param name="isEntering">True if mouse is entering, false otherwise</param>
        public void btnPop(Button btn, bool isEntering = true)
        {
            if (isEntering == true)
            {
                btn.Location = new Point(btn.Location.X - (buttonPop / 2), btn.Location.Y - (buttonPop / 2));
                btn.Size = new Size(btn.Width + buttonPop, btn.Height + buttonPop);
            }
            else
            {
                btn.Location = new Point(btn.Location.X + (buttonPop / 2), btn.Location.Y + (buttonPop / 2));
                btn.Size = new Size(btn.Width - buttonPop, btn.Height - buttonPop);
            }
        }
    }
}
