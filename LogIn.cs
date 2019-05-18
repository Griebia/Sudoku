﻿using System;
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

            if (!LogInto(username, password)) {
                //Write that there is no such user
            }
        }

        /// <summary>
        /// Log in to the game
        /// </summary>
        /// <param name="username">usename</param>
        /// <param name="password">password</param>
        /// <returns>Changes the view to the games if it is correct</returns>
        public bool LogInto(string username, string password)
        {
            List<Player> users = PlayerDatabase.LoadPlayers();

            foreach (var item in users)
            {
                if (item.Username == username && item.Password == password)
                {
                    this.Hide();
                    var Menu = new Menu();
                    Menu.user = item;
                    Menu.Size = this.Size;
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
    }
}
