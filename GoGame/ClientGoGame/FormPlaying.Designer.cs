namespace ClientGoGame
{
    partial class FormPlaying
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
            panel1 = new Panel();
            buttonReady = new Button();
            labelSide = new Label();
            listBox1 = new ListBox();
            pictureBox1 = new PictureBox();
            buttonSkip = new Button();
            panel2 = new Panel();
            label1 = new Label();
            labelPrisonser2 = new Label();
            labelScore2 = new Label();
            pictureBox2 = new PictureBox();
            labelName2 = new Label();
            panel3 = new Panel();
            label2 = new Label();
            labelPrisonser1 = new Label();
            labelScore1 = new Label();
            labelName1 = new Label();
            labelYourTurn = new Label();
            textBox1 = new TextBox();
            buttonSend = new Button();
            menuStrip1 = new MenuStrip();
            tùyChọnToolStripMenuItem = new ToolStripMenuItem();
            chơiLạiToolStripMenuItem = new ToolStripMenuItem();
            thoátToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 224, 192);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(13, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(580, 640);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;
            // 
            // buttonReady
            // 
            buttonReady.Anchor = AnchorStyles.Bottom;
            buttonReady.BackColor = Color.White;
            buttonReady.Font = new Font("Modern No. 20", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonReady.ImageAlign = ContentAlignment.MiddleLeft;
            buttonReady.Location = new Point(358, 70);
            buttonReady.Name = "buttonReady";
            buttonReady.Size = new Size(92, 36);
            buttonReady.TabIndex = 0;
            buttonReady.Text = "Ready";
            buttonReady.UseVisualStyleBackColor = false;
            buttonReady.Click += buttonReady_Click;
            // 
            // labelSide
            // 
            labelSide.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelSide.AutoSize = true;
            labelSide.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelSide.Location = new Point(1133, 22);
            labelSide.Name = "labelSide";
            labelSide.Size = new Size(65, 28);
            labelSide.TabIndex = 1;
            labelSide.Text = "label1";
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            listBox1.Font = new Font("Times New Roman", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 22;
            listBox1.Location = new Point(769, 196);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(461, 312);
            listBox1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(11, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(57, 48);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // buttonSkip
            // 
            buttonSkip.BackColor = Color.White;
            buttonSkip.Font = new Font("Modern No. 20", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonSkip.Location = new Point(358, 20);
            buttonSkip.Name = "buttonSkip";
            buttonSkip.Size = new Size(92, 36);
            buttonSkip.TabIndex = 4;
            buttonSkip.Text = "Skip";
            buttonSkip.UseVisualStyleBackColor = false;
            buttonSkip.Click += buttonSkip_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(labelPrisonser2);
            panel2.Controls.Add(labelScore2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(labelName2);
            panel2.Location = new Point(774, 10);
            panel2.Name = "panel2";
            panel2.Size = new Size(461, 130);
            panel2.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Mongolian Baiti", 9F, FontStyle.Bold);
            label1.Location = new Point(142, 29);
            label1.Name = "label1";
            label1.Size = new Size(83, 19);
            label1.TabIndex = 10;
            label1.Text = "Prisoner:";
            // 
            // labelPrisonser2
            // 
            labelPrisonser2.AutoSize = true;
            labelPrisonser2.Font = new Font("Mongolian Baiti", 9F, FontStyle.Bold);
            labelPrisonser2.Location = new Point(231, 29);
            labelPrisonser2.Name = "labelPrisonser2";
            labelPrisonser2.Size = new Size(19, 19);
            labelPrisonser2.TabIndex = 9;
            labelPrisonser2.Text = "0";
            // 
            // labelScore2
            // 
            labelScore2.AutoSize = true;
            labelScore2.Font = new Font("Mongolian Baiti", 9F, FontStyle.Bold);
            labelScore2.Location = new Point(142, 68);
            labelScore2.Name = "labelScore2";
            labelScore2.Size = new Size(61, 19);
            labelScore2.TabIndex = 8;
            labelScore2.Text = "Score:";
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(9, 11);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(57, 48);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // labelName2
            // 
            labelName2.AutoSize = true;
            labelName2.Font = new Font("Bookman Old Style", 9F, FontStyle.Bold | FontStyle.Italic);
            labelName2.Location = new Point(9, 68);
            labelName2.Name = "labelName2";
            labelName2.Size = new Size(72, 21);
            labelName2.TabIndex = 0;
            labelName2.Text = "Friend";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(label2);
            panel3.Controls.Add(labelPrisonser1);
            panel3.Controls.Add(labelScore1);
            panel3.Controls.Add(labelName1);
            panel3.Controls.Add(pictureBox1);
            panel3.Controls.Add(buttonSkip);
            panel3.Controls.Add(buttonReady);
            panel3.Location = new Point(774, 662);
            panel3.Name = "panel3";
            panel3.Size = new Size(461, 123);
            panel3.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Mongolian Baiti", 9F, FontStyle.Bold);
            label2.Location = new Point(123, 20);
            label2.Name = "label2";
            label2.Size = new Size(83, 19);
            label2.TabIndex = 8;
            label2.Text = "Prisoner:";
            // 
            // labelPrisonser1
            // 
            labelPrisonser1.AutoSize = true;
            labelPrisonser1.Font = new Font("Mongolian Baiti", 9F, FontStyle.Bold);
            labelPrisonser1.Location = new Point(212, 20);
            labelPrisonser1.Name = "labelPrisonser1";
            labelPrisonser1.Size = new Size(19, 19);
            labelPrisonser1.TabIndex = 7;
            labelPrisonser1.Text = "0";
            // 
            // labelScore1
            // 
            labelScore1.AutoSize = true;
            labelScore1.Font = new Font("Mongolian Baiti", 9F, FontStyle.Bold);
            labelScore1.Location = new Point(126, 76);
            labelScore1.Name = "labelScore1";
            labelScore1.Size = new Size(61, 19);
            labelScore1.TabIndex = 6;
            labelScore1.Text = "Score:";
            // 
            // labelName1
            // 
            labelName1.AutoSize = true;
            labelName1.Font = new Font("Bookman Old Style", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelName1.Location = new Point(11, 76);
            labelName1.Name = "labelName1";
            labelName1.Size = new Size(45, 21);
            labelName1.TabIndex = 5;
            labelName1.Text = "You";
            // 
            // labelYourTurn
            // 
            labelYourTurn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelYourTurn.AutoSize = true;
            labelYourTurn.Font = new Font("Bauhaus 93", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            labelYourTurn.ForeColor = Color.Red;
            labelYourTurn.Location = new Point(774, 608);
            labelYourTurn.Name = "labelYourTurn";
            labelYourTurn.Size = new Size(131, 25);
            labelYourTurn.TabIndex = 7;
            labelYourTurn.Text = "YOUR TURN!";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textBox1.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(769, 547);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(343, 28);
            textBox1.TabIndex = 8;
            // 
            // buttonSend
            // 
            buttonSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSend.Font = new Font("Modern No. 20", 8.999999F);
            buttonSend.Image = Properties.Resources.icons8_send_24;
            buttonSend.ImageAlign = ContentAlignment.MiddleRight;
            buttonSend.Location = new Point(1118, 547);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(107, 28);
            buttonSend.TabIndex = 9;
            buttonSend.Text = "Send";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Click += buttonSend_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Gainsboro;
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { tùyChọnToolStripMenuItem });
            menuStrip1.Location = new Point(10, 10);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1225, 33);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // tùyChọnToolStripMenuItem
            // 
            tùyChọnToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { chơiLạiToolStripMenuItem, thoátToolStripMenuItem });
            tùyChọnToolStripMenuItem.Name = "tùyChọnToolStripMenuItem";
            tùyChọnToolStripMenuItem.Size = new Size(103, 29);
            tùyChọnToolStripMenuItem.Text = "Tùy Chọn";
            // 
            // chơiLạiToolStripMenuItem
            // 
            chơiLạiToolStripMenuItem.Name = "chơiLạiToolStripMenuItem";
            chơiLạiToolStripMenuItem.Size = new Size(270, 34);
            chơiLạiToolStripMenuItem.Text = "Chơi lại";
            chơiLạiToolStripMenuItem.Click += chơiLạiToolStripMenuItem_Click;
            // 
            // thoátToolStripMenuItem
            // 
            thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            thoátToolStripMenuItem.Size = new Size(270, 34);
            thoátToolStripMenuItem.Text = "Thoát";
            // 
            // FormPlaying
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1245, 795);
            Controls.Add(buttonSend);
            Controls.Add(textBox1);
            Controls.Add(labelYourTurn);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(labelSide);
            Controls.Add(listBox1);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            Name = "FormPlaying";
            Padding = new Padding(10);
            Text = "FormPlaying";
            FormClosing += FormPlaying_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button buttonReady;
        private Label labelSide;
        private ListBox listBox1;
        private PictureBox pictureBox1;
        private Button buttonSkip;
        private Panel panel2;
        private Panel panel3;
        private Label labelName1;
        private PictureBox pictureBox2;
        private Label labelName2;
        private Label labelScore2;
        private Label labelScore1;
        private Label labelPrisonser2;
        private Label labelPrisonser1;
        private Label labelYourTurn;
        private TextBox textBox1;
        private Button buttonSend;
        private Label label1;
        private Label label2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem tùyChọnToolStripMenuItem;
        private ToolStripMenuItem chơiLạiToolStripMenuItem;
        private ToolStripMenuItem thoátToolStripMenuItem;
    }
}