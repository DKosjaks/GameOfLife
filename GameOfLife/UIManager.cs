using System;
using System.Text;

namespace GameOfLife
{
    class UIManager
    {
        // Displays game grid and statistics
        public void ShowActiveGame(StringBuilder stringBuilder, int iterationCount, int cellCount)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Iteration: {iterationCount}");
            Console.WriteLine($"Cells: {cellCount}");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Press any key to stop");
        }

        // Asks user if he wants to use file to load last game state
        public bool IsFromFile()
        {
            Console.WriteLine("Load from file?(y/n):");

            return Console.ReadLine() == "y" ? true : false;
        }

        // Asks user to enter rows number
        public int GetRows()
        {
            bool valid = false;
            int rows = 0;

            while (!valid)
            {
                Console.WriteLine("Enter rows:");

                valid = int.TryParse(Console.ReadLine(), out rows);
            }

            return rows;
        }

        // Asks user to enter columns number
        public int GetColumns()
        {
            bool valid = false;
            int columns = 0;

            while (!valid)
            {
                Console.WriteLine("Enter columns:");

                valid = int.TryParse(Console.ReadLine(), out columns);
            }

            return columns;
        }
    }
}
