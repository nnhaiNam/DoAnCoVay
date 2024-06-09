using System.Net;
using System.Net.Sockets;
using System.Xml;

namespace ServerGoGame
{
    public partial class FormServer : Form
    {

        private int maxUsers;
        private List<User> userList = new List<User>();
        private int maxTables;
        private GameTable[] gameTables;
        private IPAddress localAddress;
        private int port = 51888;
        private TcpListener myListener;
        private Service service;

        public FormServer()
        {
            InitializeComponent();
            service = new Service(listBox1);
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            listBox1.HorizontalScrollbar = true;
            localAddress = IPAddress.Parse("127.0.0.1");
            buttonStop.Enabled = false;
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxMaxTables.Text, out maxTables) == false ||
                int.TryParse(textBoxMaxUsers.Text, out maxUsers) == false)
            {

                MessageBox.Show("Please enter a positive integer in the range!!!");
                return;

            }

            if (maxUsers < 1 || maxUsers > 300)
            {
                MessageBox.Show("The number of people allowed is 1-300!!!");
                return;
            }
            if (maxTables < 1 || maxTables > 100)
            {
                MessageBox.Show("The number of tables allowed is 1-100!!!");
                return;
            }

            textBoxMaxTables.Enabled = false;
            textBoxMaxUsers.Enabled = false;

            //Create an array of game tables
            gameTables = new GameTable[maxTables];
            for (int i = 0; i < maxTables; i++)
            {
                gameTables[i] = new GameTable(listBox1);
            }

            //monitor
            myListener = new TcpListener(localAddress, port);
            myListener.Start();
            service.AddItem(string.Format("Start listening for client connections at {0}:{1}", localAddress, port));

            //Create a thread to listen for client connection requests
            Thread myThread = new Thread(new ThreadStart(ListenClientConnect));

            myThread.IsBackground= true;

