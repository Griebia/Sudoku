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
            this.Hide();
            var login = new LogIn();
            login.Size = this.Size;
            login.StartPosition = FormStartPosition.Manual;
            login.Location = new Point(this.Location.X, this.Location.Y);
            if (login.ShowDialog() == DialogResult.OK)
            {
                this.Size = login.Size;
                this.Location = new Point(login.Location.X, login.Location.Y);
                this.Show();
            } 
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var register = new Register();
            register.Size = this.Size;
            register.StartPosition = FormStartPosition.Manual;
            register.Location = new Point(this.Location.X, this.Location.Y);
            if (register.ShowDialog() == DialogResult.OK)
            {
                this.Size = register.Size;
                this.Location = new Point(register.Location.X, register.Location.Y);
                this.Show();
            }
            else
            {
                Close();
            }
        }
    }
}
