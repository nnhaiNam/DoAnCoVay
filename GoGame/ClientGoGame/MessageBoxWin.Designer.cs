namespace ClientGoGame
{
    partial class MessageBoxWin
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
            label1 = new Label();
            buttonX = new Button();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            buttonOki = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(563, 63);
            label1.TabIndex = 0;
            label1.Text = "Thông báo";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonX
            // 
            buttonX.Font = new Font("Showcard Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonX.Location = new Point(501, 6);
            buttonX.Name = "buttonX";
            buttonX.Size = new Size(48, 47);
            buttonX.TabIndex = 1;
            buttonX.Text = "X";
            buttonX.UseVisualStyleBackColor = true;
            buttonX.Click += buttonX_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.success;
            pictureBox1.Location = new Point(57, 79);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(418, 257);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sitka Subheading", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(109, 367);
            label2.Name = "label2";
            label2.Size = new Size(305, 29);
            label2.TabIndex = 3;
            label2.Text = "Chúc mừng bạn đã chiến thắng!";
            // 
            // buttonOki
            // 
            buttonOki.Font = new Font("Stencil", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonOki.Location = new Point(213, 414);
            buttonOki.Name = "buttonOki";
            buttonOki.Size = new Size(112, 34);
            buttonOki.TabIndex = 4;
            buttonOki.Text = "OKI";
            buttonOki.UseVisualStyleBackColor = true;
            buttonOki.Click += buttonOki_Click;
            // 
            // MessageBoxWin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(563, 460);
            Controls.Add(buttonOki);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(buttonX);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MessageBoxWin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MessageBoxWin";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button buttonX;
        private PictureBox pictureBox1;
        private Label label2;
        private Button buttonOki;
    }
}