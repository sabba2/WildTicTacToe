using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace TwoPlayerBoardGames
{
    public class WildTicTacToe : Game
    {
        protected int boardSize;
        public WildTicTacToe() : base()
        {
            boardSize = 3;
        }

        protected override Board CreateBoard()
        {
            return new WildTicTacToeBoard(boardSize); // Assuming a 3x3 board for Tic-Tac-Toe
        }


        protected override Player CreatePlayer(PlayerType type, string name)
        {
            switch (type)
            {
                case PlayerType.Human:
                    return new WildTicTacToeHumanPlayer(name, wildTicTacToeBoard);
                case PlayerType.Computer:
                    return new WildTicTacToeComputerPlayer(name, wildTicTacToeBoard);
                default:
                    throw new ArgumentException("Unsupported player type");
            }
        }
        protected override void Welcome()
        {
            WriteLine("Welcome to Wild TicTacToe!\n");
            WriteLine("General rules and instructions:");
            WriteLine(" - Players take turns placing either 'X' or 'O' on the 3x3 grid.");
            WriteLine(" - You can place a symbol on the board by entering a command in the format of \"[row] [column] [symbol]\"");
            WriteLine(" - First player to make either a diagonal or line of three symbols wins the game!");
            WriteLine(" - Type help for more assistance on rules and commands.\n");
        }
        protected override int DetermineGameMode()
        {
            int lowerBound = 1;
            int upperBound = 2;
            int gameMode;
            WriteLine("Select a game mode by entering a number");
            WriteLine("  1 -> Human vs Human");
            WriteLine("  2 -> Human vs Computer");
            Write("Enter a number: ");

            string input = ReadLine() ?? "";

            while (!IsValidInt(input, lowerBound, upperBound, out gameMode))
            {
                Write($"Invalid input, please enter a number from {lowerBound} to {upperBound}: ");
                input = ReadLine() ?? "";
            }
            WriteLine();
            return gameMode;
        }

        protected override void SetupGame()
        {
            gameMode = DetermineGameMode();
            //fileManager = CreateGameFileManager();
            InitialiseHelpCommands();
            wildTicTacToeBoard = CreateBoard();

            string player1Name = GetPlayerName(1);
            string player2Name = gameMode == 1 ? GetPlayerName(2) : "Computer";

            player1 = CreatePlayer(PlayerType.Human, player1Name);
            if (gameMode == 1)
            {
                player2 = CreatePlayer(PlayerType.Human, player2Name);
            }
            else
            {
                player2 = CreatePlayer(PlayerType.Computer, player2Name);
            }
            currentPlayer = player1;
        }
        // Adding WildTicTacToe specific commands 
        protected override void InitialiseHelpCommands()
        {
            helpSystem.AddCommand("rules", "Displays the rules of the game.");
            helpSystem.AddCommand("[row] [col] [symbol]", "Places either a 'X' or 'O' symbol on a specified location on the board. e.g. 2 1 X");
            helpSystem.AddCommand("undo", "Allows a player to undo their previous move.");
            helpSystem.AddCommand("redo", "Allows a player to redo their previous move.");
            helpSystem.AddCommand("save", "Saves the current game state to a file.");
            helpSystem.AddCommand("load", "Loads a game state from a file.");
        }

        protected override void TakeTurn(Player currentPlayer)
        {
            bool turnCompleted = false;
            bool isCommandProcessed = false; // Flag to track if a non-turn-ending command was processed

            while (!turnCompleted) // Use a loop to ensure the turn does not end on invalid input
            {
                if (currentPlayer is WildTicTacToeComputerPlayer)
                {
                    WriteLine($"{currentPlayer.Name}'s turn. {currentPlayer.Name} is thinking...");
                    Thread.Sleep(1200);
                }
                else
                {
                    Write($"{currentPlayer.Name}'s turn. Enter your move: ");
                }
                string commandOrMove = currentPlayer.GetCommand();
                WriteLine();
                // Reset flag at the start of each loop iteration
                isCommandProcessed = false;

                // Check if it's a special command (undo, redo) or a move
                if (HandleSpecialCommands(commandOrMove, ref isCommandProcessed))
                {
                    turnCompleted = true; // A special command like undo/redo ends the turn
                }
                else if (!isCommandProcessed && TryParseAndExecuteMove(commandOrMove))
                {
                    turnCompleted = true; // Valid move ends the turn
                }
                else if (!isCommandProcessed)
                {
                    WriteLine("Invalid move. Type \"help\" if you need assistance."); // Invalid move, loop continues
                }
                // Let it stay Human's turn if they undo or redo against a computer for better game flow.
                if ((commandOrMove == "undo" || commandOrMove == "redo") && (player2 is WildTicTacToeComputerPlayer))
                {
                    turnCompleted = false;
                    wildTicTacToeBoard.Display();
                }
                // If isCommandProcessed is true but we're here, it means a valid non-turn-ending command was processed
            }
        }

        private bool TryParseAndExecuteMove(string input)
        {
            // Parse the input into components (e.g., "2 3 X")
            var parts = input.Split();
            if (parts.Length == 3 && int.TryParse(parts[0], out int row) && int.TryParse(parts[1], out int col) && (parts[2].ToUpper() == "X" || parts[2].ToUpper() == "O"))
            {
                char symbol = parts[2].ToUpper()[0];
                // Adjust row and col to 0-based index if necessary
                row--; col--;

                // Validate the move using the board's method
                if (wildTicTacToeBoard.IsValidCell(row, col))
                {
                    // If the move is valid and successfully placed, create and execute a move command
                    IMove wildTicTacToeMove = new WildTicTacToeMove(wildTicTacToeBoard, row, col, symbol);
                    ExecuteMove(wildTicTacToeMove);

                    return true; // Move executed successfully
                }
            }
            return false; // Invalid move format or move not allowed
        }


        private bool HandleSpecialCommands(string command, ref bool isCommandProcessed)
        {
            switch (command.ToLower())
            {
                case "help":
                    helpSystem.DisplayHelp();
                    isCommandProcessed = true;
                    return false;
                case "rules":
                    helpSystem.DisplayRules();
                    isCommandProcessed = true;
                    return false;
                case "undo":
                    return Undo();
                case "redo":
                    return Redo();
                //case "save":
                //    return
                //case "load":
                //    return
                default:
                    return false; // Not a special command, proceed to move processing
            }
        }
    }
}
