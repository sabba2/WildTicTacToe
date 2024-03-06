using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TwoPlayerBoardGames
{
    public class WildTicTacToeHumanPlayer : Player
    {
        public WildTicTacToeHumanPlayer(string name, Board board) : base(name, board) { }

        public override string GetCommand()
        {
            string input = "";

            input = ReadLine().Trim();
            // Prompt the player until a non-null and non-empty string is provided.
            while (string.IsNullOrEmpty(input))
            {
                WriteLine($"Input was empty.");
                WriteLine($"{Name}, please enter your move or enter \"help\" for assistance.");
                input = ReadLine().Trim();
            }

            return input;
        }


    }
}
