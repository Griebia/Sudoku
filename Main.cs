using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Main : Form
    {
        Player user;


        public Main()
        {
            InitializeComponent();
            closeAllPanels();
            panel1.Visible = true;
        }

        private void closeAllPanels()
        {
            panel1.Visible = false;
            logInPannel.Visible = false;
            registerPannel.Visible = false;
            menuPanel.Visible = false;
            gamePanel.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            closeAllPanels();
            panel1.Visible = true;
            logInPannel.Visible = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            closeAllPanels();
            panel1.Visible = true;
            logInPannel.Visible = true;
            registerPannel.Visible = true;
        }

        private void logInBtn_Click(object sender, EventArgs e)
        {
            string username = usernameLogInText.Text.ToLower();
            string password = passwordLogInText.Text;

            if (!LogInto(username, password))
            {
                //Write that there is no such user
            }
        }

        private void showMenu()
        {
            closeAllPanels();
            panel1.Visible = true;
            logInPannel.Visible = true;
            registerPannel.Visible = true;
            menuPanel.Visible = true;
            scoreMenuLabel.Text = "Score: " + user.Score;
            welcomeMenulabel.Text = "Welcome " + user.Username;
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
                    user = item;
                    showMenu();
                    return true;
                }
            }

            return false;
        }

        private void backLogInBtn_Click(object sender, EventArgs e)
        {
            closeAllPanels();
            panel1.Visible = true;
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string username = usernameRegisterBox.Text.ToLower();
            string password = passRegisterBox1.Text;
            if (usernameCheck(username))
            {
                registerLabel.Text = "This usernames is already taken \n Please select a diffrent one";
            }
            else if (passRegisterBox1.Text != passRegisterBox2.Text)
            {
                registerLabel.Text = "Please check have you typed the same passwords in both boxes";
            }
            else
            {
                passRegisterBox1.Text = "Successfuly created account";
                PlayerDatabase.SavePlayer(new Player(username, password));
                LogInto(username, password);
            }
        }

        /// <summary>
        /// Checks if there is this kind of usename in the system
        /// </summary>
        /// <param name="username">Username that is checked</param>
        /// <returns>If there is this kind of username</returns>
        private bool usernameCheck(string username)
        {
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

        private void registerBackBtn_Click(object sender, EventArgs e)
        {
            closeAllPanels();
            panel1.Visible = true;
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
                
                switch (comboBox1.SelectedIndex)
                {
                    case 1:
                        removeNumber = 1;
                        break;
                    case 2:
                        removeNumber = 30;
                        break;
                    case 3:
                        removeNumber = 40;
                        break;
                }
                if (comboBox1.SelectedIndex != 0)
                {
                closeAllPanels();
                panel1.Visible = true;
                logInPannel.Visible = true;
                registerPannel.Visible = true;
                menuPanel.Visible = true;
                gamePanel.Visible = true;
                buildSudoku();
                //Change to play the game
            }
            }

        private void gameCheckBtn_Click(object sender, EventArgs e)
        {
            bool correct = true;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j].Text == "" || int.Parse(grid[i, j].Text) != answers[i, j])
                    {
                        Debug.WriteLine(grid[i, j].Text);
                        Debug.WriteLine(answers[i, j]);
                        correct = false;
                        grid[i, j].BackColor = Color.Red;
                    }
                }
            }

            if (correct)
            {
                user.Score++;
                PlayerDatabase.UpdateScore(user);
                gameLabel.Text = "You won!!!\n Congratulations!!!";
                // write to a label Congratulation you solved the sudoku correctly

            }
            else
            {

            }
        }

        private void gamePlayAgainBtn_Click(object sender, EventArgs e)
        {

        }

        private void gameBackBtn_Click(object sender, EventArgs e)
        {
            showMenu();
        }

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {

        }


        /////////////////////////////////////////////////////Game Logic/////////////////////////////////////////////////////////////
        public int removeNumber;
        
        Random rand = new Random();
        const int size = 9;
        const int height = 50; //The size of the grid(box sizes) 
        private int[,] numbers = new int[size, size];
        private int[,] answers = new int[size, size];
        //private int[,] numbers = { {5,3,4,6,7,8,9,1,2},
        //                           {6,7,2,1,9,5,3,4,8},
        //                           {1,9,8,3,4,2,5,6,7},
        //                           {8,5,9,7,6,1,4,2,3},
        //                           {4,2,6,8,5,3,7,9,1},
        //                           {7,1,3,9,2,4,8,5,6},
        //                           {9,6,1,5,3,7,2,8,4},
        //                           {2,8,7,4,1,9,6,3,5},
        //                           {3,4,5,2,8,6,1,7,9}};
        List<int>[,] notGood = new List<int>[size, size];
        private TextBox[,] grid = new TextBox[size, size];

        private void buildSudoku()
        {
            MakeGrid();
            GenerateSudoku();
        }

        public void GenerateSudoku()
        {
            RecursiveGeneration(0, 0);
            RemoveNumbers(removeNumber);
            AddNumbersToGrid();
        }

        /// <summary>
        /// Generates all of the numbers
        /// </summary>
        /// <param name="rowNum">Numebrs that are in that same exact row</param>
        /// <param name="x">Which collum</param>
        /// <param name="y">Which row</param>
        /// <returns></returns>
        public bool RecursiveGeneration(int x, int y)
        {

            //Return that the generation is done
            if (y >= size)
            {
                return true;
            }

            //Return to another row if it is all right to use
            if (x >= size)
            {
                x = 0;
                y++;
                return RecursiveGeneration(x, y);
            }

            //Makes a number list that is generated if there ar any not needed numbers
            List<int> notUse;

            if (notGood[y, x] == null)
            {
                notUse = MakeNotUsedNumbers(ref numbers, x, y);
                notGood[y, x] = notUse;
            }
            else
            {
                notUse = notGood[y, x];
            }


            //Making a list of numbers that can be used to add that number in that space
            var list = Enumerable.Range(1, 9).Except(notUse).ToArray();
            if (list.Length > 0)
            {
                int num = list[rand.Next(0, list.Length)];
                numbers[y, x] = num;
                notUse.Add(num);
                x++;
            }
            else
            {
                numbers[y, x] = 0;
                notGood[y, x] = null;
                if (x == 0)
                {
                    x = 8;
                    y--;
                }
                else
                    x--;
            }

            return RecursiveGeneration(x, y);
        }

        //Not tested if it works

        public List<int> MakeNotUsedNumbers(ref int[,] num, int x, int y)
        {
            //Adds all of the numbers from the box
            List<int> notUse = BoxNumbers(ref num, x, y);

            //Add all of the numbers from the row
            for (int i = 0; i < x; i++)
            {
                if (!notUse.Contains(num[y, i]) && num[y, i] != -1)
                {
                    notUse.Add(num[y, i]);
                }
            }

            //Add all of not added numbers for the collums
            for (int i = 0; i < y; i++)
            {
                if (!notUse.Contains(num[i, x]) && num[i, x] != -1)
                {
                    notUse.Add(num[i, x]);
                }
            }

            return notUse;
        }

        /// <summary>
        /// Makes and removes numbers form a generated list to make the game
        /// </summary>
        /// <param name="num">Remove number count</param>
        void RemoveNumbers(int num)
        {
            int x, y;
            answers = (int[,])numbers.Clone();
            for (int i = 0; i < num; i++)
            {
                x = rand.Next(0, 9);
                y = rand.Next(0, 9);
                if (numbers[y, x] == -1)
                {
                    i--;
                    continue;
                }
                int removedNumber = numbers[y, x];
                numbers[y, x] = -1;

                if (!CheckIfSolvable())
                {
                    numbers[y, x] = removedNumber;
                    i--;
                }
            }

        }

        /// <summary>
        /// Checks if made sudoku is solvable
        /// </summary>
        /// <returns>If it is solvable</returns>
        private bool CheckIfSolvable()
        {
            //Copy the sudoku that we are checking
            int[,] solvable = (int[,])numbers.Clone();
            //Add atleast one that the cicle would start
            //This number represents for how many solved spaces there is in an iteration
            int found = 1;
            while (found > 0)
            {
                found = 0;
                //Iterating throw all of the spaces
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        //If there is a empty space than look in to it
                        if (solvable[i, j] == -1)
                        {
                            //Find all of the numbers that cannot be added to that space
                            List<int> notUsed = MakeNotUsedNumbers(ref solvable, j, i);
                            //If the number of digits that cannot be placed are equal to 8 than there is only one number that could be added
                            if (notUsed.Count >= 8)
                            {
                                var list = Enumerable.Range(1, 9).Except(notUsed).ToArray();
                                solvable[i, j] = list[0];
                                found++;
                            }
                        }
                    }
                }
            }

            //Checks if there is any places that couldn't be solved
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (solvable[i, j] == -1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Finds used ints in the specific space
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public List<int> BoxNumbers(ref int[,] num, int x, int y)
        {
            List<int> ans = new List<int>();

            //Finding in which box is the added number
            int boxColumn = x / 3;
            int boxRow = y / 3;

            for (int j = 3 * boxRow; j < boxRow * 3 + 3; j++)
            {
                for (int i = 3 * boxColumn; i < boxColumn * 3 + 3; i++)
                {

                    if (num[j, i] == 0)
                    {
                        break;
                    }
                    if (num[j, i] != -1)
                    {
                        ans.Add(num[j, i]);
                    }
                }
            }
            return ans;
        }

        /// <summary>
        /// Adds all of the numbers to grid
        /// </summary>
        public void AddNumbersToGrid()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (numbers[i, j] != -1)
                    {
                        grid[i, j].Text = numbers[i, j].ToString();
                        grid[i, j].BackColor = Color.BlueViolet;
                    }
                    else
                    {
                        grid[i, j].Text = "";
                    }

                }
            }
        }

        /// <summary>
        /// Makes size x size grid that is centered 
        /// </summary>
        public void MakeGrid()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = new TextBox();
                    grid[i, j].Text = i.ToString();
                    grid[i, j].Size = new Size(height, height);
                    grid[i, j].Location = new Point(j * height, i * height);
                    grid[i, j].Font = new Font("Arial", height - 20);
                    grid[i, j].TextAlign = HorizontalAlignment.Center;
                    grid[i, j].MaxLength = 1;
                    grid[i, j].KeyPress += delegate (object o, KeyPressEventArgs e)
                    {
                        if (!char.IsControl(e.KeyChar) && !char.IsNumber(e.KeyChar))
                        {
                            e.Handled = true;
                        }

                    };
                 
                    gamePanel.Controls.Add(grid[i, j]);
                }
            }
        }
    }
    }

