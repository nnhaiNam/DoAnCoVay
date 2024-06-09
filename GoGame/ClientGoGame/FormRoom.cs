using System.Net.Sockets;
using System.Windows.Forms;

namespace ClientGoGame
{
    public partial class FormRoom : Form
    {

        private int maxPlayingTables;
        private CheckBox[,] checkBoxGameTables;
        private TcpClient client = null;
        private StreamWriter sw;
        private StreamReader sr;
        private Service service;
        private FormPlaying formPlaying = null;

        private bool normalExit = false;
        private bool isReceiveComand = false;
        private int side = -1;
        public FormRoom()
        {
            InitializeComponent();
        }


        private void FormRoom_Load(object sender, EventArgs e)
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            textBoxName.Text = "Player" + r.Next(1, 100);
            maxPlayingTables = 0;
            textBoxLocal.ReadOnly = true;
            textBoxServer.ReadOnly = true;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 51888);
            }
            catch
            {
                MessageBox.Show("Failed to connect to the server", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            var localEndPoint = client.Client.LocalEndPoint as System.Net.IPEndPoint;
            var remoteEndPoint = client.Client.RemoteEndPoint as System.Net.IPEndPoint;


            if (localEndPoint != null)
            {

                string localAddress = localEndPoint.Address.MapToIPv4().ToString();
                int localPort = localEndPoint.Port;


                textBoxLocal.Text = $"{localAddress}:{localPort}";
            }

            if (remoteEndPoint != null)
            {

                string remoteAddress = remoteEndPoint.Address.MapToIPv4().ToString();
                int remotePort = remoteEndPoint.Port;


                textBoxServer.Text = $"{remoteAddress}:{remotePort}";
            }


            buttonConnect.Enabled = false;
            //get network stream
            NetworkStream netStream = client.GetStream();
            sr = new StreamReader(netStream, System.Text.Encoding.UTF8);
            sw = new StreamWriter(netStream, System.Text.Encoding.UTF8);
            service = new Service(listBox1, sw);
            //Get the server table information
            //Format: login, nickname
            service.SendToServer("Login," + textBoxName.Text.Trim());
            Thread threadReceice = new Thread(new ThreadStart(ReceiveData));

            //them
            threadReceice.IsBackground = true;


            threadReceice.Start();
        }

        //
        private void ReceiveData()
        {

            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = sr.ReadLine();
                }
                catch
                {
                    service.AddItemToListBox("Failed to receive data");
                }

              

                if (receiveString == null)
                {

                    return;
                }

                //
                service.AddItemToListBox("Received:" + receiveString);
                string[] splitString = receiveString.Split(',');
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "sorry":
                        MessageBox.Show("Successful connection, but the lobby is full");
                        exitWhile = true;
                        break;
                    case "tables":

                        string s = splitString[1];
                        //If maxPlayingTables is 0, it means checkBoxGameTables has not been created
                        if (maxPlayingTables == 0)
                        {
                            // count the number of tables
                            maxPlayingTables = s.Length / 2;
                            checkBoxGameTables = new CheckBox[maxPlayingTables, 2];
                            isReceiveComand = true;
                            //Add the CheckBox object to the array
                            for (int i = 0; i < maxPlayingTables; i++)
                            {
                                AddCheckBoxToPanel(s, i);
                            }
                            isReceiveComand = false;
                        }
                        else
                        {
                            isReceiveComand = true;
                            for (int i = 0; i < maxPlayingTables; i++)
                            {
                                isReceiveComand = true;
                                for (int j = 0; j < 2; j++)
                                {
                                    if (s[2 * i + j] == '0')
                                    {
                                        UpdateCheckBox(checkBoxGameTables[i, j], false);
                                    }
                                    else
                                    {
                                        UpdateCheckBox(checkBoxGameTables[i, j], true);
                                    }
                                }
                                isReceiveComand = false;
                            }
                        }
                        break;


                    case "chessinfo":
                        int tableInndex = int.Parse(splitString[1]);
                        int side = int.Parse(splitString[2]);
                        int x = int.Parse(splitString[3]);
                        int y = int.Parse(splitString[4]);
                        int anotherside = int.Parse(splitString[5]);
                        formPlaying.rawChessPieces(side, x, y, anotherside);

                        break;

                    case "sitdown":
                        int sidee = int.Parse(splitString[1]);
                        string namee = splitString[2];
                        formPlaying.SetInfo(sidee, namee);

                        formPlaying.SetMessageChat(namee + ": enter to room!");



                        break;

                    case "allready":
                        formPlaying.BeginGame();


                        break;

                    case "message":

                        string text = splitString[1];
                        formPlaying.SetMessageChat(text);
                        break;

                    case "piececapture":

                        List<(int, int)> listpiececapture = new List<(int, int)>();
                        for (int i = 1; i <= splitString.Length - 1;)
                        {
                            (int, int) tmp = (int.Parse(splitString[i]), int.Parse(splitString[i + 1]));
                            listpiececapture.Add(tmp);
                            i = i + 2;
                        }
                        string str = "";
                        foreach (var item in listpiececapture)
                        {
                            str += item.Item1.ToString() + item.Item2.ToString() + " ";
                        }


                        formPlaying.DrawAgainBroadAfterGo(listpiececapture);
                        break;

                    case "numberprison":
                        int sideinfo = int.Parse(splitString[1]);
                        int numberPrison = int.Parse(splitString[2]);
                        formPlaying.SetNumberPrison(sideinfo, numberPrison);
                        break;


                    case "score":


                        double score = double.Parse(splitString[1]);
                        formPlaying.SetScore(score);

                        break;

                    case "skip":

                        int anotherrside = int.Parse(splitString[1]);
                        formPlaying.SkipEvent(anotherrside);
                        break;

                    case "endgame":
                        formPlaying.InfoEndGame();


                        break;

                    case "infoendgame":

                        double scoreYourFriend = double.Parse(splitString[1]);
                        formPlaying.Final(scoreYourFriend);
                        break;

                    case "messagechat":
                        string name = splitString[1];
                        int index = IndexDelimiter(receiveString);
                        string message = receiveString.Substring(index + 1);
                        string messagefinal = name + " : " + message;
                        formPlaying.SetMessageChat(messagefinal);


                        break;


                    case "loop":

                        string stringBoard = splitString[1];
                        formPlaying.TransStatusBoard(stringBoard);


                        break;

                    case "playagaincomplete":
                        formPlaying.Reset();

                        break;
                    case "requireplayagain":
                        string namePlayer= splitString[1];
                        formPlaying.SetMessageChat($"PLAYER: {namePlayer} want to play again");    
                       

                        break;

                    case "escapeinbattle":
                        formPlaying.WinGameOpponentEscape();

                        break;
                }

            }

        }



        private int IndexDelimiter(string s)
        {
            char delimiter = ',';
            int count = 0;
            int index = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == delimiter)
                {
                    count++;
                    if (count == 2)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }



        // Option to add game table seats
        private void CreateCheckBox(int i, int j, string s, string text)
        {
            int x = j == 0 ? 100 : 200;
            checkBoxGameTables[i, j] = new CheckBox();
            checkBoxGameTables[i, j].Name = string.Format("check{0:0000}{1:0000}", i, j);
            checkBoxGameTables[i, j].Width = 90;
            checkBoxGameTables[i, j].Location = new Point(x, 10 + i * 30);
            checkBoxGameTables[i, j].Text = text;
            checkBoxGameTables[i, j].TextAlign = ContentAlignment.MiddleLeft;
            //MessageBox.Show(checkBoxGameTables[i, j].Name);
            if (s[2 * i + j] == '1')
            {
                //1 means someone
                checkBoxGameTables[i, j].Enabled = false;
                checkBoxGameTables[i, j].Checked = true;
            }
            else
            {
                //0 means no one
                checkBoxGameTables[i, j].Enabled = true;
                checkBoxGameTables[i, j].Checked = false;
            }
            this.panel1.Controls.Add(checkBoxGameTables[i, j]);
            checkBoxGameTables[i, j].CheckedChanged +=
                 new EventHandler(checkBox_CheckedChanged);
        }

        //Triggered when the Checked property of the CheckBox changes
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
          
            if (isReceiveComand == true)
            {
                return;
            }
            CheckBox checkbox = (CheckBox)sender;
           
            if (checkbox.Checked == true)
            {
                int i = int.Parse(checkbox.Name.Substring(5, 4));
                int j = int.Parse(checkbox.Name.Substring(9, 4));
                string name = textBoxName.Text;

                side = j;
                //Format: SitDown, Nickname, Table Number, Seat Number
                service.SendToServer(string.Format("SitDown,{0},{1}", i, j));

                formPlaying = new FormPlaying(i, j, sw);
                formPlaying.ShowDialog();


            }


        }


        delegate void Paneldelegate(string s, int i);
        //Add a game table
        private void AddCheckBoxToPanel(string s, int i)
        {
            if (panel1.InvokeRequired == true)
            {
                Paneldelegate d = AddCheckBoxToPanel;
                this.Invoke(d, s, i);
            }
            else
            {
                Label label = new Label();
                label.Location = new Point(10, 15 + i * 30);
                label.Text = string.Format("Table {0}: ", i + 1);
                label.Width = 90;
                this.panel1.Controls.Add(label);
                CreateCheckBox(i, 1, s, "White");
                CreateCheckBox(i, 0, s, "Black");

            }
        }

        delegate void CheckBoxDelegate(CheckBox checkbox, bool isChecked);
        //Modify the selection state
        private void UpdateCheckBox(CheckBox checkbox, bool isChecked)
        {
            if (checkbox.InvokeRequired == true)
            {
                CheckBoxDelegate d = UpdateCheckBox;
                this.Invoke(d, checkbox, isChecked);
            }
            else
            {
                if (side == -1)
                {
                    checkbox.Enabled = !isChecked;
                }
                else
                {
                    
                    //checkbox.Enabled = false;
                }
                checkbox.Checked = isChecked;

                if(checkbox.Checked)
                {
                    checkbox.Enabled = false;
                }
            }


        }

        private void FormRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.SendToServer("EXITNORMAL");
        }

       
    }


}
