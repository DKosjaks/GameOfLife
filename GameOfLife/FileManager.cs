namespace GameOfLife
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Class for all game file operations
    /// </summary>
    public class FileManager
    {
        private static string _currentStateFile = AppDomain.CurrentDomain.BaseDirectory + @"/current_state.txt";

        /// <summary>
        /// Saves all game data to a file
        /// </summary>
        /// <param name="stringBuilder"></param>
        public static void SaveState(List<Game> games)
        {
            var json = JsonConvert.SerializeObject(games);
            File.WriteAllText(_currentStateFile, json);
        }

        /// <summary>
        /// Reads game data from file
        /// </summary>
        /// <returns></returns>
        public static List<Game> LoadState()
        {
            var fileContents = File.ReadAllText(_currentStateFile);
            var games = JsonConvert.DeserializeObject<List<Game>>(fileContents);

            return games;
        }
    }
}
