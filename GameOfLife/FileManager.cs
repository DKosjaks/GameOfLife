namespace GameOfLife
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class for all game file operations
    /// </summary>
    class FileManager
    {
        public int FileRows { get; private set; }
        public int FileColumns { get; private set; }

        private string currentStateFile = AppDomain.CurrentDomain.BaseDirectory + @"/current_state.txt";

        /// <summary>
        /// Save game grid to a file
        /// </summary>
        /// <param name="stringBuilder"></param>
        public void SaveState(StringBuilder stringBuilder)
        {
            File.WriteAllText(currentStateFile, stringBuilder.ToString());
        }

        /// <summary>
        /// Gets game cell grid info from file
        /// </summary>
        /// <returns></returns>
        public string[] LoadState()
        {
            var fileContents = File.ReadAllLines(currentStateFile);
            FileRows = fileContents.Count();
            FileColumns = fileContents.First().Count();

            return fileContents;
        }
    }
}
