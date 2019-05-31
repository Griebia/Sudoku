using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Dapper;

namespace Sudoku
{
    class PlayerDatabase
    {
        public static List<Player> LoadPlayers() {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Player>("select * from Player", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SavePlayer(Player player)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Player(Username,Password,Score) values (@Username,@Password,@Score)", player);
            }
        }

        public static void UpdateScore(Player player)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("UPDATE Player SET Score =@Score WHERE Username =@Username", player);
            }
        }

        private static string LoadConnectionString(string id = "Default"){

            return ConfigurationManager.ConnectionStrings[id].ConnectionString;

        }
    }
}
