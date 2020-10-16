namespace GameOfLifeXUnitTests
{
    using Xunit;
    using GameOfLife;
    using System.Collections.Generic;
    using System.IO.Abstractions.TestingHelpers;
    using Newtonsoft.Json;

    public class FileManagerTests
    {
        private MockFileSystem _mockFileSystem;
        private FileManager _fileManager;

        public FileManagerTests()
        {
            _mockFileSystem = new MockFileSystem();
            _fileManager = new FileManager(_mockFileSystem);
        }

        [Fact]
        public void LoadState_Returns_ListOfGames()
        {
            var mockInputFile = new MockFileData(JsonConvert.SerializeObject(InitTestList()));
            _mockFileSystem.AddFile(FileManager._currentStateFile, mockInputFile);

            var result = _fileManager.LoadState();

            Assert.IsType<List<Game>>(result);
            Assert.Single(result);
            Assert.Equal(4, result[0].Rows);
            Assert.Equal(CellEnum.Alive, result[0].Grid[2,2]);
        }

        /// <summary>
        /// Prepare a list of games for testing
        /// </summary>
        /// <returns>Returns a list of games</returns>
        private List<Game> InitTestList()
        {
            var grid = new CellEnum[4, 4];
            grid[1, 1] = CellEnum.Alive;
            grid[1, 2] = CellEnum.Alive;
            grid[2, 1] = CellEnum.Alive;
            grid[2, 2] = CellEnum.Alive;

            var games = new List<Game>
            {
                new Game { Id = 0, Rows = 4, Columns = 4, Grid = grid }
            };

            return games;
        }
    }
}
