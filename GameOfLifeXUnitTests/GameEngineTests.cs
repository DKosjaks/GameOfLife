namespace GameOfLifeXUnitTests
{
    using Xunit;
    using GameOfLife;

    public class GameEngineTests
    {
        private GameEngine _gameEngine;

        public GameEngineTests()
        {
            _gameEngine = new GameEngine();
        }

        [Fact]
        public void InitRandom_Result_NotNull()
        {
            var result = _gameEngine.InitRandom(1, 20, 20);

            Assert.NotNull(result);
        }

        [Fact]
        public void Iterate_PredefinedShapesResults_AreValid()
        {
            var blockGame = InitBlockGame();
            var blinkerGame = InitBlinkerGame();

            _gameEngine.Iterate(blockGame);
            _gameEngine.Iterate(blinkerGame);

            Assert.Equal(InitBlockGame().Grid, blockGame.Grid);
            Assert.Equal(CellEnum.Alive, blinkerGame.Grid[2, 1]);
            Assert.Equal(CellEnum.Alive, blinkerGame.Grid[2, 2]);
            Assert.Equal(CellEnum.Alive, blinkerGame.Grid[2, 3]);
            Assert.Equal(CellEnum.Dead, blinkerGame.Grid[1, 2]);
            Assert.Equal(CellEnum.Dead, blinkerGame.Grid[3, 2]);
        }

        /// <summary>
        /// Create game with predefined block grid
        /// </summary>
        /// <returns></returns>
        private Game InitBlockGame()
        {
            var blockGame = new Game
            {
                Id = 1,
                Rows = 4,
                Columns = 4,
                Grid = new CellEnum[4, 4]
            };
            blockGame.Grid[1, 1] = CellEnum.Alive;
            blockGame.Grid[1, 2] = CellEnum.Alive;
            blockGame.Grid[2, 1] = CellEnum.Alive;
            blockGame.Grid[2, 2] = CellEnum.Alive;

            return blockGame;
        }

        /// <summary>
        /// Create game with predefined blinker grid
        /// </summary>
        /// <returns></returns>
        private Game InitBlinkerGame()
        {
            var blinkerGame = new Game
            {
                Id = 1,
                Rows = 5,
                Columns = 5,
                Grid = new CellEnum[5, 5]
            };
            blinkerGame.Grid[1, 2] = CellEnum.Alive;
            blinkerGame.Grid[2, 2] = CellEnum.Alive;
            blinkerGame.Grid[3, 2] = CellEnum.Alive;

            return blinkerGame;
        }
    }
}
