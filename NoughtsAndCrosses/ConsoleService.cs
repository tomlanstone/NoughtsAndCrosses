using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NoughtsAndCrosses
{
    public class ConsoleService
    {
        private static readonly string EOL = Environment.NewLine;
        private readonly string[] yesValues = new string[] { "y", "yes", "true", "1" };

        public void Print(string line = "")
        {
            Console.WriteLine(line);
        }

        public string AskQuestion(string question, string errorMessage = "")
        {
            Console.Write(EOL + errorMessage + EOL + EOL + question + EOL + "--> ");
            return Console.ReadLine();
        }
        
        public bool AskYesNo(string question)
        {
            string answer = AskQuestion(question);
            return yesValues.Contains(answer.ToLower());
        }

        public void ResetConsoleColors()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            ClearScreen();
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}
