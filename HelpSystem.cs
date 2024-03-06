using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TwoPlayerBoardGames
{
    public class HelpSystem
    {
        private Dictionary<string, string> commands = new Dictionary<string, string>();

        public HelpSystem()
        {
            commands.Add("help", "Displays all available commands.");
        }

        public void AddCommand(string command, string description)
        {
            commands[command] = description; // Adds or updates the command description
        }

        public void DisplayHelp()
        {
            WriteLine("Available commands:");
            foreach (var command in commands)
            {
                WriteLine($"  {command.Key}: {command.Value}");
            }
            WriteLine();
        }

        public void DisplayRules()
        {
            WriteLine("Wild Tic Tac Toe is a variation of Tic Tac Toe, where players can choose to place either 'X' or 'O' on each turn.");
            WriteLine("Players take turns placing symbols on empty spaces on the grid.");
            WriteLine("Players can win by forming a row, column, or diagonal of 3 in a row of the same symbol.");
            WriteLine("The game is over when either the board is full, or someone can form three of the same symbol in a row.\n");
        }
    }

}
