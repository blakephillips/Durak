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
    public partial class frmMainMenu : Form
    {
        //Size of the button pop on hover (added to both width/height)
        int buttonPop = 20;

        public string player1Name = "Player 1";
        public string player2Name = "Player 2(AI)";

        

        static public frmGameGUI gameForm;
        static public frmSettings formSettings;
        static public frmStatistics formStatistics;
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
           UIEffects.btnPop(sender as Button);
        }
        /// <summary>
        /// When the mouse leaves, make button normal size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseLeave(object sender, EventArgs e)
        {

           UIEffects.btnPop(sender as Button, false);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAbout_MouseEnter(object sender, EventArgs e)
        {
           UIEffects.btnPop(sender as Button);
           
        }

        private void btnAbout_MouseLeave(object sender, EventArgs e)
        {
           UIEffects.btnPop(sender as Button, false);
        }


        private void btnPlay_MouseEnter(object sender, EventArgs e)
        {
           UIEffects.btnPop(sender as Button);
        }

        private void btnPlay_MouseLeave(object sender, EventArgs e)
        {
            UIEffects.btnPop(sender as Button, false);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By: Blake, Dylan, Clayton\n Attribution:\n MainMenu: https://pixabay.com/illustrations/gambling-pattern-casino-play-1228728/ \nAccessed March 24 2019");
        }

        /// <summary>
        /// btnPlay_Click - Closes the main menu form and opens the Durak game UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.Hide();
            gameForm = new frmGameGUI(this);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            formSettings = new frmSettings(this);
            formSettings.ShowDialog();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            formStatistics = new frmStatistics();
            formStatistics.ShowDialog();
        }
    }
}
