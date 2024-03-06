using System;
using static System.Console;

namespace TwoPlayerBoardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            Game wildTicTacToe = new WildTicTacToe();
            wildTicTacToe.PlayGame();
        }
    }
}
