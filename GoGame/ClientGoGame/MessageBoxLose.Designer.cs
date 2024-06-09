namespace ClientGoGame
{
    partial class MessageBoxLose
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
            pictureBox1 = new PictureBox();
            label2 = new Label();
            buttonOki = new Button();
            buttonX = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Showcard Gothic", 12F);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(541, 38);
            label1.TabIndex = 0;
            label1.Text = "Thông báo";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.lose;
            pictureBox1.Location = new Point(30, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(483, 280);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sitka Subheading", 10F, FontStyle.Bold | FontStyle.Italic);
            label2.Location = new Point(199, 347);
            label2.Name = "label2";
            label2.Size = new Size(139, 29);
            label2.TabIndex = 2;
            label2.Text = "Bạn đã thua!!";
            // 
            // buttonOki
            // 
            buttonOki.Font = new Font("Stencil", 9F);
            buttonOki.Location = new Point(213, 401);
            buttonOki.Name = "buttonOki";
            buttonOki.Size = new Size(112, 34);
            buttonOki.TabIndex = 3;
            buttonOki.Text = "OKI";
            buttonOki.UseVisualStyleBackColor = true;
            buttonOki.Click += buttonOki_Click;
            // 
            // buttonX
            // 
            buttonX.Font = new Font("Showcard Gothic", 9F, FontStyle.Bold);
            buttonX.Location = new Point(488, 5);
            buttonX.Name = "buttonX";
            buttonX.Size = new Size(41, 39);
            buttonX.TabIndex = 4;
            buttonX.Text = "X";
            buttonX.UseVisualStyleBackColor = true;
            buttonX.Click += buttonX_Click;
            // 
            // MessageBoxLose
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 447);
            Controls.Add(buttonX);
            Controls.Add(buttonOki);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MessageBoxLose";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MessageBoxLose";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Button buttonOki;
        private Button buttonX;
    }
}