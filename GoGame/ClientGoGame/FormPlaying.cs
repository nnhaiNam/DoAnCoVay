using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGoGame
{
    public partial class FormPlaying : Form
    {
        private bool skipToEnd = false;
        private const int BoardSize = 10;
        private const int SquareSize = 70;
        private Service service;
        private ChessBroad chessBroad;
        private int tableIndex;
        private int side;
        private bool isTurn = false;
        private int numberOfPrisoner = 0;
        private double score = 0;
        private bool eventDelete = false;

        //check black or white
        private bool isBlack = false;
        //check loop

        private bool isLoop = false;
        private int[,] statusBoard = new int[9, 9];



        public FormPlaying(int TableIndex, int Side, StreamWriter sw)
        {
            chessBroad = new ChessBroad();
            InitializeComponent();

            this.tableIndex = TableIndex;
            this.side = Side;
            labelYourTurn.Visible = false;
            if (Side == 1)
            {
                labelSide.Text = "White";
                pictureBox1.Image = Properties.Resources.quantrangnew;
                isBlack = false;
                labelYourTurn.Enabled = false;
                
              

            }
            else
            {
                labelSide.Text = "Black";
                pictureBox1.Image = Properties.Resources.quandennew;
                isBlack = true;
                isTurn = true;
                labelYourTurn.Enabled = true;
                
            }
            service = new Service(listBox1, sw);
            InitializeGoBoard();
            //
            panel1.Visible = false;
            buttonSkip.Enabled = false;

        }
        private void InitializeGoBoard()
        {
            panel1.Width = panel1.Height = SquareSize * BoardSize;
            //this.BackColor = Color.White;
            buttonSkip.Enabled = true;
        }

        private void DrawGoBoard(Graphics g)
        {
            Pen pen = new Pen(Color.Black);

            for (int i = 0; i < BoardSize - 1; i++)
            {
                g.DrawLine(pen, SquareSize, (i + 1) * SquareSize, SquareSize * (BoardSize - 1), (i + 1) * SquareSize);
                g.DrawLine(pen, (i + 1) * SquareSize, SquareSize, (i + 1) * SquareSize, SquareSize * (BoardSize - 1));
            }

            pen.Dispose();
        }

        private void DrawCircle(Graphics g, Brush brush, int x, int y, int diameter)
        {
            g.FillEllipse(brush, x, y, diameter, diameter);
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

            DrawGoBoard(e.Graphics);
            Brush brush = Brushes.Black;
            int x1 = 210 - 10 / 2;
            int x2 = 490 - 10 / 2;
            int x3 = 350 - 10 / 2;

            DrawCircle(e.Graphics, brush, x1, x1, 10);
            DrawCircle(e.Graphics, brush, x2, x1, 10);
            DrawCircle(e.Graphics, brush, x3, x3, 10);
            DrawCircle(e.Graphics, brush, x1, x2, 10);
            DrawCircle(e.Graphics, brush, x2, x2, 10);
        }


        private void SetPositionToBroad(int x, int y, int z)
        {
            int xnew = x / 70;
            int ynew = y / 70;
            chessBroad.SetPosition(xnew, ynew, z);

        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            //trang la 2
            //den la 1



            if (!isTurn)
            {
                MessageBox.Show("Chưa đến lượt của bạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            

            int toadox = ToaDoX(e.X);
            int toadoy = ToaDoY(e.Y);

          
           
            if (e.X > 630 || e.Y > 630||e.X<70||e.Y<70)
            {
                return;
            }

            if (isSameLocation(toadox, toadoy))
            {
                MessageBox.Show("Ô này đã được đánh. Vui lòng chọn ô khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            int circleSize = 32;
            int circleX = toadox - circleSize / 2;
            int circleY = toadoy - circleSize / 2;

            if (isBlack)
            {
                SetPositionToBroad(toadox, toadoy, 1);
                var list1 = chessBroad.GetCapturedStones3(side);
                var list2 = chessBroad.GetCapturedStones2(side);


               
                if (list2.Count > 0)
                {
                    int[,] broadnew = (int[,])chessBroad.board.Clone();
                    for (int i = 0; i < list2.Count; i++)
                    {
                        broadnew[list2[i].Item1, list2[i].Item2] = 0;
                    }
                    if (AreArraysEqual(broadnew, this.statusBoard))
                    {
                        MessageBox.Show("Không được đánh tại đây! LOOP", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetPositionToBroad(toadox, toadoy, 0);
                        return;
                    }
                }



                //loop



                if (list1.Count > 0 && list2.Count == 0)
                {
                    SetPositionToBroad(toadox, toadoy, 0);
                    MessageBox.Show("Không được đánh tại đây!");
                    return;
                }
                else
                {
                    SetPositionToBroad(toadox, toadoy, 0);
                    service.SendToServer(string.Format("ChessInfo,{0},{1},{2},{3}", tableIndex, side, toadox, toadoy));
                }



            }

            else
            {

                SetPositionToBroad(toadox, toadoy, 2);


                var list1 = chessBroad.GetCapturedStones3(side);
                var list2 = chessBroad.GetCapturedStones2(side);




                if (list2.Count > 0)
                {
                    int[,] broadnew = (int[,])chessBroad.board.Clone();
                    for (int i = 0; i < list2.Count; i++)
                    {
                        broadnew[list2[i].Item1, list2[i].Item2] = 0;
                    }
                    if (AreArraysEqual(broadnew, this.statusBoard))
                    {
                        MessageBox.Show("Không được đánh tại đây! LOOP", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetPositionToBroad(toadox, toadoy, 0);
                        return;
                    }
                }

                //loop



                if (list1.Count > 0 && list2.Count == 0)
                {
                    SetPositionToBroad(toadox, toadoy, 0);
                    MessageBox.Show("Không được đánh tại đây!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    SetPositionToBroad(toadox, toadoy, 0);
                    service.SendToServer(string.Format("ChessInfo,{0},{1},{2},{3}", tableIndex, side, toadox, toadoy));
                }


            }


        }

        private bool isSameLocation(int x,int y)
        {
            int xBroad = x / 70 - 1;
            int yBroad=y / 70 - 1;
            if (this.chessBroad.board[yBroad,xBroad]!=0)
            {
                return true;
            }
            return false;
        }

        public bool AreArraysEqual(int[,] arr1, int[,] arr2)
        {
            if (arr1.GetLength(0) != arr2.GetLength(0) || arr1.GetLength(1) != arr2.GetLength(1))
                return false;

            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                for (int j = 0; j < arr1.GetLength(1); j++)
                {
                    if (arr1[i, j] != arr2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public void rawChessPieces(int side, int x, int y, int anotherside)
        {
            int[,] tmp = (int[,])chessBroad.board.Clone();

            skipToEnd = false;
            eventDelete = false;
            //side 0 black 1 white
            int toadox = ToaDoX(x);
            int toadoy = ToaDoY(y);

            //this.xFriend = y/70-1;
            //this.yFriend = x/70-1;


            int circleSize = 32;
            int circleX = toadox - circleSize / 2;
            int circleY = toadoy - circleSize / 2;

            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Size = new Size(circleSize, circleSize);
            pictureBox.Location = new Point(circleX, circleY);


            pictureBox.Paint += (s, args) =>
            {
                if (side == 0)
                {
                    args.Graphics.FillEllipse(Brushes.Black, 0, 0, circleSize, circleSize);
                    //SetPositionToBroad(toadox, toadoy, 1);

                   


                }
                else
                {
                    args.Graphics.FillEllipse(Brushes.White, 0, 0, circleSize, circleSize);
                    //SetPositionToBroad(toadox, toadoy, 2);
                    


                }
            };




            if (side == 0)
            {
                SetPositionToBroad(toadox, toadoy, 1);

            }
            else
            {
                SetPositionToBroad(toadox, toadoy, 2);
            }

            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(() =>
                {
                    panel1.Controls.Add(pictureBox);


                }));
            }

            //Check an quan
            List<(int, int)> listCapture = new List<(int, int)>();
            if (CheckThePieceIsCaptured(out listCapture) && isTurn == true)
            {

                //CO SK XOA

                eventDelete = true;
                string str = "";
                for (int i = 0; i < listCapture.Count; i++)
                {
                    if (i != listCapture.Count - 1)
                    {
                        str += listCapture[i].Item1 + "," + listCapture[i].Item2 + ",";
                    }
                    else
                    {
                        str += listCapture[i].Item1 + "," + listCapture[i].Item2;
                    }

                }
                service.SendToServer("PieceCapture" + "," + tableIndex + "," + str);


                //DAY LA TEST LOOP CO GI XOA O DAY

                //statusBoard = tmp;

                string stringboard = "";
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {

                        stringboard += tmp[i, j];
                    }
                    stringboard += " ";
                }
                stringboard = stringboard.Trim();


                service.SendToServer(string.Format("Loop,{0},{1},{2}", tableIndex, this.side, stringboard));

                // MessageBox.Show("PieceCapture" + "," + tableIndex +","+ str);
                // DrawAgainBroadAfterGo();

                //service.SendToServer(string.Format("PieceCapture"))

            }
            else
            {
                //MessageBox.Show("Chưa có gì hết!");
            }


            if (anotherside == this.side)
            {
                isTurn = true;
                if (buttonSkip.InvokeRequired)
                {
                    buttonSkip.Invoke(new MethodInvoker(() =>
                    {
                        buttonSkip.Enabled = true;
                    }));
                }

                if (labelYourTurn.InvokeRequired)
                {
                    labelYourTurn.Invoke(new MethodInvoker(() =>
                    {
                        labelYourTurn.Enabled = true;
                    }));
                }

            }
            else
            {
                isTurn = false;
                if (buttonSkip.InvokeRequired)
                {
                    buttonSkip.Invoke(new MethodInvoker(() =>
                    {
                        buttonSkip.Enabled = false;
                    }));
                }

                if (labelYourTurn.InvokeRequired)
                {
                    labelYourTurn.Invoke(new MethodInvoker(() =>
                    {
                        labelYourTurn.Enabled = false;
                    }));
                }
            }


            //Display();


            //KHONG CO SK XOA
            if (eventDelete == false)
            {
                //Display();

                if (this.side == 1)
                {
                    score = CalculateWhiteTerritory() + 6.5;
                    service.SendToServer(string.Format("Score,{0},{1},{2}", tableIndex, this.side, score));
                    if (labelScore1.InvokeRequired)
                    {
                        labelScore1.Invoke(new MethodInvoker(() =>
                        {
                            labelScore1.Text = "Score: " + score.ToString();
                        }));
                    }
                }
                else
                {
                    score = CalculateBlackTerritory();
                    service.SendToServer(string.Format("Score,{0},{1},{2}", tableIndex, this.side, score));
                    if (labelScore1.InvokeRequired)
                    {
                        labelScore1.Invoke(new MethodInvoker(() =>
                        {
                            labelScore1.Text = "Score: " + score.ToString();
                        }));
                    }
                }


                eventDelete = false;
            }



        }

        public void Display()
        {
            chessBroad.Display();
        }

        public bool CheckThePieceIsCaptured(out List<(int, int)> listCapture)
        {

            listCapture = chessBroad.GetCapturedStones2(side);
            if (listCapture.Count > 0)
            {
                return true;
            }
            return false;
        }
        public bool CheckThePieceIsCaptured2(out List<(int, int)> listCapture)
        {
            int sidee = 0;
            if (side == 0)
            {
                sidee = 1;
            }
            else
            {
                sidee = 0;
            }
            listCapture = chessBroad.GetCapturedStones2(sidee);
            if (listCapture.Count > 0)
            {
                return true;
            }
            return false;
        }



        public void BeginGame()
        {
            panel1.Invoke(new MethodInvoker(() =>
            {
                this.panel1.Visible = true;

            }));

            labelYourTurn.Invoke(new MethodInvoker(() =>
            {
                labelYourTurn.Visible = true;
            }));


        }
        private int ToaDoX(int X)
        {
            int du = X % 70;
            if (du == 0)
            {
                return X;
            }
            else
            {
                int thuong = X / 70;
                int gocX = thuong * 70;
                if (X <= gocX + 35)
                {
                    return gocX;
                }
                else
                {
                    return gocX + 70;
                }

            }


        }
        private int ToaDoY(int Y)
        {
            int du = Y % 70;
            if (du == 0)
            {
                return Y;
            }
            else
            {
                int thuong = Y / 70;
                int gocY = thuong * 70;
                if (Y <= gocY + 35)
                {
                    return gocY;
                }
                else
                {
                    return gocY + 70;
                }
            }
        }

        private void buttonReady_Click(object sender, EventArgs e)
        {
            service.SendToServer(string.Format("Start,{0},{1}", tableIndex, side));
            if (buttonReady.InvokeRequired)
            {
                buttonReady.Invoke(new MethodInvoker(() =>
                {
                    buttonReady.Enabled = false;
                }));
            }
            else
            {
                buttonReady.Enabled = false;
            }


        }


        void RemovePictureBoxAtLocation(Panel panel, Point location)
        {
            // Duyệt qua tất cả các PictureBox trong Panel và kiểm tra tọa độ
            foreach (Control control in panel.Controls)
            {
                if (control is PictureBox pictureBox && pictureBox.Bounds.Contains(location))
                {
                    if (panel.InvokeRequired)
                    {
                        panel.Invoke(new MethodInvoker(() =>
                        {
                            panel.Controls.Remove(pictureBox); // Xóa PictureBox nếu tọa độ nằm trong PictureBox
                            pictureBox.Dispose(); // Giải phóng tài nguyên
                            return; // Thoát sau khi xóa

                        }));
                    }


                }
            }
        }

        public void DrawAgainBroadAfterGo(List<(int, int)> listStone)
        {

            //Display();

            //var listStone = chessBroad.GetCapturedStones();
            int x = listStone[0].Item1;
            int y = listStone[0].Item2;
            int sideinfoo = -1;
            if (chessBroad.board[x, y] == 1)
            {
                //bi an
                sideinfoo = 0;
            }
            else
            {
                sideinfoo = 1;
            }


            int newnumber = listStone.Count;
            if (newnumber > 0)
            {
                if (side != sideinfoo)
                {
                    service.SendToServer(string.Format("NumberPrison,{0},{1},{2}", tableIndex, sideinfoo, newnumber));
                }
                //service.SendToServer(string.Format("NumberPrison,{0},{1},{2}",tableIndex,sideinfoo, newnumber));
                //MessageBox.Show(newnumber + "");
            }


            foreach (var item in listStone)
            {


                int toadox = (item.Item1 + 1) * 70;
                int toadoy = (item.Item2 + 1) * 70;

                RemovePictureBoxAtLocation(panel1, new Point(toadoy, toadox));
                chessBroad.SetPosition(item.Item2 + 1, item.Item1 + 1, 0);
            }



            if (this.side == 1)
            {
                score = CalculateWhiteTerritory() + 6.5;
                service.SendToServer(string.Format("Score,{0},{1},{2}", tableIndex, this.side, score));
                if (labelScore1.InvokeRequired)
                {
                    labelScore1.Invoke(new MethodInvoker(() =>
                    {
                        labelScore1.Text = "Score: " + score.ToString();
                    }));
                }
            }
            else
            {
                score = CalculateBlackTerritory();
                service.SendToServer(string.Format("Score,{0},{1},{2}", tableIndex, this.side, score));
                if (labelScore1.InvokeRequired)
                {
                    labelScore1.Invoke(new MethodInvoker(() =>
                    {
                        labelScore1.Text = "Score: " + score.ToString();
                    }));
                }
            }



        }

        private void buttonSkip_Click(object sender, EventArgs e)
        {
            if (skipToEnd)
            {

                if (buttonSkip.InvokeRequired)
                {
                    buttonSkip.Invoke(new MethodInvoker(() =>
                    {
                        this.buttonSkip.Enabled = false;
                    }));
                }
                else
                {
                    this.buttonSkip.Enabled = false;
                }
                service.SendToServer(string.Format("EndGame,{0}", tableIndex));
                return;
            }
            service.SendToServer(string.Format("Skip,{0},{1}", tableIndex, this.side));
            isTurn = false;
            labelYourTurn.Enabled = false;
            if (buttonSkip.InvokeRequired)
            {
                buttonSkip.Invoke(new MethodInvoker(() =>
                {
                    this.buttonSkip.Enabled = false;
                }));
            }
            else
            {
                this.buttonSkip.Enabled = false;
            }


        }


        public void SkipEvent(int another)
        {
            skipToEnd = true;
            if (this.side == another)
            {
                isTurn = true;
                if (buttonSkip.InvokeRequired)
                {
                    buttonSkip.Invoke(new MethodInvoker(() =>
                    {
                        buttonSkip.Enabled = true;
                    }));
                }
                if (labelYourTurn.InvokeRequired)
                {
                    labelYourTurn.Invoke(new MethodInvoker(() =>
                    {
                        labelYourTurn.Enabled = true;
                    }));
                }

            }


        }

        public void SetInfo(int sidee, string namee)
        {
            if (side == sidee)
            {
                if (labelName1.InvokeRequired)
                {
                    labelName1.Invoke(new MethodInvoker(() =>
                    {
                        labelName1.Text = namee;
                    }));
                }


            }
            else
            {

                if (labelName2.InvokeRequired)
                {
                    labelName2.Invoke(new MethodInvoker(() =>
                    {
                        labelName2.Text = namee;
                    }));
                }
                if (sidee == 1)
                {
                    if (pictureBox2.InvokeRequired)
                    {
                        pictureBox2.Invoke(new MethodInvoker(() =>
                        {
                            pictureBox2.Image = Properties.Resources.quantrangnew;
                        }));

                    }

                }
                else
                {
                    if (pictureBox2.InvokeRequired)
                    {
                        pictureBox2.Invoke(new MethodInvoker(() =>
                        {
                            pictureBox2.Image = Properties.Resources.quandennew;
                        }));

                    }
                }
            }
        }
        public void SetNumberPrison(int side, int number)
        {

            if (this.side != side)
            {
                numberOfPrisoner += number;
                if (labelPrisonser1.InvokeRequired)
                {
                    labelPrisonser1.Invoke(new MethodInvoker(() =>
                    {
                        this.labelPrisonser1.Text = numberOfPrisoner.ToString();
                    }));
                }

            }
            else
            {
                int friendNumberOfPrisoner = int.Parse(labelPrisonser2.Text);
                friendNumberOfPrisoner += number;
                if (labelPrisonser2.InvokeRequired)
                {
                    labelPrisonser2.Invoke(new MethodInvoker(() =>
                    {
                        this.labelPrisonser2.Text = friendNumberOfPrisoner.ToString();
                    }));
                }
            }
        }

        //Score White
        public double CalculateWhiteTerritory()
        {
            int territoryCount = 0;
            bool[,] visited = new bool[9, 9]; 

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (chessBroad.board[i, j] == 0 && !visited[i, j])
                    {
                        if (IsWhiteTerritory(i, j))
                        {
                            territoryCount++;
                        }
                    }
                }
            }

            return territoryCount;
        }
        public bool IsWhiteTerritory(int x, int y)
        {
            

            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();

            stack.Push(new Tuple<int, int>(x, y));

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                int i = current.Item1;
                int j = current.Item2;

                if (i < 0 || i >= 9 || j < 0 || j >= 9 || visited.Contains(current))
                    continue;

                visited.Add(current);

                if (chessBroad.board[i, j] == 1)
                    return false;

                if (chessBroad.board[i, j] == 0) 
                {
                   
                    stack.Push(new Tuple<int, int>(i + 1, j));
                    stack.Push(new Tuple<int, int>(i - 1, j));
                    stack.Push(new Tuple<int, int>(i, j + 1));
                    stack.Push(new Tuple<int, int>(i, j - 1));
                }
            }

            
            return true;
        }

        //Score Black
        public double CalculateBlackTerritory()
        {
            int territoryCount = 0;
            bool[,] visited = new bool[9, 9]; 

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (chessBroad.board[i, j] == 0 && !visited[i, j]) 
                    {
                        if (IsBlackTerritory(i, j)) 
                        {
                            territoryCount++;
                        }
                    }
                }
            }

            return territoryCount;
        }


        public bool IsBlackTerritory(int x, int y)
        {
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();

            stack.Push(new Tuple<int, int>(x, y));

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                int i = current.Item1;
                int j = current.Item2;

                if (i < 0 || i >= 9 || j < 0 || j >= 9 || visited.Contains(current))
                    continue;

                visited.Add(current);

                if (chessBroad.board[i, j] == 2) 
                {
                    return false;
                }

                if (chessBroad.board[i, j] == 0) 
                {
                    
                    stack.Push(new Tuple<int, int>(i + 1, j));
                    stack.Push(new Tuple<int, int>(i - 1, j));
                    stack.Push(new Tuple<int, int>(i, j + 1));
                    stack.Push(new Tuple<int, int>(i, j - 1));
                }

            }

            
            return true;
        }


        public void SetScore(double score)
        {
            if (labelScore2.InvokeRequired)
            {
                labelScore2.Invoke(new MethodInvoker(() =>
                {
                    this.labelScore2.Text = "Score: " + score.ToString();
                }));
            }

        }

        public void InfoEndGame()
        {
            double number = Convert.ToDouble(numberOfPrisoner);
            double final = this.score + number;
            service.SendToServer(string.Format("InfoEndGame,{0},{1},{2}", tableIndex, this.side, final));
        }

        public void Final(double scoreYourFriend)
        {
            double yourScore = this.score + Convert.ToDouble(this.numberOfPrisoner);
            if (scoreYourFriend > yourScore)
            {
                //MessageBox.Show("Bạn đã thua!!");
                MessageBoxLose messageBoxLose = new MessageBoxLose();
                messageBoxLose.ShowDialog();
            }
            else
            {
                //MessageBox.Show("Chúc mừng bạn đã chiến thắng!!");
                MessageBoxWin messageBoxWin = new MessageBoxWin();
                messageBoxWin.ShowDialog();
            }

            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(() =>
                {
                    panel1.Enabled = false;
                }));
            }
            else
            {
                panel1.Enabled = false;
            }

            //Close();
        }

        private void FormPlaying_FormClosing(object sender, FormClosingEventArgs e)
        {
           service.SendToServer($"EscapeInBattle,{this.tableIndex},{this.side}");
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string content = textBox1.Text;
            string name = labelName1.Text;
            if (!string.IsNullOrWhiteSpace(content))
            {
                service.SendToServer(string.Format("MessageChat,{0},{1},{2}", tableIndex, name, content));

            }
            textBox1.Text = "";
        }
        public void SetMessageChat(string s)
        {
            service.AddItemToListBox(s);
        }

        public void PrintBoard(int[,] bo, string ten)
        {
            string str = "";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    str += bo[i, j] + " ";
                }
                str += "\n";
            }
            MessageBox.Show(str, ten);
        }

        public void TransStatusBoard(string s)
        {
            var part = s.Split(' ');
            int row = 0;

            for (int i = 0; i < part.Length; i++)
            {
                for (int j = 0; j < part[i].Length; j++)
                {
                    statusBoard[row, j] = (int)char.GetNumericValue(part[i][j]);
                }
                row++;
            }

        }

        private void chơiLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.service.SendToServer(string.Format("PlayAgain,{0},{1},{2}", tableIndex,this.side,labelName1.Text.ToString()));
        }


        public void Reset()
        {
            RemoveAllStone();
            this.chessBroad.ResetBoard();
            if (side == 1)
            {
                isTurn = false;
            }
            else
            {
                isTurn = true;
            }

            this.numberOfPrisoner = 0;



            
           

            if (labelPrisonser1.InvokeRequired)
            {
                labelPrisonser1.Invoke(new MethodInvoker(() =>
                {
                    labelPrisonser1.Text = "0";
                }));
            }
            else
            {
                labelPrisonser1.Text = "0";
            }
            //
            if (labelPrisonser2.InvokeRequired)
            {
                labelPrisonser2.Invoke(new MethodInvoker(() =>
                {
                    labelPrisonser1.Text = "0";
                }));
            }
            else
            {
                labelPrisonser2.Text = "0";
            }
            //
            if (labelScore1.InvokeRequired)
            {
                labelScore1.Invoke(new MethodInvoker(() =>
                {
                    labelScore1.Text = "Score:";
                }));
            }
            else
            {
                labelScore1.Text = "Score:";
            }
            //
            if (labelScore2.InvokeRequired)
            {
                labelScore2.Invoke(new MethodInvoker(() =>
                {
                    labelScore2.Text = "Score:";
                }));
            }
            else
            {
                labelScore2.Text = "Score:";
            }
            //panel1.Invalidate();


            if(this.side==1)
            {
                if (labelYourTurn.InvokeRequired)
                {
                    labelYourTurn.Invoke(new MethodInvoker(() =>
                    {
                        labelYourTurn.Enabled = false;
                    }));
                }
            }
            else
            {
                if (labelYourTurn.InvokeRequired)
                {
                    labelYourTurn.Invoke(new MethodInvoker(() =>
                    {
                        labelYourTurn.Enabled = true;
                    }));
                }
            }
  

            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(() =>
                {
                    panel1.Enabled = true;
                }));
            }
            else
            {
                panel1.Enabled = true;
            }



        }

        private void RemoveAllStone()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.chessBroad.board[i, j] != 0)
                    {
                        RemovePictureBoxAtLocation(panel1,new Point((j+1)*70,(i+1)*70));
                    }    
                }
            }
        }


        public void WinGameOpponentEscape()
        {
            MessageBoxWin messageBoxWin = new MessageBoxWin();
            messageBoxWin.ShowDialog();            
        }


    }
}
