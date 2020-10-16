namespace GameOfLife
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Timers;

    /// <summary>
    /// Handles game logic and objects
    /// </summary>
    public class GameEngine
    {
        private Timer _timer;
        private readonly UIManager _uiManager;
        private readonly FileManager _fileManager;
        private List<Game> games;

        /// <summary>
        /// Handles game logic and objects
        /// </summary>
        public GameEngine()
        {
            _uiManager = new UIManager();
            _fileManager = new FileManager();
        }

        /// <summary>
        /// Inits games from file or randomly
        /// </summary>
        private void InitAllGames()
        {
            if (_uiManager.IsFromFile())
            {
                games = _fileManager.LoadState();
            }
            else
            {
                games = new List<Game>();
                var gridSize = _uiManager.GetGridSize();
                int rows = gridSize[0];
                int columns = gridSize[1];

                for (int i = 0; i < 1000; i++)
                {
                    games.Add(InitRandom(i, rows, columns));
                }
            }
        }

        /// <summary>
        /// Starts games iterations
        /// </summary>
        public void RunGames()
        {
            InitAllGames();

            var gameIds = _uiManager.GetGamesIds();
            var filteredGames = games.Where(game => gameIds.Contains(game.Id)).ToList();

            _timer = new Timer(1000);
            _timer.Elapsed += (sender, e) => OnTimedGameEvent(filteredGames);
            _timer.AutoReset = true;
            _timer.Enabled = true;

            Console.ReadKey();
            _timer.Stop();
            _timer.Dispose();
            _fileManager.SaveState(games);
        }

        /// <summary>
        /// Draws and iterates games on timer
        /// </summary>
        /// <param name="filteredGames">List of games filtered by console input ids</param>
        private void OnTimedGameEvent(List<Game> filteredGames)
        {
            _uiManager.DrawAllGames(filteredGames.Count > 0 ? filteredGames : games,
                    games.Count,
                    games.Sum(game => game.CellCount));
            Parallel.ForEach(games, game =>
            {
                Iterate(game);
                game.IterationCount++;
            });
        }

        /// <summary>
        /// Updates game grid based on game rules
        /// </summary>
        /// <param name="game">Game object</param>
        public void Iterate(Game game)
        {
            var nextGrid = new CellEnum[game.Rows, game.Columns];

            for (var row = 1; row < game.Rows - 1; row++)
            {
                for (var column = 1; column < game.Columns - 1; column++)
                {
                    var aliveNeighbors = GetAliveNeighbors(game, row, column);
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
            }

            game.Grid = nextGrid;
            game.CellCount = nextGrid.Cast<int>().Sum();
        }

        /// <summary>
        /// Count alive cell neighbors
        /// </summary>
        /// <param name="game">Game object</param>
        /// <param name="row">Current row</param>
        /// <param name="column">Current column</param>
        /// <returns>Number of alive neighbor cells</returns>
        private int GetAliveNeighbors(Game game, int row, int column)
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

            return aliveNeighbors;
        }

        /// <summary>
        /// Init game object using random numbers generator
        /// </summary>
        /// <param name="id">Game unique id</param>
        /// <param name="rows">Total grid rows</param>
        /// <param name="columns">Total grid columns</param>
        /// <returns>Game object</returns>
        public Game InitRandom(int id, int rows, int columns)
        {
            var game = new Game
            {
                Id = id,
                Rows = rows,
                Columns = columns,
                Grid = new CellEnum[rows, columns]
            };

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    game.Grid[row, column] = (CellEnum)RandomNumberGenerator.GetInt32(0, 2);
                    game.CellCount += game.Grid[row, column] == CellEnum.Alive ? 1 : 0;
                }
            }

            return game;
        }
    }
}
