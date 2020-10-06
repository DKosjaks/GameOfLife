using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    /// <summary>
    /// Handles game logic and objects
    /// </summary>
    class GameEngine
    {
        private readonly List<Game> games;
        private FileManager fileManager;
        private UIManager uiManager;

        public GameEngine()
        {
            games = new List<Game>();
            fileManager = new FileManager();
            uiManager = new UIManager();
        }

        // Inits game object and starts game process
        public void InitGame()
        {
            var game = uiManager.IsFromFile() ?
                InitFromFile(fileManager.LoadState()) :
                InitRandom(uiManager.GetRows(), uiManager.GetColumns());

            Run(game);
        }

        // Starts game iteration infinite loop
        private void Run(Game game)
        {
            while (!Console.KeyAvailable)
            {
                Draw(game);
                Iterate(game);
                game.IterationCount++;
            }
        }

        // Draws current iteration on screen
        private void Draw(Game game)
        {
            var stringBuilder = new StringBuilder();

            for (var row = 0; row < game.Rows; row++)
            {
                for (var column = 0; column < game.Columns; column++)
                {
                    var cell = game.Grid[row, column];
                    stringBuilder.Append(cell == Cell.Alive ? 'O' : ' ');

                    if (cell == Cell.Alive)
                        game.CellCount++;
                }
                stringBuilder.Append("\n");
            }

            uiManager.ShowActiveGame(stringBuilder, game.IterationCount, game.CellCount);
            fileManager.SaveState(stringBuilder);
            Thread.Sleep(1000);
        }

        // Updates game grid based on game rules
        private void Iterate(Game game)
        {
            var nextGrid = new Cell[game.Rows, game.Columns];

            for (var row = 1; row < game.Rows - 1; row++)
                for (var column = 1; column < game.Columns - 1; column++)
                {
                    var aliveNeighbors = 0;
                    for (var i = -1; i <= 1; i++)
                    {
                        for (var j = -1; j <= 1; j++)
                        {
                            if (i != 0 || j != 0)
                            {
                                aliveNeighbors += game.Grid[row + i, column + j] == Cell.Alive ? 1 : 0;
                            }
                        }
                    }

                    var currentCell = game.Grid[row, column];

                    if (currentCell == Cell.Alive && aliveNeighbors < 2)
                    {
                        nextGrid[row, column] = Cell.Dead;
                    }
                    else if (currentCell == Cell.Alive && aliveNeighbors > 3)
                    {
                        nextGrid[row, column] = Cell.Dead;
                    }
                    else if (currentCell == Cell.Dead && aliveNeighbors == 3)
                    {
                        nextGrid[row, column] = Cell.Alive;
                    }
                    else
                    {
                        nextGrid[row, column] = currentCell;
                    }
                }

            game.Grid = nextGrid;
        }

        // Init game object from file data
        private Game InitFromFile(string[] fileContents)
        {
            var rows = fileManager.FileRows;
            var columns = fileManager.FileColumns;
            var game = new Game(rows, columns, new Cell[rows, columns]);

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    game.Grid[row, column] = (Cell)(fileContents[row][column].Equals('O') ? 1 : 0);
                }
            }

            return game;
        }

        // Init game object using random numbers generator
        private Game InitRandom(int rows, int columns)
        {
            var game = new Game(rows, columns, new Cell[rows, columns]);

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    game.Grid[row, column] = (Cell)RandomNumberGenerator.GetInt32(0, 2);
                }
            }

            return game;
        }
    }
}
