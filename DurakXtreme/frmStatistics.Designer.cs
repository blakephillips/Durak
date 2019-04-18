namespace DurakXtreme
{
    partial class frmStatistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblGamesWon = new System.Windows.Forms.Label();
            this.lblGamesWonDisplay = new System.Windows.Forms.Label();
            this.lblGamesLost = new System.Windows.Forms.Label();
            this.lblGamesLostDisplay = new System.Windows.Forms.Label();
            this.lblAttacksWonDisplay = new System.Windows.Forms.Label();
            this.lblAttacksWon = new System.Windows.Forms.Label();
            this.lblCardsDrawnDisplay = new System.Windows.Forms.Label();
            this.lblCardsDrawn = new System.Windows.Forms.Label();
            this.lblDefensesRepelledDisplay = new System.Windows.Forms.Label();
            this.lblDefensesReppelled = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGamesWon
            // 
            this.lblGamesWon.AutoSize = true;
            this.lblGamesWon.Location = new System.Drawing.Point(12, 9);
            this.lblGamesWon.Name = "lblGamesWon";
            this.lblGamesWon.Size = new System.Drawing.Size(69, 13);
            this.lblGamesWon.TabIndex = 0;
            this.lblGamesWon.Text = "Games Won:";
            // 
            // lblGamesWonDisplay
            // 
            this.lblGamesWonDisplay.AutoSize = true;
            this.lblGamesWonDisplay.Location = new System.Drawing.Point(125, 9);
            this.lblGamesWonDisplay.Name = "lblGamesWonDisplay";
            this.lblGamesWonDisplay.Size = new System.Drawing.Size(13, 13);
            this.lblGamesWonDisplay.TabIndex = 1;
            this.lblGamesWonDisplay.Text = "0";
            // 
            // lblGamesLost
            // 
            this.lblGamesLost.AutoSize = true;
            this.lblGamesLost.Location = new System.Drawing.Point(12, 32);
            this.lblGamesLost.Name = "lblGamesLost";
            this.lblGamesLost.Size = new System.Drawing.Size(66, 13);
            this.lblGamesLost.TabIndex = 2;
            this.lblGamesLost.Text = "Games Lost:";
            // 
            // lblGamesLostDisplay
            // 
            this.lblGamesLostDisplay.AutoSize = true;
            this.lblGamesLostDisplay.Location = new System.Drawing.Point(125, 32);
            this.lblGamesLostDisplay.Name = "lblGamesLostDisplay";
            this.lblGamesLostDisplay.Size = new System.Drawing.Size(13, 13);
            this.lblGamesLostDisplay.TabIndex = 3;
            this.lblGamesLostDisplay.Text = "0";
            // 
            // lblAttacksWonDisplay
            // 
            this.lblAttacksWonDisplay.AutoSize = true;
            this.lblAttacksWonDisplay.Location = new System.Drawing.Point(125, 78);
            this.lblAttacksWonDisplay.Name = "lblAttacksWonDisplay";
            this.lblAttacksWonDisplay.Size = new System.Drawing.Size(13, 13);
            this.lblAttacksWonDisplay.TabIndex = 7;
            this.lblAttacksWonDisplay.Text = "0";
            // 
            // lblAttacksWon
            // 
            this.lblAttacksWon.AutoSize = true;
            this.lblAttacksWon.Location = new System.Drawing.Point(12, 78);
            this.lblAttacksWon.Name = "lblAttacksWon";
            this.lblAttacksWon.Size = new System.Drawing.Size(72, 13);
            this.lblAttacksWon.TabIndex = 6;
            this.lblAttacksWon.Text = "Attacks Won:";
            // 
            // lblCardsDrawnDisplay
            // 
            this.lblCardsDrawnDisplay.AutoSize = true;
            this.lblCardsDrawnDisplay.Location = new System.Drawing.Point(125, 55);
            this.lblCardsDrawnDisplay.Name = "lblCardsDrawnDisplay";
            this.lblCardsDrawnDisplay.Size = new System.Drawing.Size(13, 13);
            this.lblCardsDrawnDisplay.TabIndex = 5;
            this.lblCardsDrawnDisplay.Text = "0";
            // 
            // lblCardsDrawn
            // 
            this.lblCardsDrawn.AutoSize = true;
            this.lblCardsDrawn.Location = new System.Drawing.Point(12, 55);
            this.lblCardsDrawn.Name = "lblCardsDrawn";
            this.lblCardsDrawn.Size = new System.Drawing.Size(71, 13);
            this.lblCardsDrawn.TabIndex = 4;
            this.lblCardsDrawn.Text = "Cards Drawn:";
            // 
            // lblDefensesRepelledDisplay
            // 
            this.lblDefensesRepelledDisplay.AutoSize = true;
            this.lblDefensesRepelledDisplay.Location = new System.Drawing.Point(125, 99);
            this.lblDefensesRepelledDisplay.Name = "lblDefensesRepelledDisplay";
            this.lblDefensesRepelledDisplay.Size = new System.Drawing.Size(13, 13);
            this.lblDefensesRepelledDisplay.TabIndex = 9;
            this.lblDefensesRepelledDisplay.Text = "0";
            // 
            // lblDefensesReppelled
            // 
            this.lblDefensesReppelled.AutoSize = true;
            this.lblDefensesReppelled.Location = new System.Drawing.Point(12, 99);
            this.lblDefensesReppelled.Name = "lblDefensesReppelled";
            this.lblDefensesReppelled.Size = new System.Drawing.Size(100, 13);
            this.lblDefensesReppelled.TabIndex = 8;
            this.lblDefensesReppelled.Text = "Defenses Repelled:";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(12, 116);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(178, 23);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "&Reset Statistics";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // frmStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 143);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblDefensesRepelledDisplay);
            this.Controls.Add(this.lblDefensesReppelled);
            this.Controls.Add(this.lblAttacksWonDisplay);
            this.Controls.Add(this.lblAttacksWon);
            this.Controls.Add(this.lblCardsDrawnDisplay);
            this.Controls.Add(this.lblCardsDrawn);
            this.Controls.Add(this.lblGamesLostDisplay);
            this.Controls.Add(this.lblGamesLost);
            this.Controls.Add(this.lblGamesWonDisplay);
            this.Controls.Add(this.lblGamesWon);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStatistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistics";
            this.Load += new System.EventHandler(this.frmStatistics_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGamesWon;
        private System.Windows.Forms.Label lblGamesWonDisplay;
        private System.Windows.Forms.Label lblGamesLost;
        private System.Windows.Forms.Label lblGamesLostDisplay;
        private System.Windows.Forms.Label lblAttacksWonDisplay;
        private System.Windows.Forms.Label lblAttacksWon;
        private System.Windows.Forms.Label lblCardsDrawnDisplay;
        private System.Windows.Forms.Label lblCardsDrawn;
        private System.Windows.Forms.Label lblDefensesRepelledDisplay;
        private System.Windows.Forms.Label lblDefensesReppelled;
        private System.Windows.Forms.Button btnReset;
    }
}