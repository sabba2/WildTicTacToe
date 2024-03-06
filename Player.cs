using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoPlayerBoardGames
{
    public abstract class Player
    {

        public string Name { get; }
        protected Board board;
        protected Player(string name, Board board)
        {
            Name = name;
            this.board = board;
        }
        public abstract string GetCommand();

    }
}
