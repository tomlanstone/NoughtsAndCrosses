using System;
using System.Runtime.CompilerServices;

namespace NoughtsAndCrosses
{
    class Program 
    {
        private static readonly string EOL = Environment.NewLine;

        private static readonly string[] boardTemplate = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        private static readonly string[] board = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        private static bool isP1 = true;

        private static bool gameOver = false;

        private static bool stalemate = false;

        private static bool playAgain = true;

        private static string player1 = "Player 1";
        
        private static string player2 = "Player 2";

        private static string winnerShape = "";

        private static string playAgainAnswer = "";

        private static int player1Score = 0;

        private static int player2Score = 0;

        private static int stalemateCount = 0;

        static void Main()
        {
            GetPlayerNames();
            while (playAgain)
            {
                GameLoop();
            }
        }

        private static void GameLoop()
        {
            while (!gameOver)
            {
                MakeBoard();
                ProcessTurn();
                CheckForVictory();
                CheckForStalemate();
                TogglePlayer();
            }

            MakeBoard();
            DisplayWinner();
            TogglePlayAgain();
            ResetBoard();
        }

        private static void ResetBoard()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = boardTemplate[i];
                gameOver = false;
                stalemate = false;
                playAgainAnswer = "";
                winnerShape = "";
            }
        }

        private static void TogglePlayAgain()
        {
            string errorMessage = "";
            while (playAgainAnswer != "Y" && playAgainAnswer != "N" && playAgainAnswer != "y" && playAgainAnswer != "n" && playAgainAnswer != "yes" && playAgainAnswer != "no" && playAgainAnswer != "Yes" && playAgainAnswer != "No")
            {
                MakeBoard();
                playAgainAnswer = AskQuestion("Play Again?" + EOL + "  (Y/N)", errorMessage);
                errorMessage = "Try Again";
            }
            if (playAgainAnswer == "Y" || playAgainAnswer == "y" || playAgainAnswer == "yes" || playAgainAnswer == "Yes")
            {
                playAgain = true;
            }
            if (playAgainAnswer == "N" || playAgainAnswer == "n" || playAgainAnswer == "no" || playAgainAnswer == "No")
            {
                playAgain = false;
            }
        }

        private static void DisplayWinner()
        {
            if (stalemate == false)
            {
                if (winnerShape == "X")
                {
                    Console.WriteLine("Congratulations " + player1 + "," + EOL + "You Are The Winner!");
                    player1Score++;
                }
                else
                {
                    Console.WriteLine("Congratulations " + player2 + "," + EOL + "You Are The Winner!");
                    player2Score++;
                }
            }
            else
            {
                Console.WriteLine(EOL + "Game Over" + EOL + "No Winners This Time");
                stalemateCount++;
            } 
        }

        private static void CheckForVictory()
        {
            int[][] patterns =
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            for (int i = 0; i < patterns.Length; i++)
            {
                if (board[patterns[i][0]] == board[patterns[i][1]] && board[patterns[i][1]] == board[patterns[i][2]])
                {
                    winnerShape = board[patterns[i][0]];
                    gameOver = true;
                }
            }
        }

        private static void CheckForStalemate()
        {
            if (board[0] != "1" && board[1] != "2" && board[2] != "3" && board[3] != "4" && board[4] != "5" && board[5] != "6" && board[6] != "7" && board[7] != "8" && board[8] != "9")
            {
                gameOver = true;
                stalemate = true;
            }

        }

        private static void GetPlayerNames()
        {
            player1 = AskQuestion(player1 + ", enter your name:");
            ClearScreen();
            player2 = AskQuestion(player2 + ", enter your name:");
        }

        private static void TogglePlayer()
        {
            isP1 = !isP1;
        }

        private static void ProcessTurn()
        {
            string errorMessage = "";
            int move = -1;

            while (!ValidateMove(move))
            {
                MakeBoard();
                if (isP1)
                {
                    move = AskForInt(player1 + ", enter your move", errorMessage);
                }
                else
                {
                    move = AskForInt(player2 + ", enter your move", errorMessage);
                }
                move -= 1;
                errorMessage = "Try Again";
            }

            PlaceMove(move);
        }

        private static void PlaceMove(int move)
        {
            if (isP1)
            {
                board[move] = "X";
            }
            else
            {
                board[move] = "O";
            }
        }

        private static bool ValidateMove(int move)
        {
            if (move > 9 || move < 0)
            {
                return false;
            }

            return board[move] != "X" && board[move] != "O";
        }

        private static void ClearScreen()
        {
            Console.Clear();
        }

        private static string AskQuestion(string question, string errorMessage = "")
        {
            Console.Write(EOL + errorMessage + EOL + EOL + question + EOL + "--> ");
            return Console.ReadLine();
        }

        private static int AskForInt(string question, string errorMessage = "")
        {
            int value;

            if (!int.TryParse(AskQuestion(question, errorMessage), out value))
            {
                MakeBoard();
                return AskForInt(question, "Try Again");
            }

            return value;
        }

        private static void MakeBoard()
        {
            ClearScreen();
            Console.WriteLine(player1 + " " + player1Score + ":" + player2Score + " " + player2);
            Console.WriteLine(EOL);
            Console.WriteLine("Stalemates: " + stalemateCount);
            Console.WriteLine(EOL);
            Console.WriteLine(player1 + " = X");
            Console.WriteLine(player2 + " = O");
            Console.WriteLine(EOL);
            Console.WriteLine("-------------");
            Console.WriteLine("| " + board[0] + " | " + board[1] + " | " + board[2] + " |");
            Console.WriteLine("-------------");
            Console.WriteLine("| " + board[3] + " | " + board[4] + " | " + board[5] + " |");
            Console.WriteLine("-------------");
            Console.WriteLine("| " + board[6] + " | " + board[7] + " | " + board[8] + " |");
            Console.WriteLine("-------------");
        }
    }
}
