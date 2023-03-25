using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        public TicTacToe()
        {
            InitializeComponent();
        }

        List<Button> ShapeButtons = new List<Button>();
        private Random rand = new Random();
        private bool player1Turn;
        private bool player2Turn;
        private bool playerClicked = false;
        private bool startedGame = false;
        private string[] playerShape = new string[2] { "O", "X" };
        private bool winner = false;
        private string winnerSign = "#";
        private int buttonsClicked = 0;
        private int player1Score = 0;
        private int player2Score = 0;

        private void ButtonList()
        {
            ShapeButtons.Add(ShapeButton1);
            ShapeButtons.Add(ShapeButton2);
            ShapeButtons.Add(ShapeButton3);
            ShapeButtons.Add(ShapeButton4);
            ShapeButtons.Add(ShapeButton5);
            ShapeButtons.Add(ShapeButton6);
            ShapeButtons.Add(ShapeButton7);
            ShapeButtons.Add(ShapeButton8);
            ShapeButtons.Add(ShapeButton9);
        }

        private static bool IsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }

        private void StartGame()
        {
            startedGame = true;
            player1Turn = rand.Next(2) == 0;
            player2Turn = !player1Turn;
            
            ButtonList();
        }

        private void MarkButton(int buttonNumber)
        {
            if (ShapeButtons[buttonNumber].Text == "")
            {
                if (player1Turn && !player2Turn)
                {
                    ShapeButtons[buttonNumber].Text = playerShape[0];
                    ShapeButtons[buttonNumber].ForeColor = Color.DodgerBlue;
                }
                if (!player1Turn && player2Turn)
                {
                    ShapeButtons[buttonNumber].Text = playerShape[1];
                    ShapeButtons[buttonNumber].ForeColor = Color.Firebrick;
                }
            }
        }

        private void PlayerTurn()
        {
            if (playerClicked && player1Turn)
            {
                player1Turn = false;
                playerClicked = false;
                player2Turn = true;
                TurnLabel.Text = playerShape[1];
                TurnLabel.ForeColor = Color.Firebrick;
            }
            else if (playerClicked && player2Turn)
            {
                playerClicked = false;
                player2Turn = false;
                player1Turn = true;
                TurnLabel.Text = playerShape[0];
                TurnLabel.ForeColor = Color.DodgerBlue;
            }
            Winner();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            StartGame();
            playerClicked = true;
            PlayButton.Hide();
            ExitButton.Hide();
            PlayerTurn();
            Reset();
        }

        private void PlayerClick(int i)
        {
            bool isEmpty = IsEmpty(ShapeButtons);
            if (!isEmpty)
            {
                if (ShapeButtons[i].Text == "")
                {
                    playerClicked = true;
                    buttonsClicked++;
                    MarkButton(i);
                    PlayerTurn();
                }
            }
            Winner();
            if (winner)
                EndMatch();
            else
                EndMatchDraw();
        }

        private void ShapeButton1_Click(object sender, EventArgs e)
        {
            PlayerClick(0);
        }

        private void ShapeButton2_Click(object sender, EventArgs e)
        {
            PlayerClick(1);
        }

        private void ShapeButton3_Click(object sender, EventArgs e)
        {
            PlayerClick(2);
        }

        private void ShapeButton4_Click(object sender, EventArgs e)
        {
            PlayerClick(3);
        }

        private void ShapeButton5_Click(object sender, EventArgs e)
        {
            PlayerClick(4);
        }

        private void ShapeButton6_Click(object sender, EventArgs e)
        {
            PlayerClick(5);
        }

        private void ShapeButton7_Click(object sender, EventArgs e)
        {
            PlayerClick(6);
        }

        private void ShapeButton8_Click(object sender, EventArgs e)
        {
            PlayerClick(7);
        }

        private void ShapeButton9_Click(object sender, EventArgs e)
        {
            PlayerClick(8);
        }

        private bool CheckThree(string a, string b, string c)
        {
            if (a == b && a == c && b == c)
            {
                if ((a == playerShape[0] && b == playerShape[0] && c == playerShape[0]) || (a == playerShape[1] && b == playerShape[1] && c == playerShape[1]))
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private void Winner()
        {
            bool isEmpty = IsEmpty(ShapeButtons);
            if (!isEmpty)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckThree(ShapeButtons[i*3].Text, ShapeButtons[i*3+1].Text, ShapeButtons[i*3+2].Text))
                    {
                        winner = true;
                        winnerSign = ShapeButtons[i*3].Text;
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    if (CheckThree(ShapeButtons[j].Text, ShapeButtons[j+3].Text, ShapeButtons[j+6].Text))
                    {
                        winner = true;
                        winnerSign = ShapeButtons[j+3].Text;
                    }
                }
                int m = 0;
                for (int k = 0; k <= 2; k += 2)
                {
                    
                    if (CheckThree(ShapeButtons[k].Text, ShapeButtons[k+4-m].Text, ShapeButtons[k+8-2*m].Text))
                    {
                        winner = true;
                        winnerSign = ShapeButtons[k].Text;
                    }
                    m = m + 2;
                }
            }
        }

        private void ButtonsOpacity()
        {
            bool isEmpty = IsEmpty(ShapeButtons);
            if (!isEmpty)
            {
                for (int i = 0; i < 9; i++)
                {
                    ShapeButtons[i].BackColor = Color.FromArgb(50, Color.WhiteSmoke);
                    ShapeButtons[i].ForeColor = Color.FromArgb(0, Color.LightCyan);
                }
            }
        }

        private void EndMatch()
        {
            if (winner)
            {
                ButtonsOpacity();
                if (winnerSign == playerShape[0])
                {
                    WinnerLabel.Text = "Player 1 Wins!";
                    WinnerLabel.ForeColor = Color.DodgerBlue;
                    player1Score++;
                    Player1ScoreLabel.Text = Convert.ToString(player1Score);
                }
                if (winnerSign == playerShape[1])
                {
                    WinnerLabel.Text = "Player 2 Wins!";
                    WinnerLabel.ForeColor = Color.Firebrick;
                    player2Score++;
                    Player2ScoreLabel.Text = Convert.ToString(player2Score);
                }
                AdjustPlayButton();
            }
            
        }
        private void EndMatchDraw()
        {
            if (buttonsClicked == 9)
            {
                ButtonsOpacity();
                WinnerLabel.Text = "Draw!";
                WinnerLabel.ForeColor = Color.Gray;
                AdjustPlayButton();
            }
        }
        private void Reset()
        {
            WinnerLabel.Hide();
            for (int i = 0; i < 9; i++)
            {
                ShapeButtons[i].BackColor = Color.FromArgb(255, Color.Azure);
                ShapeButtons[i].ForeColor = Color.FromArgb(255, Color.Black);
                ShapeButtons[i].Text = "";
            }
            winner = false;
            winnerSign = "#";
            buttonsClicked = 0;
        }

        private void AdjustPlayButton()
        {
            WinnerLabel.BackColor = Color.FromArgb(15, Color.Transparent);
            WinnerLabel.Visible = true;
            PlayButton.Visible = true;
            ExitButton.Visible = true;
            PlayButton.Width = 150;
            PlayButton.Location = new Point(110, 166);
            PlayButton.Text = "Play again";
            TurnLabel.Text = "";
            startedGame = false;
            playerClicked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}