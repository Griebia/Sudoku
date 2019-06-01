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
            string username = textBox1.Text.ToLower();
            string password = textBox2.Text;

            if (!LogInto(username, password,this)) {
                //Write that there is no such user
            }
        }

        /// <summary>
        /// Log in to the game
        /// </summary>
        /// <param name="username">usename</param>
        /// <param name="password">password</param>
        /// <returns>Changes the view to the games if it is correct</returns>
        static public bool LogInto(string username, string password, Form name)
        {
            List<Player> users = PlayerDatabase.LoadPlayers();

            foreach (var item in users)
            {
                if (item.Username == username && item.Password == password)
                {
                    name.Hide();
                    var Menu = new Menu();
                    Menu.user = item;
                    Menu.Size = name.Size;
                    Menu.StartPosition = FormStartPosition.Manual;
                    Menu.Location = new Point(name.Location.X, name.Location.Y);
                    Menu.ShowDialog();
                    return true ;
                }
            }

            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
