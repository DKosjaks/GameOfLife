namespace GameOfLife
{
    using System;
    using System.Text;

    class UIManager
    {
        /// <summary>
        /// Displays game grid and statistics
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="iterationCount"></param>
        /// <param name="cellCount"></param>
        public void ShowActiveGame(StringBuilder stringBuilder, int iterationCount, int cellCount)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Iteration: {iterationCount}");
            Console.WriteLine($"Cells: {cellCount}");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Press any key to stop");
        }

        /// <summary>
        /// Asks user if he wants to use file to load last game state
        /// </summary>
        /// <returns></returns>
        public bool IsFromFile()
        {
            Console.WriteLine("Load from file?(y/n):");

            return Console.ReadLine() == "y" ? true : false;
        }

        /// <summary>
        /// Asks user to enter rows number
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Asks user to enter columns number
        /// </summary>
        /// <returns></returns>
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
