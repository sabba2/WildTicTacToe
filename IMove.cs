using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoPlayerBoardGames
{
    public interface IMove
    {
        void Execute();
        void Undo();
    }
}
