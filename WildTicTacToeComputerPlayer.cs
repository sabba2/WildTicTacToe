using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TwoPlayerBoardGames
{

    public class WildTicTacToeComputerPlayer : Player
    {
        private Random _random = new Random();

        public WildTicTacToeComputerPlayer(string name, Board board) : base(name, board) { }

        public override string GetCommand()
        {
            int row, col;
            char symbol;
            bool isValidMove = false;

            // Keep trying until a valid move is generated
            do
            {
                // Generate a random position within the board's bounds
                row = _random.Next(0, 3); // Assuming a 3x3 board for simplicity
                col = _random.Next(0, 3);

                // Randomly choose between 'X' and 'O'
                symbol = _random.Next(0, 2) == 0 ? 'X' : 'O';

                // Check if the cell is unoccupied
                if (board.GetCellState(row, col) == ' ')
                {
                    isValidMove = true;
                }
            }
            while (!isValidMove);

            // Convert the move to a string format and return it
            return $"{row + 1} {col + 1} {symbol}"; // Adjust row and col to 1-based for consistency with human input
        }
    }

}
