namespace DurakXtreme
{
    partial class frmGameGUI
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
            this.pbTrump = new System.Windows.Forms.PictureBox();
            this.pbDeck = new System.Windows.Forms.PictureBox();
            this.pnlPlayArea = new System.Windows.Forms.Panel();
            this.pbDiscard = new System.Windows.Forms.PictureBox();
            this.pnlPlayerTop = new System.Windows.Forms.Panel();
            this.pnlPlayerBottom = new System.Windows.Forms.Panel();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.btnTakePass = new System.Windows.Forms.Button();
            this.lblDeckCount = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrump)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDiscard)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTrump
            // 
            this.pbTrump.BackColor = System.Drawing.Color.Transparent;
            this.pbTrump.Location = new System.Drawing.Point(14, 200);
            this.pbTrump.Name = "pbTrump";
            this.pbTrump.Size = new System.Drawing.Size(75, 110);
            this.pbTrump.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTrump.TabIndex = 13;
            this.pbTrump.TabStop = false;
            // 
            // pbDeck
            // 
            this.pbDeck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(32)))));
            this.pbDeck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbDeck.Location = new System.Drawing.Point(14, 150);
            this.pbDeck.Name = "pbDeck";
            this.pbDeck.Size = new System.Drawing.Size(75, 110);
            this.pbDeck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDeck.TabIndex = 12;
            this.pbDeck.TabStop = false;
            // 
            // pnlPlayArea
            // 
            this.pnlPlayArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(12)))), ((int)(((byte)(27)))));
            this.pnlPlayArea.Location = new System.Drawing.Point(101, 150);
            this.pnlPlayArea.Name = "pnlPlayArea";
            this.pnlPlayArea.Size = new System.Drawing.Size(669, 160);
            this.pnlPlayArea.TabIndex = 14;
            // 
            // pbDiscard
            // 
            this.pbDiscard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(32)))));
            this.pbDiscard.Location = new System.Drawing.Point(781, 176);
            this.pbDiscard.Name = "pbDiscard";
            this.pbDiscard.Size = new System.Drawing.Size(75, 110);
            this.pbDiscard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDiscard.TabIndex = 15;
            this.pbDiscard.TabStop = false;
            // 
            // pnlPlayerTop
            // 
            this.pnlPlayerTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayerTop.Enabled = false;
            this.pnlPlayerTop.Location = new System.Drawing.Point(101, 6);
            this.pnlPlayerTop.Name = "pnlPlayerTop";
            this.pnlPlayerTop.Size = new System.Drawing.Size(669, 140);
            this.pnlPlayerTop.TabIndex = 15;
            // 
            // pnlPlayerBottom
            // 
            this.pnlPlayerBottom.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayerBottom.Location = new System.Drawing.Point(101, 314);
            this.pnlPlayerBottom.Name = "pnlPlayerBottom";
            this.pnlPlayerBottom.Size = new System.Drawing.Size(669, 140);
            this.pnlPlayerBottom.TabIndex = 16;
            // 
            // txtMessages
            // 
            this.txtMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(32)))));
            this.txtMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessages.ForeColor = System.Drawing.Color.White;
            this.txtMessages.Location = new System.Drawing.Point(0, 487);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.Size = new System.Drawing.Size(868, 115);
            this.txtMessages.TabIndex = 17;
            this.txtMessages.TabStop = false;
            // 
            // btnTakePass
            // 
            this.btnTakePass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(18)))), ((int)(((byte)(29)))));
            this.btnTakePass.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTakePass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.btnTakePass.Location = new System.Drawing.Point(781, 355);
            this.btnTakePass.Name = "btnTakePass";
            this.btnTakePass.Size = new System.Drawing.Size(75, 54);
            this.btnTakePass.TabIndex = 18;
            this.btnTakePass.Text = "&Take";
            this.btnTakePass.UseVisualStyleBackColor = false;
            this.btnTakePass.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // lblDeckCount
            // 
            this.lblDeckCount.BackColor = System.Drawing.Color.Transparent;
            this.lblDeckCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckCount.ForeColor = System.Drawing.Color.White;
            this.lblDeckCount.Location = new System.Drawing.Point(14, 112);
            this.lblDeckCount.Name = "lblDeckCount";
            this.lblDeckCount.Size = new System.Drawing.Size(75, 23);
            this.lblDeckCount.TabIndex = 19;
            this.lblDeckCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblStatus.Location = new System.Drawing.Point(0, 462);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(868, 25);
            this.lblStatus.TabIndex = 21;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmGameGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DurakXtreme.Properties.Resources.texturedBackground;
            this.ClientSize = new System.Drawing.Size(868, 602);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDeckCount);
            this.Controls.Add(this.btnTakePass);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.pnlPlayerBottom);
            this.Controls.Add(this.pnlPlayerTop);
            this.Controls.Add(this.pbDiscard);
            this.Controls.Add(this.pnlPlayArea);
            this.Controls.Add(this.pbDeck);
            this.Controls.Add(this.pbTrump);
            this.DoubleBuffered = true;
            this.Name = "frmGameGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm2";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pbTrump)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDiscard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pbTrump;
        public System.Windows.Forms.PictureBox pbDeck;
        private System.Windows.Forms.Panel pnlPlayArea;
        public System.Windows.Forms.Panel pnlPlayerTop;
        private System.Windows.Forms.TextBox txtMessages;
        public System.Windows.Forms.Panel pnlPlayerBottom;
        private System.Windows.Forms.Button btnTakePass;
        public System.Windows.Forms.Label lblDeckCount;
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.PictureBox pbDiscard;
    }
}