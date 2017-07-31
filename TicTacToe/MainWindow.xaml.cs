// Matthew Kerr
// March 1, 2015

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string game_mode;
        private int[,] game_board;
        private int turn;
        private int game_status;
        //private Boolean game_reset;
        private Boolean game_active;
        private Rectangle[,,] x_lines; // row, col, left/right
        private Ellipse[,] o_ellipses; // row, col
        private Random rand;
        public MainWindow()
        {
            InitializeComponent();
            InitializeLines();
            InitializeEllipses();
            InitializeGameBoard();
            turn = -1;
            game_status = 0;
            game_mode = "2player";
            game_active = false;
            rand = new Random();
        }


        private void InitializeLines()
        {
            x_lines = new Rectangle[3, 3, 2];

            // top left
            x_lines[0, 0, 0] = xLineLeft_topRowLeftCol;
            x_lines[0, 0, 1] = xLineRight_topRowLeftCol;

            x_lines[0, 0, 0].IsEnabled = false;
            x_lines[0, 0, 1].IsEnabled = false;

            // top mid
            x_lines[0, 1, 0] = xLineLeft_topRowMidCol;
            x_lines[0, 1, 1] = xLineRight_topRowMidCol;

            // top right
            x_lines[0, 2, 0] = xLineLeft_topRowRightCol;
            x_lines[0, 2, 1] = xLineRight_topRowRightCol;

            // mid left
            x_lines[1, 0, 0] = xLineLeft_midRowLeftCol;
            x_lines[1, 0, 1] = xLineRight_midRowLeftCol;


            // mid mid
            x_lines[1, 1, 0] = xLineLeft_midRowMidCol;
            x_lines[1, 1, 1] = xLineRight_midRowMidCol;

            // mid right
            x_lines[1, 2, 0] = xLineLeft_midRowRightCol;
            x_lines[1, 2, 1] = xLineRight_midRowRightCol;

            // bot left
            x_lines[2, 0, 0] = xLineLeft_botRowLeftCol;
            x_lines[2, 0, 1] = xLineRight_botRowLeftCol;

            // bot mid
            x_lines[2, 1, 0] = xLineLeft_botRowMidCol;
            x_lines[2, 1, 1] = xLineRight_botRowMidCol;

            // bot right
            x_lines[2, 2, 0] = xLineLeft_botRowRightCol;
            x_lines[2, 2, 1] = xLineRight_botRowRightCol;

            ResetLines();
        }

        private void ResetLines()
        {
            // top left
            x_lines[0, 0, 0].Visibility = Visibility.Hidden;
            x_lines[0, 0, 1].Visibility = Visibility.Hidden;

            // top mid
            x_lines[0, 1, 0].Visibility = Visibility.Hidden;
            x_lines[0, 1, 1].Visibility = Visibility.Hidden;

            // top right
            x_lines[0, 2, 0].Visibility = Visibility.Hidden;
            x_lines[0, 2, 1].Visibility = Visibility.Hidden;

            // mid left
            x_lines[1, 0, 0].Visibility = Visibility.Hidden;
            x_lines[1, 0, 1].Visibility = Visibility.Hidden;


            // mid mid
            x_lines[1, 1, 0].Visibility = Visibility.Hidden;
            x_lines[1, 1, 1].Visibility = Visibility.Hidden;

            // mid right
            x_lines[1, 2, 0].Visibility = Visibility.Hidden;
            x_lines[1, 2, 1].Visibility = Visibility.Hidden;

            // bot left
            x_lines[2, 0, 0].Visibility = Visibility.Hidden;
            x_lines[2, 0, 1].Visibility = Visibility.Hidden;

            // bot mid
            x_lines[2, 1, 0].Visibility = Visibility.Hidden;
            x_lines[2, 1, 1].Visibility = Visibility.Hidden;

            // bot right
            x_lines[2, 2, 0].Visibility = Visibility.Hidden;
            x_lines[2, 2, 1].Visibility = Visibility.Hidden;
        }

        private void InitializeEllipses()
        {
            o_ellipses = new Ellipse[3, 3];

            // top left
            o_ellipses[0, 0] = oEllipse_topRowLeftCol;
            o_ellipses[0, 0].IsEnabled = false;

            // top mid
            o_ellipses[0, 1] = oEllipse_topRowMidCol;

            // top right
            o_ellipses[0, 2] = oEllipse_topRowRightCol;

            // mid left
            o_ellipses[1, 0] = oEllipse_midRowLeftCol;

            // mid mid
            o_ellipses[1, 1] = oEllipse_midRowMidCol;

            // mid right
            o_ellipses[1, 2] = oEllipse_midRowRightCol;

            // bot left
            o_ellipses[2, 0] = oEllipse_botRowLeftCol;

            // bot mid
            o_ellipses[2, 1] = oEllipse_botRowMidCol;

            // bot right
            o_ellipses[2, 2] = oEllipse_botRowRightCol;

            ResetEllipses();
        }

        private void ResetEllipses()
        {
            // top left
            o_ellipses[0, 0].Visibility = Visibility.Hidden;

            // top mid
            o_ellipses[0, 1].Visibility = Visibility.Hidden;

            // top right
            o_ellipses[0, 2].Visibility = Visibility.Hidden;

            // mid left
            o_ellipses[1, 0].Visibility = Visibility.Hidden;

            // mid mid
            o_ellipses[1, 1].Visibility = Visibility.Hidden;

            // mid right
            o_ellipses[1, 2].Visibility = Visibility.Hidden;

            // bot left
            o_ellipses[2, 0].Visibility = Visibility.Hidden;

            // bot mid
            o_ellipses[2, 1].Visibility = Visibility.Hidden;

            // bot right
            o_ellipses[2, 2].Visibility = Visibility.Hidden;
        }

        private void InitializeGameBoard()
        {
            game_board = new int[3,3];

            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                        game_board[i,j] = -1;
                }
            }
        }

        private void radioBtn_playComputer_Checked(object sender, RoutedEventArgs e)
        {
            game_mode = "computer";
            btn_coinToss.IsEnabled = true;
            mnu_coinToss.IsEnabled = true;
            mnu_playComputer.IsChecked = true;
        }

        private void radioBtn_twoPlayers_Checked(object sender, RoutedEventArgs e)
        {
            game_mode = "2player";
            btn_coinToss.IsEnabled = true;
            mnu_coinToss.IsEnabled = true;
            mnu_twoPlayers.IsChecked = true;
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            lbl_playerTurn.Content = "";
            game_active = false;
            ResetGame();
            btn_reset.IsEnabled = false;
            mnu_reset.IsEnabled = false;
            btn_coinToss.IsEnabled = true;
            mnu_coinToss.IsEnabled = true;
            radioBtn_playComputer.IsEnabled = true;
            mnu_playComputer.IsEnabled = true;
            radioBtn_twoPlayers.IsEnabled = true;
            mnu_twoPlayers.IsEnabled = true;
        }

        private void btn_coinToss_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            btn_reset.IsEnabled = true;
            mnu_reset.IsEnabled = true;
            btn_coinToss.IsEnabled = false;
            mnu_coinToss.IsEnabled = false;
            radioBtn_playComputer.IsEnabled = false;
            mnu_playComputer.IsEnabled = false;
            radioBtn_twoPlayers.IsEnabled = false;
            mnu_twoPlayers.IsEnabled = false;
            
            StartGame();
        }

        private void ResetGame()
        {
            //radioBtn_playComputer.IsEnabled = true;
            //radioBtn_twoPlayers.IsEnabled = true;
            InitializeGameBoard();
            ResetLines();
            ResetEllipses();
        }

        private void StartGame()
        {
            ResetGame();
            game_active = true;
            if (game_mode == "computer")
            {
                lbl_player1TurnInfo.Content = "Computer = O";
            }
            else
            {
                lbl_player1TurnInfo.Content = "Player 1 = O";
            }
            if (rand.Next(1, 3) == 1) // O goes first
            {
                turn = 1;
                lbl_playerTurn.Content = "O's Turn";
            }
            else // X goes first
            {
                turn = 2;
                lbl_playerTurn.Content = "X's Turn";
            }
            
            // computer is always player 1
            if (turn == 1 && game_mode == "computer")
            {
                TakeComputerTurn();
            }
        }


        public int CheckGameStatus()
        {
            if ((game_board[0, 0] == 1 && game_board[0, 1] == 1 && game_board[0, 2] == 1) // top row
             || (game_board[1, 0] == 1 && game_board[1, 1] == 1 && game_board[1, 2] == 1) // mid row
             || (game_board[2, 0] == 1 && game_board[2, 1] == 1 && game_board[2, 2] == 1) // bot row
             || (game_board[0, 0] == 1 && game_board[1, 0] == 1 && game_board[2, 0] == 1) // left col
             || (game_board[0, 1] == 1 && game_board[1, 1] == 1 && game_board[2, 1] == 1) // mid col
             || (game_board[0, 2] == 1 && game_board[1, 2] == 1 && game_board[2, 2] == 1) // right col
             || (game_board[0, 0] == 1 && game_board[1, 1] == 1 && game_board[2, 2] == 1) // left diag
             || (game_board[0, 2] == 1 && game_board[1, 1] == 1 && game_board[2, 0] == 1)) // right diag
            {
                // player 1 (O) won
                return 1;
            }
            else if ((game_board[0, 0] == 2 && game_board[0, 1] == 2 && game_board[0, 2] == 2) // top row
                  || (game_board[1, 0] == 2 && game_board[1, 1] == 2 && game_board[1, 2] == 2) // mid row
                  || (game_board[2, 0] == 2 && game_board[2, 1] == 2 && game_board[2, 2] == 2) // bot row
                  || (game_board[0, 0] == 2 && game_board[1, 0] == 2 && game_board[2, 0] == 2) // left col
                  || (game_board[0, 1] == 2 && game_board[1, 1] == 2 && game_board[2, 1] == 2) // mid col
                  || (game_board[0, 2] == 2 && game_board[1, 2] == 2 && game_board[2, 2] == 2) // right col
                  || (game_board[0, 0] == 2 && game_board[1, 1] == 2 && game_board[2, 2] == 2) // left diag
                  || (game_board[0, 2] == 2 && game_board[1, 1] == 2 && game_board[2, 0] == 2)) // right diag
            {
                // player 2 (X) won
                return 2;
            }
            else if (game_board[0, 0] != -1 && game_board[0, 1] != -1 && game_board[0, 2] != -1
                   && game_board[1, 0] != -1 && game_board[1, 1] != -1 && game_board[1, 2] != -1
                   && game_board[2, 0] != -1 && game_board[2, 1] != -1 && game_board[2, 2] != -1)
            {
                return 3;
            }
            // game not over yet
            return 0;
        }


        private void DrawX(int row, int col)
        {
            x_lines[row, col, 0].Visibility = Visibility.Visible;
            x_lines[row, col, 1].Visibility = Visibility.Visible;
        }

        private void DrawO(int row, int col)
        {
            o_ellipses[row, col].Visibility = Visibility.Visible;
        }


        private void canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            canvas.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            canvas.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
        }

        private void TakeTurn(int row, int col)
        {
            if (game_board[row, col] != -1) // spot is already taken
            {
                return;
            }
            else if (turn == 1)
            {
                game_board[row, col] = 1;
                DrawO(row, col);
            }
            else
            {
                game_board[row, col] = 2;
                DrawX(row, col);
            }

            if (CheckGameStatus() != 0)
            {
                GameOverResults();
                return;
            }

            if (turn == 1)
            {
                turn = 2;
                lbl_playerTurn.Content = "X's Turn";
            }
            else
            {
                turn = 1;
                lbl_playerTurn.Content = "O's Turn";
            }

            if (game_mode == "computer" && turn == 1)
            {
                TakeComputerTurn();
                if (CheckGameStatus() != 0)
                {
                    GameOverResults();
                }
            }
        }

        private void TakeComputerTurn()
        {
            Boolean found_valid_move = false;
            int move_row, move_col;
            while (!found_valid_move)
            {
                move_row = rand.Next(0, 3);
                move_col = rand.Next(0, 3);
                lbl_playerTurn.Content = move_row + "," + move_col;
                if (game_board[move_row, move_col] == -1)
                {
                    TakeTurn(move_row, move_col);
                    found_valid_move = true;

                }
            }
            turn = 2;
        }

        private void GameOverResults()
        {
            if (CheckGameStatus() == 3)
            {
                lbl_playerTurn.Content = "Tie Game!";
            }
            else if (game_mode == "computer" && CheckGameStatus() == 1)
            {
                lbl_playerTurn.Content = "Computer Wins!";
            }
            else
            {
                lbl_playerTurn.Content = "Player " + CheckGameStatus() + " wins!";
            }
            game_active = false;
        }

        private void canvas_topRowLeftCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(0, 0);
            }
        }

        private void canvas_topRowMidCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(0, 1);
            }
        }

        private void canvas_topRowRightCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(0, 2);
            }
        }

        private void canvas_midRowLeftCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(1, 0);
            }
        }

        private void canvas_midRowMidCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(1, 1);
            }
        }

        private void canvas_midRowRightCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(1, 2);
            }
        }

        private void canvas_botRowLeftCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(2, 0);
            }
        }

        private void canvas_botRowMidCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(2, 1);
            }
        }

        private void canvas_botRowRightCol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game_active)
            {
                TakeTurn(2, 2);
            }
        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnu_playComputer_Checked(object sender, RoutedEventArgs e)
        {
            radioBtn_playComputer.IsChecked = true;
            game_mode = "computer";
            btn_coinToss.IsEnabled = true;
        }

        private void mnu_twoPlayers_Checked(object sender, RoutedEventArgs e)
        {
            radioBtn_twoPlayers.IsChecked = true;
            game_mode = "2players";
            btn_coinToss.IsEnabled = true;
        }

        private void mnu_coinToss_Click(object sender, RoutedEventArgs e)
        {
            btn_reset.IsEnabled = true;
            mnu_reset.IsEnabled = true;
            btn_coinToss.IsEnabled = false;
            mnu_coinToss.IsEnabled = false;
            radioBtn_playComputer.IsEnabled = false;
            mnu_playComputer.IsEnabled = false;
            radioBtn_twoPlayers.IsEnabled = false;
            mnu_twoPlayers.IsEnabled = false;
            ResetGame();
            StartGame();
        }

        private void mnu_reset_Click(object sender, RoutedEventArgs e)
        {
            lbl_playerTurn.Content = "";
            game_active = false;
            ResetGame();
            btn_reset.IsEnabled = false;
            mnu_reset.IsEnabled = false;
            btn_coinToss.IsEnabled = true;
            mnu_coinToss.IsEnabled = true;
            radioBtn_playComputer.IsEnabled = true;
            mnu_playComputer.IsEnabled = true;
            radioBtn_twoPlayers.IsEnabled = true;
            mnu_twoPlayers.IsEnabled = true;
        }

        private void mnu_howToPlay_Click(object sender, RoutedEventArgs e)
        {
            string instructions = "Matthew Kerr\n"
                                + "Eastern Washington University\n"
                                + "Winter 2015\n\n"
                                + "Version 1.0.0.0\n\n"
                                + "The objective of Tic Tac Toe is to line up three in a row\n"
                                + "either horizontal, vertical, or diagonal.\n"
                                + "A tie occurs if all nine squares are filled up with no winner.\n"
                                + "Players take turns alternating placing X's and O's on the game board.\n\n"
                                + "Choose to either play against the computer or with 2 human players.\n"
                                + "Then click \"Coin Toss\" to begin the game.\n"
                                + "Click \"Reset\" to abandon the current game.";
            MessageBox.Show(instructions);
        }

        







    }
}
