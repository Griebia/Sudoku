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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text.ToLower();
            string password = passBox1.Text;
            if (usernameCheck(username))
            {
                label5.Text = "This usernames is already taken \n Please select a diffrent one";
            }
            else if (passBox1.Text != passBox2.Text)
            {
                label5.Text = "Please check have you typed the same passwords in both boxes";
            }
            else {
                label5.Text = "Successfuly created account";
                PlayerDatabase.SavePlayer(new Player(username, password));
                LogIn.LogInto(username, password,this);
            }
        }

        /// <summary>
        /// Checks if there is this kind of usename in the system
        /// </summary>
        /// <param name="username">Username that is checked</param>
        /// <returns>If there is this kind of username</returns>
        private bool usernameCheck(string username) {
            List<Player> users = PlayerDatabase.LoadPlayers();

            foreach (var item in users)
            {
                if (item.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
