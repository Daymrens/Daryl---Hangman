namespace Daryl___Hangman
{
    partial class Form1
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
            this.hangmanPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.hangmanPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // hangmanPictureBox
            // 
            this.hangmanPictureBox.Location = new System.Drawing.Point(172, 124);
            this.hangmanPictureBox.Name = "hangmanPictureBox";
            this.hangmanPictureBox.Size = new System.Drawing.Size(524, 436);
            this.hangmanPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.hangmanPictureBox.TabIndex = 0;
            this.hangmanPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1288, 721);
            this.Controls.Add(this.hangmanPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.hangmanPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox hangmanPictureBox;
    }
}

