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
    public partial class Menu : Form
    {
        public Player user { get; set; }

        public Menu()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game game = new Game(); ;
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    game.removeNumber = 1;
                    break;
                case 2:
                    game.removeNumber = 30;
                    break;
                case 3:
                    game.removeNumber = 40;
                    break;
            }
            if (comboBox1.SelectedIndex != 0)
            {
                this.Hide();
                game.Size = this.Size;
                game.StartPosition = FormStartPosition.Manual;
                game.Location = new Point(this.Location.X, this.Location.Y);
                game.user = this.user;
                if (game.ShowDialog() == DialogResult.OK)
                {
                    this.Size = game.Size;
                    this.Location = new Point(game.Location.X, game.Location.Y);
                    this.Show();
                }
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            label1.Text += user.Username;
            label2.Text += user.Score;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
