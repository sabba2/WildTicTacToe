using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TwoPlayerBoardGames
{
    public abstract class Board
    {
        public int Size { get; private set; } // Represents the size of the board (e.g., 3x3)
        protected char[,] grid; // Stores the current state of the board
        protected int occupiedPositions = 0;

        // Initialize a new board of given size
        public Board(int size)
        {
            Size = size;
            grid = new char[size, size];
            InitializeBoard();
        }
        // Set up the board with an initial state
        protected void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    grid[i, j] = ' ';
                }
            }
        }
        // Provides the state of a specific cell without exposing the entire grid
        public char GetCellState(int row, int col)
        {
            if (row >= 0 && row < Size && col >= 0 && col < Size)
            {
                return grid[row, col];
            }
            throw new ArgumentOutOfRangeException("Row or column is out of the board's bounds.");
        }

        // Display the current state of the board
        public abstract void Display();

        public virtual bool IsValidCell(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }


        // Make a move on the board
        public abstract bool PlaceMove(int row, int col, char playerSymbol);
        public abstract void UndoMove(int row, int col);

        // Method stub for checking win conditions - to be implemented in derived classes
        public abstract bool CheckWinCondition();
        public bool isFull()
        {
            return occupiedPositions == Size * Size;
        }

        public char GetCell(int row, int col)
        {
            // Add validation if necessary.
            return grid[row, col];
        }
        public void SetCell(int row, int col, char value)
        {
            // Add validation if necessary.
            grid[row, col] = value;
        }

    }
}
