using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    /// <summary>
    /// Class for all game file operations
    /// </summary>
    class FileManager
    {
        public int FileRows { get; private set; }
        public int FileColumns { get; private set; }

        private string currentStateFile = AppDomain.CurrentDomain.BaseDirectory + @"/current_state.txt";

        // Saves game cell grid to file
        public void SaveState(StringBuilder stringBuilder)
        {
            File.WriteAllText(currentStateFile, stringBuilder.ToString());
        }

        // Gets game cell grid info from file
        public string[] LoadState()
        {
            var fileContents = File.ReadAllLines(currentStateFile);
            FileRows = fileContents.Count();
            FileColumns = fileContents.First().Count();

            return fileContents;
        }
    }
}
