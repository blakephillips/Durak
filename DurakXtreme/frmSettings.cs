using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DurakXtreme
{
    public partial class frmSettings : Form
    {
        frmMainMenu menu;
        public frmSettings(frmMainMenu mainMenu)
        {
            InitializeComponent();
            menu = mainMenu;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            txtPlayerOne.Text = menu.player1Name;
            txtPlayerTwo.Text = menu.player2Name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            menu.player1Name = txtPlayerOne.Text;
            menu.player2Name = txtPlayerTwo.Text;

            this.Close();
        }
    }
}
