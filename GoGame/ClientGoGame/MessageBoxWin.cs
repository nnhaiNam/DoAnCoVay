using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGoGame
{
    public partial class MessageBoxWin : Form
    {
        public MessageBoxWin()
        {
            InitializeComponent();
        }

        private void buttonOki_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
