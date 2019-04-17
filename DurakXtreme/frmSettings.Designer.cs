namespace DurakXtreme
{
    partial class frmSettings
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
            this.lblFirstPlayer = new System.Windows.Forms.Label();
            this.lblSecondPlayer = new System.Windows.Forms.Label();
            this.txtPlayerOne = new System.Windows.Forms.TextBox();
            this.txtPlayerTwo = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFirstPlayer
            // 
            this.lblFirstPlayer.AutoSize = true;
            this.lblFirstPlayer.Location = new System.Drawing.Point(31, 34);
            this.lblFirstPlayer.Name = "lblFirstPlayer";
            this.lblFirstPlayer.Size = new System.Drawing.Size(97, 13);
            this.lblFirstPlayer.TabIndex = 0;
            this.lblFirstPlayer.Text = "First Players Name:";
            // 
            // lblSecondPlayer
            // 
            this.lblSecondPlayer.AutoSize = true;
            this.lblSecondPlayer.Location = new System.Drawing.Point(13, 70);
            this.lblSecondPlayer.Name = "lblSecondPlayer";
            this.lblSecondPlayer.Size = new System.Drawing.Size(115, 13);
            this.lblSecondPlayer.TabIndex = 1;
            this.lblSecondPlayer.Text = "Second Players Name:";
            // 
            // txtPlayerOne
            // 
            this.txtPlayerOne.Location = new System.Drawing.Point(135, 34);
            this.txtPlayerOne.Name = "txtPlayerOne";
            this.txtPlayerOne.Size = new System.Drawing.Size(140, 20);
            this.txtPlayerOne.TabIndex = 2;
            // 
            // txtPlayerTwo
            // 
            this.txtPlayerTwo.Location = new System.Drawing.Point(134, 70);
            this.txtPlayerTwo.Name = "txtPlayerTwo";
            this.txtPlayerTwo.Size = new System.Drawing.Size(140, 20);
            this.txtPlayerTwo.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 134);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(436, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 169);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPlayerTwo);
            this.Controls.Add(this.txtPlayerOne);
            this.Controls.Add(this.lblSecondPlayer);
            this.Controls.Add(this.lblFirstPlayer);
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFirstPlayer;
        private System.Windows.Forms.Label lblSecondPlayer;
        private System.Windows.Forms.TextBox txtPlayerOne;
        private System.Windows.Forms.TextBox txtPlayerTwo;
        private System.Windows.Forms.Button btnSave;
    }
}