using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerGoGame
{
    public class User
    {
        public TcpClient client {  get; set; }
        public StreamReader sr { get; set; }
        public StreamWriter sw { get; set; }
        public string userName { get; set; }    

        public User(TcpClient client)
        {
           this.client = client;
            this.userName = "";
            NetworkStream networkStream = client.GetStream();
            sr = new StreamReader(networkStream, System.Text.Encoding.UTF8);
            sw=new StreamWriter(networkStream, System.Text.Encoding.UTF8);
        }
    }
}
