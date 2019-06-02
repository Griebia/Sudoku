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

/// Created by Gabrielius Ulejevas

namespace Sudoku
{
    public partial class Main : Form
    {
        Player user;//Main usage of user


        public Main()
        {
            InitializeComponent();
            closeAllPanels();
            panel1.Visible = true;
        }

        /// <summary>
        /// Makes all of the panels not visable
        /// </summary>
        private void closeAllPanels()
        {
            panel1.Visible = false;
            logInPannel.Visible = false;
            registerPannel.Visible = false;
            menuPanel.Visible = false;
            gamePanel.Visible = false;
        }

        /// <summary>
        /// Button click which opens up the log in panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            closeAllPanels();
            logInPannel.Visible = true;
        }

        /// <summary>
        /// Button click which opens up the register panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            closeAllPanels();
            registerPannel.Visible = true;
        }


        /// <summary>
        /// Button click of pressing "Log in" in log in panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logInBtn_Click(object sender, EventArgs e)
        {
            string username = usernameLogInText.Text.ToLower();
            string password = passwordLogInText.Text;

            if (!LogInto(username, password))
            {
                loginLabel.Text = "Your username or password was wrong";
            }
        }


        /// <summary>
        /// Function that when called opens menu panel and sets the information in it
        /// </summary>
        private void showMenu()
        {
            closeAllPanels();
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

        /// <summary>
        /// Back button from login panel to the main one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backLogInBtn_Click(object sender, EventArgs e)
        {
            closeAllPanels();
            panel1.Visible = true;
        }


        /// <summary>
        /// Button click of "Register" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerBtn_Click(object sender, EventArgs e)
        {
            string username = usernameRegisterBox.Text.ToLower();
            string password = passRegisterBox1.Text;

            //If that look if the textboxes are correctly filled in
            if (username == "" || passRegisterBox1.Text == "" || passRegisterBox2.Text == "")
            {
                registerLabel.Text = "Please fill in all of the boxes!";
            }
            else if (usernameCheck(username))
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
                //Puts new entery to the database and logs the user in
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

        /// <summary>
        /// Back button in register panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerBackBtn_Click(object sender, EventArgs e)
        {
            closeAllPanels();
            panel1.Visible = true;
        }

        /// <summary>
        /// Play button in menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBtn_Click(object sender, EventArgs e)
        {
            //Switches from the selected difficulty
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    removeNumber = 20;
                    break;
                case 2:
                    removeNumber = 30;
                    break;
                case 3:
                    removeNumber = 36;
                    break;
            }
            //Looks if the difficulty is picked
            if (comboBox1.SelectedIndex != 0 && comboBox1.SelectedIndex != -1)
            {
                closeAllPanels();
                gamePanel.Visible = true;
                buildSudoku();

            }
        }

        /// <summary>
        /// Button that checks if the sudoku is solved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameCheckBtn_Click(object sender, EventArgs e)
        {
            gameCheckBtn.Visible = false;
            bool correct = true;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j].Text == "" || int.Parse(grid[i, j].Text) != answers[i, j])
                    {
                        correct = false;
                        grid[i, j].BackColor = Color.Red;
                    }
                }
            }
            //If true it adds to the user score
            if (correct)
            {
                user.Score++;
                PlayerDatabase.UpdateScore(user);
                gameLabel.Text = "You won!!!\n Congratulations!!!";
                // write to a label Congratulation you solved the sudoku correctly

            }
            else
            {
                gameLabel.Text = "There were wrong numbers added in red boxes.\n Good luck next time.";
            }
        }

        /// <summary>
        /// Button click when pressed "Play again" in game panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gamePlayAgainBtn_Click(object sender, EventArgs e)
        {
            buildSudoku();
        }


        /// <summary>
        /// Back button in game panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameBackBtn_Click(object sender, EventArgs e)
        {
            showMenu();
        }



        /////////////////////////////////////////////////////Game Logic/////////////////////////////////////////////////////////////



        public int removeNumber;            //Number that represents how many of numbers there is a need to dispose (It is set when the button is pressed in the menu panel)
        static Random rand = new Random();  //Random number generator
        const int size = 9;                 // Size of the grid in sudoku (9x9)
        const int height = 50;              //The size of the grid(box sizes) 
        private int[,] numbers;             //The numbers that are added to the grid
        private int[,] answers;             //The numbers that are later used to check if it is all right
        List<int>[,] notGood;               //List of numbers in specific place where they cannot be put
        private TextBox[,] grid;            //Grid that adds that shows the sudoku element


        /// <summary>
        /// Builds the sudoku in game panel
        /// </summary>
        private void buildSudoku()
        {
            //Resets the elements in the game panel
            gameCheckBtn.Visible = true;
            gameLabel.Text = "";
            //Removes all of the added textbox elements (grid) from the visualization
            foreach (Control item in gamePanel.Controls.OfType<TextBox>().ToList())
            {
                gamePanel.Controls.Remove(item);
            }
            //Clears/creates old/new information
            numbers = new int[size, size];
            answers = new int[size, size];
            notGood = new List<int>[size, size];
            grid = new TextBox[size, size];
            //Makes the grid in which the sudoku is places
            MakeGrid();
            //Generates sudoku
            GenerateSudoku();
        }

        /// <summary>
        /// Generates sudoku in the given grid
        /// </summary>
        public void GenerateSudoku()
        {
            //Recursively generates the sudoku from specified place
            RecursiveGeneration(0, 0);
            //Removes the numbers count by the difficulty
            RemoveNumbers(removeNumber);
            //Adds the numbers to the gird
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

        /// <summary>
        /// Generates the numbers that cannot be used in a specific point
        /// </summary>
        /// <param name="num"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Return a list of int that contains numbers that cannot be usesd</returns>
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
                        grid[i, j].ReadOnly = true;
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


        /// <summary>
        /// Button click when presed to log out in menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutBtn_Click(object sender, EventArgs e)
        {
            user = null;
            closeAllPanels();
            panel1.Visible = true;
        }

    }
}

