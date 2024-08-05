using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOGameProject.Properties;

namespace XOGameProject
{
    public partial class XOGAME : Form
    {
        public XOGAME()
        {
            InitializeComponent();
        }


        //The Global Variables
        private int playerTurn = 8;
        private int sizeOfXStatusArray = 0;
        private int sizeOfOStatusArray = 0;
        private int[] XStatus = new int[5];
        private int[] OStatus = new int[5]; //it should be four but I made it 5 for the loop


        //The Methods
        void SpecifyTurn(PictureBox pictureBox)
        {
            HandleClickedFields(pictureBox);
            if(playerTurn % 2 == 0)
            {
                playerTurn--;
                OPlayerTurn(); //Because When X's Played His Turn Next Will Be O's Turn
                AddXTurn(pictureBox, 'X');
            }
            else
            {
                playerTurn--;
                XPlayerTurn(); //Because When O's Played His Turn Next Will Be X's Turn
                AddOTurn(pictureBox, 'O');
            }
        }
        void HandleClickedFields(PictureBox pictureBox)
        {
            pictureBox.Enabled = false;
        }
        void XPlayerTurn() 
        {
            lblTurnResult.Text = "Player 1 Turn";
        }
        void OPlayerTurn()
        {
            lblTurnResult.Text = "Player 2 Turn";
        }
        void AddXTurn(PictureBox pictureBox, char playerTurn)
        {
            pictureBox.Image = Resources.X;
            SpecifyStatusOfTheGame(pictureBox, playerTurn);
        }
        void AddOTurn(PictureBox pictureBox, char playerTurn)
        {
            pictureBox.Image = Resources.O;
            SpecifyStatusOfTheGame(pictureBox, playerTurn);
        }
        void SpecifyStatusOfTheGame(PictureBox pictureBox, char playerTurn)
        {
            if(this.playerTurn == -1)
            {
                GameOver();
            }
            WinnerCombinations(pictureBox, playerTurn);
        }
        void WinnerCombinations(PictureBox pictureBox, char playerTurn)
        {
            int[,] winningCombinations =
            {
                {1,2,3},
                {4,5,6},
                {7,8,9},
                {1,5,9},
                {3,5,7},
                {1,4,7},
                {2,5,8},
                {3,6,9},
            };
            if (playerTurn == 'X')
            {
                int[] XComb = FindXCombination(pictureBox);
                CheckWinner(XComb, winningCombinations, playerTurn);
            }
            if (playerTurn == 'O')//else
            {
                int[] OComb = FindOCombination(pictureBox);
                CheckWinner(OComb, winningCombinations, playerTurn);
            }
            }
        int[] FindXCombination(PictureBox pictureBox)
        {
            int XPlaceTag = Convert.ToInt32(pictureBox.Tag.ToString());
            XStatus[sizeOfXStatusArray] = XPlaceTag;
            sizeOfXStatusArray++;
            return XStatus;
        }
        int[] FindOCombination(PictureBox pictureBox)
        {
            int OPlaceTag = Convert.ToInt32(pictureBox.Tag.ToString());
            OStatus[sizeOfOStatusArray] = OPlaceTag;
            sizeOfOStatusArray++;
            return OStatus;
        }
        void CheckWinner(int[] Comb, int[,] winningCombinations, char player)
        {
            int checkWinner = 0;
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    for (int xo = 0; xo < 5; xo++)
                    {
                        if (Comb[xo] == winningCombinations[r, c])
                        {
                           //{1,3,6,5,9}
                           //{1,3,5}
                            checkWinner++;
                            continue;
                        }
                    }
                }
                if (checkWinner == 3)
                {
                    String statement = player == 'X' ? "Player 1" : "Player 2";
                    Winner($"{statement} Has Won The Game");
                    return;
                }
            checkWinner = 0;
            }
        }
        void Winner(String winningStatement)
        {
            lblWinnerResult.Text = winningStatement;
            MessageBox.Show(winningStatement, "We have A Winner🏆🥇!!!",MessageBoxButtons.OK);
            pb1.Enabled = false;
            pb2.Enabled = false;
            pb3.Enabled = false;
            pb4.Enabled = false;
            pb5.Enabled = false;
            pb6.Enabled = false;
            pb7.Enabled = false;
            pb8.Enabled = false;
            pb9.Enabled = false;
        }
        void GameOver()
        {
            lblWinnerResult.Text = "Game Over";
            MessageBox.Show("We Don't Have A Winner :(", "No Winner!!!", MessageBoxButtons.OK);
            pb1.Enabled = false;
            pb2.Enabled = false;
            pb3.Enabled = false;
            pb4.Enabled = false;
            pb5.Enabled = false;
            pb6.Enabled = false;
            pb7.Enabled = false;
            pb8.Enabled = false;
            pb9.Enabled = false;
        }
        void ResetGame()
        {
            pb1.Image = Resources.question_mark_96;
            pb2.Image = Resources.question_mark_96;
            pb3.Image = Resources.question_mark_96;
            pb4.Image = Resources.question_mark_96;
            pb5.Image = Resources.question_mark_96;
            pb6.Image = Resources.question_mark_96;
            pb7.Image = Resources.question_mark_96;
            pb8.Image = Resources.question_mark_96;
            pb9.Image = Resources.question_mark_96;
            pb1.Enabled = true;
            pb2.Enabled = true;
            pb3.Enabled = true;
            pb4.Enabled = true;
            pb5.Enabled = true;
            pb6.Enabled = true;
            pb7.Enabled = true;
            pb8.Enabled = true;
            pb9.Enabled = true;
            XStatus = new int[5];
            OStatus = new int[5];
            sizeOfXStatusArray = 0;
            sizeOfOStatusArray = 0;
            playerTurn = 8;
            lblTurnResult.Text = "In Progress";
            lblWinnerResult.Text = "In Progress";
        }

      
        //The Event Handlers
        private void PaintingForm(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255, 255);
            Pen Pen = new Pen(White);
            Pen.Width = 10;
            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(Pen, 400, 100, 400, 400);
            e.Graphics.DrawLine(Pen, 500, 100, 500, 400);
            e.Graphics.DrawLine(Pen, 300, 200, 600, 200);
            e.Graphics.DrawLine(Pen, 300, 300, 600, 300);
        }
        private void pb_Click(object sender, EventArgs e)
        {
            SpecifyTurn((PictureBox)sender);
        }
        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}
