using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var login = new LogIn();
            login.Size = this.Size;
            login.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var register = new Register();
            register.Size = this.Size;
            register.Show();
            this.Hide();
        }
    }
}