            myThread.Start();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;

        }

        public void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newClient = null;
                try
                {
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                //create a thread for each client
                Thread threadReceive = new Thread(ReceiveData);

                //test
                threadReceive.IsBackground = true;
                //test

                User user = new User(newClient);
                threadReceive.Start(user);
                userList.Add(user);
                service.AddItem(string.Format("{0}Enter", newClient.Client.RemoteEndPoint));
                service.AddItem(string.Format("Number of currently connected users: {0}", userList.Count));
            }


        }
        public void ReceiveData(object obj)
        {
            
            User user = (User)obj;
            TcpClient client = user.client;
            bool normalExit = false;
            bool exitWhile = false;

            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = user.sr.ReadLine();
                }
                catch
                {
                    service.AddItem("Failed to receive data!");
                    
                }
                //
                //
                if(receiveString == null)
                {
                    return;
                }

                service.AddItem(string.Format("from {0}:{1}", user.userName, receiveString));
                string[] splitString = receiveString.Split(',');
                int tableIndex = -1;
                int side = -1;
                int anotherSide = -1;
                string sendString = "";
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "login":
                        if (userList.Count > maxUsers)
                        {
                            sendString = "Sorry";
                            service.SendToOne(user, sendString);
                            service.AddItem("The number of people is full, refuse" + splitString[1] + "Enter the game room");
                            exitWhile = true;
                        }
                        else
                        {
                            user.userName = string.Format("[{0}]", splitString[1]);
                            sendString = "Tables," + this.GetOnlineString();
                            service.SendToOne(user, sendString);
                        }

                        break;
                    case "sitdown":
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        gameTables[tableIndex].gamePlayer[side].user = user;
                        gameTables[tableIndex].gamePlayer[side].someone = true;
                        gameTables[tableIndex].gamePlayer[side].isplaying = true;
                        service.AddItem(string.Format("{0} is seated at table {1}, seat {2}", user.userName,
                                                        tableIndex + 1, side + 1));
                        //Get the seat number of the other party
                        anotherSide = (side + 1) % 2;
                        // Determine if the other party is someone
                        if (gameTables[tableIndex].gamePlayer[anotherSide].someone == true)
                        {
                            // Tell the user that the other party is seated
                            //Format: SitDown, seat number, username
                            sendString = string.Format("SitDown,{0},{1}", anotherSide,
                                            gameTables[tableIndex].gamePlayer[anotherSide].user.userName);
                            service.SendToOne(user, sendString);
                        }
                        //Tell both users that the user is seated
                        //Format: SitDown, seat number, username

                        sendString = string.Format("SitDown,{0},{1}", side, user.userName);
                        service.SentToBoth(gameTables[tableIndex], sendString);


                        //fix
                        //Send the status of each table in the game room to all users
                        service.SendToAll(userList, "Tables," + this.GetOnlineString());

                        break;

                    case "chessinfo":
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        int x = int.Parse(splitString[3]);
                        int y = int.Parse(splitString[4]);
                        anotherSide = (side + 1) % 2;
                        sendString = string.Format("ChessInfo,{0},{1},{2},{3},{4}", tableIndex, side, x, y, anotherSide);
                       
                        service.SentToBoth(gameTables[tableIndex], sendString);

                        break;

                    //Prepare, format: Start, table number, seat number
                    case "start":
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        gameTables[tableIndex].gamePlayer[side].started = true;
                        if (side == 0)
                        {
                            anotherSide = 1;
                            sendString = "Message,Black is ready";
                        }
                        else
                        {
                            anotherSide = 0;
                            sendString = "Message,White is ready";
                        }
                        service.SentToBoth(gameTables[tableIndex], sendString);
                        if (gameTables[tableIndex].gamePlayer[anotherSide].started == true)
                        {
                            sendString = "AllReady";
                            service.SentToBoth(gameTables[tableIndex], sendString);
                        }
                        break;

                    case "piececapture":
                        tableIndex = int.Parse(splitString[1]);

                        int indexfinal = splitString.Length - 1;
                        sendString += "piececapture,";
                        for (int i = 2; i < splitString.Length; i++)
                        {
                            if (i != splitString.Length - 1)
                            {
                                sendString += splitString[i] + ",";
                            }
                            else
                            {
                                sendString += splitString[i];
                            }

                        }
                        
                        service.SentToBoth(gameTables[tableIndex], sendString);

                        break;

                    case "numberprison":

                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        anotherSide = (side + 1) % 2;
                        int numberPrison = int.Parse(splitString[3]);
                        sendString = string.Format("NumberPrison,{0},{1}", side, numberPrison);
                        //MessageBox.Show("Server: "+numberPrison+" "+side);
                        service.SentToBoth(gameTables[tableIndex], sendString);
                        //service.SendToOne(user, sendString);
                        break;

                    case "score":
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        anotherSide = (side + 1) % 2;
                        double score = double.Parse(splitString[3]);
                        sendString = string.Format("Score,{0}", score);
                        //service.SentToBoth(gameTables[tableIndex], sendString);
                        User anotherUser = gameTables[tableIndex].gamePlayer[anotherSide].user;
                        service.SendToOne(anotherUser, sendString);

                        break;

                    case "skip":
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        anotherSide = (side + 1) % 2;
                        User anotherUserr = gameTables[tableIndex].gamePlayer[anotherSide].user;
                        sendString = string.Format("Skip,{0}", anotherSide);
                        service.SendToOne(anotherUserr, sendString);

                        break;

                    case "endgame":
                        tableIndex = int.Parse(splitString[1]);
                        sendString = string.Format("EndGame");
                        service.SentToBoth(gameTables[tableIndex], sendString);
                        break;

                    case "infoendgame":
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        double final = double.Parse(splitString[3]);
                        anotherSide = (side + 1) % 2;
                        sendString = string.Format("infoendgame,{0}", final);
                        service.SendToOne(gameTables[tableIndex].gamePlayer[anotherSide].user, sendString);
                        gameTables[tableIndex].gamePlayer[side].isplaying = false;
                        gameTables[tableIndex].gamePlayer[anotherSide].isplaying = false;
                        break;

                    case "messagechat":

                        tableIndex = int.Parse(splitString[1]);
                        string name = splitString[2];                     
                        int indexDelimiter = IndexDelimiter(receiveString);
                        string message = receiveString.Substring(indexDelimiter+1);                        
                        sendString = string.Format("MessageChat,{0},{1}",name, message);                       
                        service.SentToBoth(gameTables[tableIndex], sendString);

                        break;

                    case "loop":

                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        string stringBoard = splitString[3];
                        anotherSide = (side + 1) % 2;
                        User an= gameTables[tableIndex].gamePlayer[anotherSide].user;
                        sendString = string.Format("Loop,{0}", stringBoard);
                        service.SendToOne(an,sendString);
                        break;

                    case "playagain":

                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        string namePlayer= splitString[3];
                        gameTables[tableIndex].gamePlayer[side].isplayagain = true;
                        anotherSide = (side + 1) % 2;
                        if (gameTables[tableIndex].gamePlayer[anotherSide].isplayagain)
                        {
                            sendString = "PlayAgainComplete";
                            service.SentToBoth(gameTables[tableIndex],sendString);
                            gameTables[tableIndex].gamePlayer[side].isplayagain=false;
                            gameTables[tableIndex].gamePlayer[anotherSide].isplayagain = false;
                        }
                        else
                        {
                            User ann = gameTables[tableIndex].gamePlayer[anotherSide].user;
                            sendString = $"RequirePlayAgain,{namePlayer}";
                            service.SendToOne(ann, sendString);
                        }
                        break;

                    case "escapeinbattle":
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        gameTables[tableIndex].gamePlayer[side].isplaying = false;
                        anotherSide = (side + 1) % 2;
                        User annn = gameTables[tableIndex].gamePlayer[anotherSide].user;
                        sendString = "escapeinbattle";
                        if (gameTables[tableIndex].gamePlayer[anotherSide].isplaying)
                        {
                            service.SendToOne(annn, sendString);
                        }
                        break;
                    case "exitnormal":
                        userList.Remove(user);
                        user = null;
                        

                        return;
                        
                }
            }
        }

        private int IndexDelimiter(string s)
        {
            char delimiter = ',';
            int count = 0;
            int index = -1;
            for(int i=0;i<s.Length; i++)
            {
                if (s[i]==delimiter)
                {
                    count++;
                    if(count==3)
                    {
                        index = i;
                        break;
                    }    
                }    
            }    
            return index;
        }

        private string GetOnlineString()
        {
            string str = "";
            for (int i = 0; i < gameTables.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    str += gameTables[i].gamePlayer[j].someone == true ? "1" : "0";
                }
            }
            return str;
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(myListener!=null)
            {
                myListener.Stop();
            }    
             

        }
    }

}
