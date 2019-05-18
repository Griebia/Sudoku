using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Player
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Score { get; set; }

        public Player() { }

        public Player(string Username, string Password) {
            this.Username = Username;
            this.Password = Password;
            this.Score = 0;
        }

    }
}
