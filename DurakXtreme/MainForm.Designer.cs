﻿namespace DurakXtreme
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
            ((System.ComponentModel.ISupportInitialize)(this.pbDeck)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpponent
            // 
            this.pnlOpponent.AllowDrop = true;
            this.pnlOpponent.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlOpponent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOpponent.Location = new System.Drawing.Point(510, 23);
            this.pnlOpponent.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlOpponent.Name = "pnlOpponent";
            this.pnlOpponent.Size = new System.Drawing.Size(1056, 256);
            this.pnlOpponent.TabIndex = 0;
            // 
            // pnlPlayerOne
            // 
            this.pnlPlayerOne.AllowDrop = true;
            this.pnlPlayerOne.BackColor = System.Drawing.Color.Orange;
            this.pnlPlayerOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayerOne.Location = new System.Drawing.Point(510, 902);
            this.pnlPlayerOne.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlPlayerOne.Name = "pnlPlayerOne";
            this.pnlPlayerOne.Size = new System.Drawing.Size(1056, 256);
            this.pnlPlayerOne.TabIndex = 1;
            // 
            // lblPlayerOne
            // 
            this.lblPlayerOne.AutoSize = true;
            this.lblPlayerOne.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayerOne.ForeColor = System.Drawing.Color.Snow;
            this.lblPlayerOne.Location = new System.Drawing.Point(510, 865);
            this.lblPlayerOne.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPlayerOne.Name = "lblPlayerOne";
            this.lblPlayerOne.Size = new System.Drawing.Size(91, 25);
            this.lblPlayerOne.TabIndex = 2;
            this.lblPlayerOne.Text = "Player 1";
            // 
            // lblOpponent
            // 
            this.lblOpponent.AutoSize = true;
            this.lblOpponent.BackColor = System.Drawing.Color.Transparent;
            this.lblOpponent.ForeColor = System.Drawing.Color.Snow;
            this.lblOpponent.Location = new System.Drawing.Point(1496, 294);
            this.lblOpponent.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOpponent.Name = "lblOpponent";
            this.lblOpponent.Size = new System.Drawing.Size(91, 25);
            this.lblOpponent.TabIndex = 3;
            this.lblOpponent.Text = "Player 2";
            // 
            // pbDeck
            // 
            this.pbDeck.BackColor = System.Drawing.Color.Transparent;
            this.pbDeck.Location = new System.Drawing.Point(190, 471);
            this.pbDeck.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pbDeck.Name = "pbDeck";
            this.pbDeck.Size = new System.Drawing.Size(150, 208);
            this.pbDeck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDeck.TabIndex = 4;
            this.pbDeck.TabStop = false;
            // 
            // pnlPlayArea
            // 
            this.pnlPlayArea.AllowDrop = true;
            this.pnlPlayArea.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayArea.Location = new System.Drawing.Point(510, 427);
            this.pnlPlayArea.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlPlayArea.Name = "pnlPlayArea";
            this.pnlPlayArea.Size = new System.Drawing.Size(1058, 321);
            this.pnlPlayArea.TabIndex = 5;
            // 
            // pnlGraveyard
            // 
            this.pnlGraveyard.BackColor = System.Drawing.Color.Transparent;
            this.pnlGraveyard.Location = new System.Drawing.Point(1620, 427);
            this.pnlGraveyard.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlGraveyard.Name = "pnlGraveyard";
            this.pnlGraveyard.Size = new System.Drawing.Size(400, 321);
            this.pnlGraveyard.TabIndex = 6;
            // 
            // btnTake
            // 
            this.btnTake.BackColor = System.Drawing.Color.Orange;
            this.btnTake.FlatAppearance.BorderSize = 0;
            this.btnTake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTake.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTake.Location = new System.Drawing.Point(644, 785);
            this.btnTake.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(232, 58);
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
            this.btnPass.Location = new System.Drawing.Point(1160, 785);
            this.btnPass.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(232, 58);
            this.btnPass.TabIndex = 8;
            this.btnPass.Text = "Pass";
            this.btnPass.UseVisualStyleBackColor = false;
            this.btnPass.MouseEnter += new System.EventHandler(this.btnPass_MouseEnter);
            this.btnPass.MouseLeave += new System.EventHandler(this.btnPass_MouseLeave);
            // 
            // lblStatusUpdate
            // 
            this.lblStatusUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusUpdate.Location = new System.Drawing.Point(510, 317);
            this.lblStatusUpdate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStatusUpdate.Name = "lblStatusUpdate";
            this.lblStatusUpdate.Size = new System.Drawing.Size(1058, 81);
            this.lblStatusUpdate.TabIndex = 9;
            // 
            // lblCardCount
            // 
            this.lblCardCount.AutoSize = true;
            this.lblCardCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCardCount.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardCount.ForeColor = System.Drawing.Color.Gray;
            this.lblCardCount.Location = new System.Drawing.Point(216, 402);
            this.lblCardCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCardCount.Name = "lblCardCount";
            this.lblCardCount.Size = new System.Drawing.Size(98, 64);
            this.lblCardCount.TabIndex = 10;
            this.lblCardCount.Text = "00";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::DurakXtreme.Properties.Resources.texturedBackground;
            this.ClientSize = new System.Drawing.Size(2044, 1204);
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
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Durak Xtreme";
            ((System.ComponentModel.ISupportInitialize)(this.pbDeck)).EndInit();
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
    }
}

