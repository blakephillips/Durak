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
            txtPlayerOne.Text = "Player 1";
            txtPlayerTwo.Text = "Player 2 (AI)";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            TextWriter tw = new StreamWriter("./DurakConfiguration");
            tw.WriteLine(txtPlayerOne.Text);
            tw.WriteLine(txtPlayerTwo.Text);
            tw.WriteLine(cbAiCardsVisible.Checked);
            tw.Close();

            this.Close();
        }
    }
}
