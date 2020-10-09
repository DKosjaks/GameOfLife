namespace GameOfLife
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Handles game logic and objects
    /// </summary>
    class GameEngine
    {
        private readonly UIManager _uiManager;

        public GameEngine()
        {
            _uiManager = new UIManager();
        }

        /// <summary>
        /// Inits and runs in parallel multiple games
        /// </summary>
        public void InitAllGames()
        {
            List<Game> games;

            if (_uiManager.IsFromFile())
            {
                games = FileManager.LoadState();
            }
            else
            {
                games = new List<Game>();
                int rows = _uiManager.GetRows();
                int columns = _uiManager.GetColumns();

                for (int i = 0; i < 1000; i++)
                    games.Add(InitRandom(rows, columns));
            }
            
            while (!Console.KeyAvailable)
            {
                _uiManager.DrawAllGames(games);
                Parallel.ForEach(games, game => { Iterate(game); game.IterationCount++; });
                Thread.Sleep(1000);
            }

            FileManager.SaveState(games);
        }

        /// <summary>
        /// Updates game grid based on game rules
        /// </summary>
        /// <param name="game"></param>
        private void Iterate(Game game)
        {
            var nextGrid = new CellEnum[game.Rows, game.Columns];

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
                                aliveNeighbors += game.Grid[row + i, column + j] == CellEnum.Alive ? 1 : 0;
                            }
                        }
                    }

                    var currentCell = game.Grid[row, column];

                    if (currentCell == CellEnum.Alive && aliveNeighbors < 2)
                    {
                        nextGrid[row, column] = CellEnum.Dead;
                    }
                    else if (currentCell == CellEnum.Alive && aliveNeighbors > 3)
                    {
                        nextGrid[row, column] = CellEnum.Dead;
                    }
                    else if (currentCell == CellEnum.Dead && aliveNeighbors == 3)
                    {
                        nextGrid[row, column] = CellEnum.Alive;
                    }
                    else
                    {
                        nextGrid[row, column] = currentCell;
                    }
                }

            game.Grid = nextGrid;
        }

        /// <summary>
        /// Init game object using random numbers generator
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private Game InitRandom(int rows, int columns)
        {
            var game = new Game();
            game.Rows = rows;
            game.Columns = columns;
            game.Grid = new CellEnum[rows, columns];

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    game.Grid[row, column] = (CellEnum)RandomNumberGenerator.GetInt32(0, 2);
                }
            }

            return game;
        }
    }
}
