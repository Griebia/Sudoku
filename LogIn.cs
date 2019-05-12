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
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            List<Player> users = PlayerDatabase.LoadPlayers();

            foreach (var item in users)
            {
                if (item.Username == username && item.Password == password)
                {
                    var Game = new Game();
                    Game.Size = this.Size;
                    Game.Show();
                    this.Hide();
                }
            }
        }
    }
}
