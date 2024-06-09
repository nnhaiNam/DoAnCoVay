﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerGoGame
{
    public class Service
    {

        private ListBox listbox;
        private delegate void AddItemDelegate(string str);
        private AddItemDelegate addItemDelegate;

        public Service(ListBox listBox)
        {
            this.listbox= listBox;
            addItemDelegate = new AddItemDelegate(AddItem);
        }

        public void AddItem(string str)
        {
            if(listbox.InvokeRequired)
            {
                listbox.Invoke(addItemDelegate, str);
            }
            else
            {
                listbox.Items.Add(str);
                listbox.SelectedIndex=listbox.Items.Count-1;
                listbox.ClearSelected();
            }

        }

        //send message to client
        public void SendToOne(User user,string str)
        {
            if(user==null)
            {
                return;
            }    
            try
            {
                user.sw.WriteLine(str);
                user.sw.Flush();
                AddItem(string.Format("Send {1} to {0}",user.userName,str));
            }
            catch
            {
                AddItem(string.Format("Failed to send to {0}", user.userName));
            }
        }

        //Send a message to the same table

        public void SentToBoth(GameTable gameTable,string str)
        {
            for(int i = 0;i<2;i++)
            {
                if (gameTable.gamePlayer[i].someone==true)
                {
                    SendToOne(gameTable.gamePlayer[i].user,str);
                }    
            }
        }

        //Send message to all clients

        public void SendToAll(List<User> userList,string str)
        {
            for(int i=0;i<userList.Count;i++) {
                SendToOne(userList[i], str);
            }
        }


        
    }
}
