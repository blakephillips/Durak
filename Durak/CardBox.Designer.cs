﻿namespace CardLibrary
{
    partial class CardBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbCard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCard
            // 
            this.pbCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCard.Location = new System.Drawing.Point(0, 0);
            this.pbCard.Name = "pbCard";
            this.pbCard.Size = new System.Drawing.Size(75, 107);
            this.pbCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCard.TabIndex = 0;
            this.pbCard.TabStop = false;
            this.pbCard.Click += new System.EventHandler(this.pbCard_Click);
            this.pbCard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbCard_MouseClick);
            // 
            // CardBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbCard);
            this.Name = "CardBox";
            this.Size = new System.Drawing.Size(75, 107);
            this.Load += new System.EventHandler(this.CardBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCard;
    }
}
