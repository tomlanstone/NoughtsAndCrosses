﻿using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace NoughtsAndCrosses
{
    class Program 
    {
        private static readonly string EOL = Environment.NewLine;

        private static readonly ConsoleService consoleService = new ConsoleService();

        private static readonly string[] boardTemplate = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        private static readonly string[] board = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        private static readonly string[] yesOptions = { "y", "yes" };

        private static readonly string[] noOptions = { "n", "no" };

        private static bool isP1 = true;

        private static bool gameOver = false;

        private static bool stalemate = false;

        private static bool playAgain = true;

        private static string player1 = "Player 1";
        
        private static string player2 = "Player 2";

        private static string winnerShape = "";

        private static string playAgainAnswer = string.Empty;

        private static int player1Score = 0;

        private static int player2Score = 0;

        private static int stalemateCount = 0;
        static void Main()
        {
            consoleService.ResetConsoleColors();
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
                PrintGame();
                ProcessTurn();
                CheckForVictory();
                CheckForStalemate();
                TogglePlayer();
            }

            PrintGame();
            DisplayWinner();
            consoleService.AskQuestion("Press enter to continue");
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
            playAgain = consoleService.AskYesNo("Play Again?" + EOL + "  (Y/N)");
        }

        private static void DisplayWinner()
        {
            if (stalemate == false)
            {
                if (winnerShape == "X")
                {
                    consoleService.Print("Congratulations ");
                    consoleService.PrintPlayerColor(player1, Player.One);
                    consoleService.Print("," + EOL + "You Are The Winner!");
                    player1Score++;
                }
                else
                {
                    consoleService.Print("Congratulations ");
                    consoleService.PrintPlayerColor(player2, Player.Two);
                    consoleService.Print("," + EOL + "You Are The Winner!");
                    player2Score++;
                }
            }
            else
            {
                consoleService.PrintLine(EOL + "Game Over" + EOL + "No Winners This Time");
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
            if (board[0] != "1" && board[1] != "2" && board[2] != "3" && board[3] != "4" && board[4] != "5" && board[5] != "6" && board[6] != "7" && board[7] != "8" && board[8] != "9" && gameOver == false)
            {
                gameOver = true;
                stalemate = true;
            }

        }

        private static void GetPlayerNames()
        {
            player1 = consoleService.AskQuestion(player1 + ", enter your name:");
            consoleService.ClearScreen();
            player2 = consoleService.AskQuestion(player2 + ", enter your name:");
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
                PrintGame();
                if (isP1)
                {
                    consoleService.PrintPlayerColor(player1, Player.One);
                    move = AskForInt(", enter your move", errorMessage);
                }
                else
                {
                    consoleService.PrintPlayerColor(player2, Player.Two);
                    move = AskForInt(", enter your move", errorMessage);
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

        private static int AskForInt(string question, string errorMessage = "")
        {
            if (!int.TryParse(consoleService.AskQuestion(question, errorMessage), out int value))
            {
                PrintGame();
                return AskForInt(question, "Try Again");
            }

            return value;
        }

        private static void PrintGame()
        {
            consoleService.ClearScreen();
            PrintScores();
            consoleService.PrintLine(EOL);
            PrintPlayerSymbols();
            consoleService.PrintLine(EOL);
            PrintBoard();
        }

        private static void PrintBoard()
        {
            /*consoleService.PrintLine("-------------");
            consoleService.PrintLine("| " + board[0] + " | " + board[1] + " | " + board[2] + " |");
            consoleService.PrintLine("-------------");
            consoleService.PrintLine("| " + board[3] + " | " + board[4] + " | " + board[5] + " |");
            consoleService.PrintLine("-------------");
            consoleService.PrintLine("| " + board[6] + " | " + board[7] + " | " + board[8] + " |");
            consoleService.PrintLine("-------------");
*/
            consoleService.PrintLine("-------------");
            for (int i = 0; i < board.Length; i++)
            {
                consoleService.Print("| ");
                PrintMove(board[i]);
                consoleService.Print(" ");

                if ((i + 1) % 3 == 0)
                {
                    consoleService.PrintLine("|" + EOL + "-------------");
                }
            }
        }

        private static void PrintMove(string move)
        {
            switch (move)
            {
                case "X":
                    consoleService.PrintPlayerColor(move, Player.One);
                    return;
                case "O":
                    consoleService.PrintPlayerColor(move, Player.Two);
                    return;
                default:
                    consoleService.Print(move);
                    return;
            }
        }
            
        private static void PrintPlayerSymbols()
        {
            consoleService.PrintPlayerColor(player1, Player.One);
            consoleService.Print(" = ");
            consoleService.PrintPlayerColor("X" + EOL, Player.One);
            consoleService.PrintPlayerColor(player2, Player.Two);
            consoleService.Print(" = ");
            consoleService.PrintPlayerColor("O" + EOL, Player.Two);
        }

        private static void PrintScores()
        {
            consoleService.PrintPlayerColor(player1 + " " + player1Score, Player.One);
            consoleService.Print(":");
            consoleService.PrintPlayerColor(player2Score + " " + player2 + EOL, Player.Two);
            consoleService.PrintLine(EOL);
            consoleService.PrintLine("Stalemates: " + stalemateCount);
        }
    }
}
