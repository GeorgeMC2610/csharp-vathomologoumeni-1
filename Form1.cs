﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_vathomologoumeni_1
{
    public partial class MainMenu : Form
    {
        public static string[] EasyHI   = new string[5];
        public static string[] NormalHI = new string[5];
        public static string[] HardHI   = new string[5];
        public static string[] ExpertHI = new string[5];
        short option;
        bool radioButtonSelected = false;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            playButton.Enabled = false;
            playButton.BackColor = Color.LightGray;

            for (int i = 0; i < 5; i++)
                EasyHI[i] = NormalHI[i] = HardHI[i] = ExpertHI[i] = "---";

            Player.SortHighScores();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            //when the player presses the play button, we hide this form, and we open the other one which plays the game
            DiceClicker form2 = new DiceClicker(option, textBox1.Text);        //We also pass the difficulty value in the other form
            form2.Show();

            //After we show the other form to the user, we hide this one, so it doesn't open twice.
            Hide();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //this checks if the button can be enabled or not. A difficulty option must be selected, and there must be a username provided.
        private void checkForEnable()
        {
            //if there is a difficulty and a username provided
            if (radioButtonSelected && textBox1.Text.Length != 0)
            {
                //we enable the button, with a blue colour, we hide the yellow text tip and we make the cursor a Hand model
                playButton.Enabled = true;
                playButton.BackColor = Color.RoyalBlue;
                label3.Text = "";
                playButton.Cursor = Cursors.Hand;
            }
            else
            {
                //if not, we disable the button, with a gray colour, and then we show the yellow text tip. Even when we change the cursor it doesn't actually change, because the button is disabled.
                playButton.Enabled = false;
                playButton.BackColor = Color.LightGray;
                label3.Text = "Select a difficulty and\nenter a nickname to\nstart the game!";
                playButton.Cursor = Cursors.No;
            }
        }

        //In each of these functions the difficulty option changes depending on the radio buttons
        private void Easy_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonSelected = true;
            checkForEnable();
            option = 1;
        }

        private void Normal_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonSelected = true;
            checkForEnable();
            option = 2;
        }

        private void Hard_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonSelected = true;
            checkForEnable();
            option = 3;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonSelected = true;
            checkForEnable();
            option = 4;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExitGame_Click(object sender, EventArgs e)
        {
            //Sometimes, exiting by pressing 'X' doesn't work. So we make sure all hidden (open) forms close.
            try
            {
                foreach (Form f in Application.OpenForms)
                    f.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //Here we show the highest scores using the buttons provided
        private void ShowHighScores(string[] highScores, string level)
        {
            MessageBox.Show("1. " + highScores[0] + "\n" +
                            "2. " + highScores[1] + "\n" +
                            "3. " + highScores[2] + "\n" +
                            "4. " + highScores[3] + "\n" +
                            "5. " + highScores[4] + "\n", level + " level highest score holders");
        }

        //each one of these buttons will call the function above, and will show the corresponding high scores in order.
        private void Easy_HI_Click(object sender, EventArgs e)
        {
            ShowHighScores(EasyHI, "Easy");
        }

        private void Normal_HI_Click(object sender, EventArgs e)
        {
            ShowHighScores(NormalHI, "Normal");
        }

        private void Hard_HI_Click(object sender, EventArgs e)
        {
            ShowHighScores(HardHI, "Hard");
        }

        private void Expert_HI_Click(object sender, EventArgs e)
        {
            ShowHighScores(ExpertHI, "Expert");
        }

        //this ensures the text box won't contain the '|' character, because if it did, it would break the database.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            checkForEnable();

            if (textBox1.Text.Contains('|'))
            {
                //we show the corresponding message to the user.
                MessageBox.Show("The vertical bar ('|') is a banned character (See Help-->Nicknames).", "Invalid Username");
                textBox1.Text = textBox1.Text.Trim('|');
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thisGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This game was created in November 2020 as a mandatory-to-publish project in the third semester course called " +
                            "\"Object Oriented Application Development\" of University of Piraeus. This project had to be created by every student individually, " +
                            "as teams were not allowed. The maximum score this kind of project could yield, was 2 (out of 10) points to the final score. ", "What is this game?");
        }

        private void theCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This game was made by Giorgos Seimenis (R.No.: p19204), Student in the Informatics Department, University of Piraeus. As soon as you press \"Yes\", you will be redirected to his GitHub Account. ", "Who created this game?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("https://github.com/GeorgeMC2610");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong. EXCEPTION MESSAGE: " + ex.Message, "Error");
                }
            }
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HOW TO START:\n" +
                "Select a difficulty from the left box containing all difficulties (Easy, Normal, Hard, Expert) and " +
                "enter a nickname. Once you've done that, the button becomes blue and allows you to start playing!\n\n" +
                
                "MAIN GOAL:\n" +
                "Click the picture box, as it moves randomly in the panel, containing the dice in order to get points. Every point is the value of the dice. " +
                "For example, if you click a dice with 4 dots, you get 4 points. All your points add up to your final score, until the game ends " +
                "in 60 seconds. Once the game ends, your score gets stored, and if it is high enough, it will show up in the top 5 leaderboard!", "How to play?");
        }

        private void difficultiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("There are four different difficulties you can choose. Specifiacally...\n\n\n" +

                "EASY: \nThe easiest difficulty available has the biggest picture box to click on, provides the biggest " +
                "amount of time *1 second* to click (multiple times) on the box before it moves. \n\n" +

                "NORMAL: \nNormal difficulty is still easy to go through, as the picture box and reaction time are 25% smaller. " +
                "There is still time to click multiple times to the picture box in order to yield more points at a time. \n\n" +
                
                "HARD: \nHard difficulty is the most challening of all. Dice are not the only ones to appear in the picture box, " +
                "as there is a 5% chance for a bomb to appear. If clicked, the bomb, removes 10 points from your current score. " +
                "The picture box speed is half a second and the picture box is small enough to lose your shot easily. It's almost " +
                "impossible to click on the box more than one time before it moves. \n\n" +
                
                "EXPERT: \nHardest difficulty is for expert players only! The creator himslef hasn't done a score over 10 points. This " +
                "difficulty gives zero time to react and the picture box is really small to click. There is still a chance to hit a bomb " +
                "instead.", "Difficulties guide");
        }

        private void nicknameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Before you play the game, a nickname must be provided, otherwise the game doesn't start. This happens because " +
                "every player's highscore must be on the leaderboard, as this is something that meets the project requirements.\n\n" +
                
                "The only banned character, which none of the players can provide, is the Vertical Bar character '|'. The text file, " +
                "in which the players' scores get stored, seprates each property of the highscore (Nickname|DIFFICULTY|score). If a player " +
                "provides the vertical bar, it breaks the way the highscores are stored.", "Nicknames guide");
        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Highscores get stored in the \"highscores.txt\" file, in which there is simple text written by the program to store the highscores. " +
                "To store the scores, the program seperates three attributes with the vertical bar character ('|'). Different players get seperated with a single " +
                "line change.\n\n" +

                "The .txt file can be modified directly, but you should make sure the " +
                "game is completely closed and be careful on how you write things, as it might not work properly if there are seperate lines with no attributes.", "Highscores guide");
        }

        private void ClearHighscores_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("highscores.txt");
            string highscores = sr.ReadToEnd();
            sr.Close();

            //if the highscores are already empty, there is no need to empty them again.
            if (highscores.Length == 0)
            {
                MessageBox.Show("This action cannot be done, as there are no high scores stored.", "Highscores Empty");
                return;
            }

            if (MessageBox.Show("Effects cannot be reversed, once highscores get deleted. Are you sure?", "Delete all high scores?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //we first delete all the values in the text file.
                    StreamWriter sw = new StreamWriter("highscores.txt");
                    sw.Write("");
                    sw.Close();

                    //and then we update the arrays (as when the form starts)
                    for (int i = 0; i < 5; i++)
                        EasyHI[i] = NormalHI[i] = HardHI[i] = ExpertHI[i] = "---";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to delete scores. \n\nReason: " + ex.Message, "Error");
                }
            }
        }
    }
}
