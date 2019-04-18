/* Authors: Blake, Clayton, Dylan
 * File Name: frmSettings.cs
 * 
 * Description: Controls the setings for the DurakGame
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DurakXtreme
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }
        private void frmSettings_Load(object sender, EventArgs e)
        {
            //if no file exists create one
            if (!File.Exists("./DurakConfiguration"))
            {
                File.Create("./DurakConfiguration").Dispose();
            }
            //read config file
            TextReader tr = new StreamReader("./DurakConfiguration");
            string player1_name = tr.ReadLine();
            string player2_name = tr.ReadLine();
            string aiCardsVisible = tr.ReadLine();
            tr.Close();


            //Set form controls
            if (!String.IsNullOrEmpty(player1_name))
            {
                txtPlayerOne.Text = player1_name;
            } else
            {
                txtPlayerOne.Text = "Player 1";
            }
            if (!String.IsNullOrEmpty(player2_name))
            {
                txtPlayerTwo.Text = player2_name;
            }
            else
            {
                txtPlayerTwo.Text = "Player 2";
            }
            bool isVisibleCards = false;
            if (!String.IsNullOrEmpty(aiCardsVisible))
            {
                isVisibleCards = bool.Parse(aiCardsVisible);
                cbAiCardsVisible.Checked = isVisibleCards;
            }

        
    }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //write config file
            TextWriter tw = new StreamWriter("./DurakConfiguration");
            tw.WriteLine(txtPlayerOne.Text);
            tw.WriteLine(txtPlayerTwo.Text);
            tw.WriteLine(cbAiCardsVisible.Checked);
            tw.Close();

            this.Close();
        }
    }
}
