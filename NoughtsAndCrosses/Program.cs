using System;

namespace NoughtsAndCrosses
{
    class Program //make method to clear screen  and store the shit
    {
        private static readonly string EOL = Environment.NewLine;

        private static readonly string[] board = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        private static bool isP1 = true;

        private static bool victory = false;

        private static bool staleMate = false;

        private static string player1 = "Player 1";
        
        private static string player2 = "Player 2";

        private static string winnerShape = "";

        static void Main()
        {
            GetPlayerNames();
            while (!victory)
            {
                MakeBoard();
                ProcessTurn();
                CheckForVictory();
                CheckForStaleMate();
                TogglePlayer();
            }

            MakeBoard();
            DisplayWinner();
            Console.ReadKey();
        }

        private static void CheckForStaleMate()
        {
            if (board[0] != "1"  && board[1] != "2" && board[2] != "3" && board[3] != "4" && board[4] != "5" && board[5] != "6" && board[6] != "7" && board[7] != "8" && board[8] != "9")
            {
                CheckForStaleMate = true;
            }
            
        }

        private static void DisplayWinner()
        {
            if (staleMate == false)
            {
                if (winnerShape == "X")
                {
                    Console.WriteLine("Congratulations " + player1 + "," + EOL + "You Are The Winner!");
                }
                else
                {
                    Console.WriteLine("Congratulations " + player2 + "," + EOL + "You Are The Winner!");
                }
            }
            else
            {
                Console.WriteLine("Game Over" + EOL + "No Winners This Time");
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
                    victory = true;
                }
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
                errorMessage = "try again dipshit";
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
                return AskForInt(question, "Try Again!");
            }

            return value;
        }

        private static void MakeBoard()
        {
            ClearScreen();
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
