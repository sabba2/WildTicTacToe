using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoPlayerBoardGames
{
    public class WildTicTacToeMove : IMove
    {
        private Board board;
        private int row, col;
        private char symbol;


        public WildTicTacToeMove(Board board, int row, int col, char symbol)
        {
            this.board = board;
            this.row = row;
            this.col = col;
            this.symbol = symbol;
        }

        public void Execute()
        {
            board.PlaceMove(row, col, symbol);
        }

        public void Undo()
        {
            board.UndoMove(row, col);
        }
    }
}
