namespace MainMenu
{
    partial class MainMenuForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(231, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(551, 207);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.DarkOrange;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Consolas", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(385, 324);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(234, 61);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.DarkOrange;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Consolas", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Location = new System.Drawing.Point(385, 404);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(234, 61);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkOrange;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Consolas", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(385, 482);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(234, 61);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MainMenu.Properties.Resources.background1;
            this.ClientSize = new System.Drawing.Size(1022, 626);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainMenuForm";
            this.Text = "Durak Xtreme";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExit;
    }
}

