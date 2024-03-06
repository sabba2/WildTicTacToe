using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TwoPlayerBoardGames
{
    public class WildTicTacToeBoard : Board
    {
        public WildTicTacToeBoard(int size) : base(size) { }

        public override bool PlaceMove(int row, int col, char playerSymbol)
        {
            // Check if the move is within bounds and the cell is empty
            if (row >= 0 && row < Size && col >= 0 && col < Size && grid[row, col] == ' ')
            {
                grid[row, col] = playerSymbol;
                occupiedPositions++; // Increment the count of occupied positions
                return true; // Move placed successfully
            }
            return false; // Move was invalid
        }
        public override void UndoMove(int row, int col)
        {
            if (row >= 0 && row < Size && col >= 0 && col < Size)
            {
                grid[row, col] = ' '; // Set the cell back to empty
                occupiedPositions--; // Decrement the count of occupied positions if necessary
            }
        }

        public override bool IsValidCell(int row, int col)
        {
            // First, check if the cell is within bounds using the base class implementation
            if (!base.IsValidCell(row, col))
            {
                return false; // The cell is out of bounds
            }

            // Then, check if the cell is already occupied by an 'X' or an 'O'
            char cellContent = grid[row, col];
            if (cellContent == 'X' || cellContent == 'O')
            {
                return false; // The cell is already occupied
            }

            return true; // The cell is valid (within bounds and not occupied)
        }

        public override bool CheckWinCondition()
        {
            // Check rows and columns for a win
            for (int i = 0; i < Size; i++)
            {
                if (CheckRow(i) || CheckColumn(i))
                {
                    return true;
                }
            }

            // Check diagonals for a win
            return CheckDiagonals();
        }

        public override void Display()
        {
            WriteLine("Current Board:");
            WriteLine("  +---+---+---+"); // Adjust based on the board size
            for (int i = 0; i < Size; i++)
            {
                Write("  |");
                for (int j = 0; j < Size; j++)
                {
                    Write($" {grid[i, j]} |");
                }
                WriteLine("\n  +---+---+---+"); // Repeat the border after each row
            }
            WriteLine();
        }

        private bool CheckColumn(int col)
        {
            char firstSymbol = grid[0, col];
            if (firstSymbol == ' ') return false; // Empty cell, can't be a winning column

            for (int row = 1; row < Size; row++)
            {
                if (grid[row, col] != firstSymbol) return false;
            }
            return true;
        }
        private bool CheckRow(int row)
        {
            char firstSymbol = grid[row, 0];
            if (firstSymbol == ' ') return false; // Empty cell, can't be a winning row

            for (int col = 1; col < Size; col++)
            {
                if (grid[row, col] != firstSymbol) return false;
            }
            return true;
        }

        private bool CheckDiagonals()
        {
            // Check the first diagonal
            char firstDiagonalSymbol = grid[0, 0];
            bool firstDiagonalWin = firstDiagonalSymbol != ' '; // Assume win until proven otherwise
            for (int i = 1; i < Size; i++)
            {
                if (grid[i, i] != firstDiagonalSymbol)
                {
                    firstDiagonalWin = false;
                    break;
                }
            }

            // Check the second diagonal
            char secondDiagonalSymbol = grid[0, Size - 1];
            bool secondDiagonalWin = secondDiagonalSymbol != ' '; // Assume win until proven otherwise
            for (int i = 1; i < Size; i++)
            {
                if (grid[i, Size - i - 1] != secondDiagonalSymbol)
                {
                    secondDiagonalWin = false;
                    break;
                }
            }

            return firstDiagonalWin || secondDiagonalWin;
        }

    }
}
