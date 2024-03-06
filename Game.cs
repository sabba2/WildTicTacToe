using System;
using System.ComponentModel.Design;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;
using static System.Console;

namespace TwoPlayerBoardGames
{
    public abstract class Game
    {
        protected Board wildTicTacToeBoard;
        protected HelpSystem helpSystem = new HelpSystem();
        protected Player player1;
        protected Player player2;
        protected Player currentPlayer;
        //protected GameFileManager fileManager = new GameFileManager();
        protected int gameMode;
        protected GameState gameState = GameState.NotStarted;
        protected Player winner;
        private Stack<IMove> undoStack = new Stack<IMove>();
        private Stack<IMove> redoStack = new Stack<IMove>();

        // Template method defining the game flow
        public void PlayGame()
        {
            Welcome();
            SetupGame();
            gameState = GameState.Ongoing;
            while (gameState == GameState.Ongoing)
            {
                wildTicTacToeBoard.Display();
                TakeTurn(currentPlayer);
                if (wildTicTacToeBoard.CheckWinCondition())
                {
                    wildTicTacToeBoard.Display();
                    gameState = GameState.Ended;
                    DisplayWinner(currentPlayer);
                }
                else if (wildTicTacToeBoard.isFull())
                {
                    wildTicTacToeBoard.Display();
                    gameState = GameState.Ended;
                    DisplayDraw();
                    break;
                }
                SwitchPlayer();
            }
        }

        public static bool IsValidInt(string input, int min, int max, out int result)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                result = 0;
                return false;
            }
            bool isValid = int.TryParse(input, out result);

            return isValid && result >= min && result <= max;
        }
        public string GetPlayerName(int playerNum)
        {
            string playerName;
            do
            {
                Write($"Player {playerNum}, please enter your name: ");
                playerName = ReadLine() ?? "";
            } while (!IsValidName(playerName));
            WriteLine();
            return playerName;
        }
        private bool IsValidName(string playerName)
        {
            return !string.IsNullOrWhiteSpace(playerName);
        }
        protected void ExecuteMove(IMove move)
        {
            move.Execute();
            undoStack.Push(move);
            redoStack.Clear(); // Clear the redo stack whenever a new move is executed
        }

        public bool Undo()
        {
            if (undoStack.Any())
            {
                IMove move = undoStack.Pop();
                move.Undo();
                redoStack.Push(move);
                WriteLine("The last move has been undone.");
                return true;

            }
            else
            {
                WriteLine("No moves to undo.");
                return false;
            }
        }
        public bool Redo()
        {
            if (redoStack.Any())
            {
                IMove move = redoStack.Pop();
                move.Execute();
                undoStack.Push(move);
                WriteLine("The last move has been redone.");
                return true;
            }
            else
            {
                WriteLine("No moves to redo.");
                return false;
            }
        }
        protected void SwitchPlayer()
        {
            currentPlayer = currentPlayer == player1 ? player2 : player1;
        }

        protected void DisplayWinner(Player currentPlayer)
        {
            WriteLine($"The winner is {currentPlayer.Name}!");
        }

        protected void DisplayDraw()
        {
            WriteLine("No moves left! The game has ended in a draw.");
        }
        protected abstract void Welcome();
        protected abstract void SetupGame();
        protected abstract void InitialiseHelpCommands();

        protected abstract void TakeTurn(Player player);

        protected abstract int DetermineGameMode();

        // Factory methods to be implemented by subclasses
        protected abstract Board CreateBoard();
        protected abstract Player CreatePlayer(PlayerType type, string name);
    }

}

