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
            // Read config file
            TextReader tr = new StreamReader("./DurakConfiguration");
            string player1_name = tr.ReadLine();
            string player2_name = tr.ReadLine();
            bool aiCardsVisible = bool.Parse(tr.ReadLine());
            txtPlayerOne.Text = player1_name;
            txtPlayerTwo.Text = player2_name;
            cbAiCardsVisible.Checked = aiCardsVisible;
            tr.Close();
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
