using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGoGame
{
    public  class Service
    {
        ListBox listBox;
        StreamWriter sw;
        public Service(ListBox listBox,StreamWriter sw)
        {
            this.listBox = listBox;
            this.sw = sw;
        }

        //send Data to Server
        public void SendToServer(string str)
        {
            try
            {
                sw.WriteLine(str);
                sw.Flush();
            }
            catch
            {
                AddItemToListBox("Failed to send data");
            }
        }

        delegate void ListBoxDelegate(string str);
        public void AddItemToListBox(string str)
        {
            if(listBox.InvokeRequired)
            {
                ListBoxDelegate d = AddItemToListBox;
                listBox.Invoke(d, str);
            }
            else
            {
                listBox.Items.Add(str);
                listBox.SelectedIndex = listBox.Items.Count - 1;
                listBox.ClearSelected();
            }
        }
    }
}
