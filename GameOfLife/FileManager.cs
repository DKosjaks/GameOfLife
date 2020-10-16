namespace GameOfLife
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;

    /// <summary>
    /// Class for all game file operations
    /// </summary>
    public class FileManager
    {
        public static string _currentStateFile = AppDomain.CurrentDomain.BaseDirectory + @"/current_state.txt";

        private readonly IFileSystem _fileSystem;

        public FileManager() : this(new FileSystem()) { }

        /// <summary>
        /// Class for all game file operations
        /// </summary>
        public FileManager(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Saves all game data to a file
        /// </summary>
        /// <param name="games">List of games</param>
        public void SaveState(List<Game> games)
        {
            var json = JsonConvert.SerializeObject(games);
            _fileSystem.File.WriteAllText(_currentStateFile, json);
        }

        /// <summary>
        /// Reads game data from file
        /// </summary>
        /// <returns>List of games</returns>
        public List<Game> LoadState()
        {
            var fileContents = _fileSystem.File.ReadAllText(_currentStateFile);
            var games = JsonConvert.DeserializeObject<List<Game>>(fileContents);

            return games;
        }
    }
}
