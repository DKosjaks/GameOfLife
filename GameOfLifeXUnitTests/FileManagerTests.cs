namespace GameOfLifeXUnitTests
{
    using Xunit;
    using GameOfLife;
    using System.Collections.Generic;
    using System.IO.Abstractions.TestingHelpers;

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
            var mockInputFile = new MockFileData(
                "[{\"Id\":0,\"Rows\":10,\"Columns\":10," +
                "\"Grid\":[[0,0,0,0,0,0,0,0,0,0],[0,0,0,1,1,0,0,0,0,0],[0,1,0,0,1,0,0,0,0,0]," +
                "[0,0,0,0,1,0,0,0,0,0],[0,1,0,1,1,1,1,0,0,0],[0,0,1,0,1,1,1,1,0,0]," +
                "[0,0,0,0,0,0,0,1,0,0],[0,0,0,0,0,0,0,1,0,0],[0,0,0,0,0,0,0,0,0,0]," +
                "[0,0,0,0,0,0,0,0,0,0]]," +
                "\"IterationCount\":2,\"CellCount\":17}]"
                );
            _mockFileSystem.AddFile(FileManager._currentStateFile, mockInputFile);

            var result = _fileManager.LoadState();

            Assert.IsType<List<Game>>(result);
            Assert.Single(result);
            Assert.Equal(10, result[0].Rows);
        }
    }
}
