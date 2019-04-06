namespace DurakXtreme
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.pnlOpponent = new System.Windows.Forms.Panel();
            this.pnlPlayerOne = new System.Windows.Forms.Panel();
            this.lblPlayerOne = new System.Windows.Forms.Label();
            this.lblOpponent = new System.Windows.Forms.Label();
            this.pbDeck = new System.Windows.Forms.PictureBox();
            this.pnlPlayArea = new System.Windows.Forms.Panel();
            this.DurakXtremeTips = new System.Windows.Forms.ToolTip(this.components);
            this.pnlGraveyard = new System.Windows.Forms.Panel();
            this.btnTake = new System.Windows.Forms.Button();
            this.btnPass = new System.Windows.Forms.Button();
            this.lblStatusUpdate = new System.Windows.Forms.Label();
            this.lblCardCount = new System.Windows.Forms.Label();
            this.pbTrump = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrump)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpponent
            // 
            this.pnlOpponent.AllowDrop = true;
            this.pnlOpponent.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlOpponent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOpponent.Location = new System.Drawing.Point(255, 12);
            this.pnlOpponent.Name = "pnlOpponent";
            this.pnlOpponent.Size = new System.Drawing.Size(529, 134);
            this.pnlOpponent.TabIndex = 0;
            // 
            // pnlPlayerOne
            // 
            this.pnlPlayerOne.AllowDrop = true;
            this.pnlPlayerOne.BackColor = System.Drawing.Color.Orange;
            this.pnlPlayerOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayerOne.Location = new System.Drawing.Point(255, 469);
            this.pnlPlayerOne.Name = "pnlPlayerOne";
            this.pnlPlayerOne.Size = new System.Drawing.Size(529, 134);
            this.pnlPlayerOne.TabIndex = 1;
            // 
            // lblPlayerOne
            // 
            this.lblPlayerOne.AutoSize = true;
            this.lblPlayerOne.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayerOne.ForeColor = System.Drawing.Color.Snow;
            this.lblPlayerOne.Location = new System.Drawing.Point(255, 450);
            this.lblPlayerOne.Name = "lblPlayerOne";
            this.lblPlayerOne.Size = new System.Drawing.Size(45, 13);
            this.lblPlayerOne.TabIndex = 2;
            this.lblPlayerOne.Text = "Player 1";
            // 
            // lblOpponent
            // 
            this.lblOpponent.AutoSize = true;
            this.lblOpponent.BackColor = System.Drawing.Color.Transparent;
            this.lblOpponent.ForeColor = System.Drawing.Color.Snow;
            this.lblOpponent.Location = new System.Drawing.Point(748, 153);
            this.lblOpponent.Name = "lblOpponent";
            this.lblOpponent.Size = new System.Drawing.Size(45, 13);
            this.lblOpponent.TabIndex = 3;
            this.lblOpponent.Text = "Player 2";
            // 
            // pbDeck
            // 
            this.pbDeck.BackColor = System.Drawing.Color.Transparent;
            this.pbDeck.Location = new System.Drawing.Point(95, 245);
            this.pbDeck.Name = "pbDeck";
            this.pbDeck.Size = new System.Drawing.Size(75, 108);
            this.pbDeck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDeck.TabIndex = 4;
            this.pbDeck.TabStop = false;
            // 
            // pnlPlayArea
            // 
            this.pnlPlayArea.AllowDrop = true;
            this.pnlPlayArea.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayArea.Location = new System.Drawing.Point(255, 222);
            this.pnlPlayArea.Name = "pnlPlayArea";
            this.pnlPlayArea.Size = new System.Drawing.Size(529, 167);
            this.pnlPlayArea.TabIndex = 5;
            // 
            // pnlGraveyard
            // 
            this.pnlGraveyard.BackColor = System.Drawing.Color.Transparent;
            this.pnlGraveyard.Location = new System.Drawing.Point(810, 222);
            this.pnlGraveyard.Name = "pnlGraveyard";
            this.pnlGraveyard.Size = new System.Drawing.Size(119, 167);
            this.pnlGraveyard.TabIndex = 6;
            // 
            // btnTake
            // 
            this.btnTake.BackColor = System.Drawing.Color.Orange;
            this.btnTake.FlatAppearance.BorderSize = 0;
            this.btnTake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTake.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTake.Location = new System.Drawing.Point(322, 408);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(116, 30);
            this.btnTake.TabIndex = 7;
            this.btnTake.Text = "Take";
            this.btnTake.UseVisualStyleBackColor = false;
            this.btnTake.MouseEnter += new System.EventHandler(this.btnTake_MouseEnter);
            this.btnTake.MouseLeave += new System.EventHandler(this.btnTake_MouseLeave);
            // 
            // btnPass
            // 
            this.btnPass.BackColor = System.Drawing.Color.Orange;
            this.btnPass.FlatAppearance.BorderSize = 0;
            this.btnPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPass.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPass.Location = new System.Drawing.Point(580, 408);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(116, 30);
            this.btnPass.TabIndex = 8;
            this.btnPass.Text = "Pass";
            this.btnPass.UseVisualStyleBackColor = false;
            this.btnPass.MouseEnter += new System.EventHandler(this.btnPass_MouseEnter);
            this.btnPass.MouseLeave += new System.EventHandler(this.btnPass_MouseLeave);
            // 
            // lblStatusUpdate
            // 
            this.lblStatusUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusUpdate.Location = new System.Drawing.Point(255, 165);
            this.lblStatusUpdate.Name = "lblStatusUpdate";
            this.lblStatusUpdate.Size = new System.Drawing.Size(529, 42);
            this.lblStatusUpdate.TabIndex = 9;
            // 
            // lblCardCount
            // 
            this.lblCardCount.AutoSize = true;
            this.lblCardCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCardCount.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardCount.ForeColor = System.Drawing.Color.Gray;
            this.lblCardCount.Location = new System.Drawing.Point(108, 209);
            this.lblCardCount.Name = "lblCardCount";
            this.lblCardCount.Size = new System.Drawing.Size(49, 33);
            this.lblCardCount.TabIndex = 10;
            this.lblCardCount.Text = "00";
            // 
            // pbTrump
            // 
            this.pbTrump.BackColor = System.Drawing.Color.Transparent;
            this.pbTrump.Location = new System.Drawing.Point(95, 359);
            this.pbTrump.Name = "pbTrump";
            this.pbTrump.Size = new System.Drawing.Size(75, 108);
            this.pbTrump.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTrump.TabIndex = 11;
            this.pbTrump.TabStop = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(82, 536);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::DurakXtreme.Properties.Resources.texturedBackground;
            this.ClientSize = new System.Drawing.Size(962, 657);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.pbTrump);
            this.Controls.Add(this.lblCardCount);
            this.Controls.Add(this.lblStatusUpdate);
            this.Controls.Add(this.btnPass);
            this.Controls.Add(this.btnTake);
            this.Controls.Add(this.pnlGraveyard);
            this.Controls.Add(this.pnlPlayArea);
            this.Controls.Add(this.pbDeck);
            this.Controls.Add(this.lblOpponent);
            this.Controls.Add(this.lblPlayerOne);
            this.Controls.Add(this.pnlPlayerOne);
            this.Controls.Add(this.pnlOpponent);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Durak Xtreme";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDeck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrump)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlOpponent;
        private System.Windows.Forms.Panel pnlPlayerOne;
        private System.Windows.Forms.Label lblPlayerOne;
        private System.Windows.Forms.Label lblOpponent;
        private System.Windows.Forms.PictureBox pbDeck;
        private System.Windows.Forms.Panel pnlPlayArea;
        private System.Windows.Forms.ToolTip DurakXtremeTips;
        private System.Windows.Forms.Panel pnlGraveyard;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Label lblStatusUpdate;
        private System.Windows.Forms.Label lblCardCount;
        private System.Windows.Forms.PictureBox pbTrump;
        private System.Windows.Forms.Button btnReset;
    }
}

