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
    public partial class frmStatistics : Form
    {
        public frmStatistics()
        {
            InitializeComponent();
        }
        GameStatistics gameStats = new GameStatistics();
        private void frmStatistics_Load(object sender, EventArgs e)
        {
            GetStatistics();
        }

        private void GetStatistics()
        {
            gameStats.InitializeStatistics();

            lblAttacksWonDisplay.Text = gameStats.attacksWon.ToString();
            lblCardsDrawnDisplay.Text = gameStats.cardsDrawn.ToString();
            lblDefensesRepelledDisplay.Text = gameStats.defensesRepelled.ToString();
            lblGamesWonDisplay.Text = gameStats.gamesWon.ToString();
            lblGamesLostDisplay.Text = gameStats.gamesLost.ToString();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            gameStats.ResetStatistics();
            GetStatistics();
        }
    }
}
