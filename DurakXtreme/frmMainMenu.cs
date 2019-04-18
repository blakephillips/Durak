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
        static public frmGameGUI gameForm;
        static public frmSettings formSettings;
        static public frmStatistics formStatistics;
        public frmMainMenu()
        {
            InitializeComponent();
        }
        /// <summary>
        /// When mouse is over a button, make it larger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseEnter(object sender, EventArgs e)
        {
            UIEffects.controlPop(sender as Control);
        }
        /// <summary>
        /// When mouse leaves a button, make it smaller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            UIEffects.controlPop(sender as Control, false);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By: Blake, Dylan, Clayton\n Attribution:\n MainMenu: https://pixabay.com/illustrations/gambling-pattern-casino-play-1228728/ \nAccessed March 24 2019");
        }
        /// <summary>
        /// btnPlay_Click - Hides menu and opens the Durak game UI
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
            formSettings = new frmSettings();
            formSettings.ShowDialog();
        }
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            formStatistics = new frmStatistics();
            formStatistics.ShowDialog();
        }
    }
}
