namespace GameOfLife
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UIManager
    {
        /// <summary>
        /// Displays game grid and statistics
        /// </summary>
        /// <param name="games"></param>
        /// <param name="gamesCount"></param>
        /// <param name="cellCount"></param>
        public void DrawAllGames(List<Game> games, int gamesCount, int cellCount)
        {
            ShowExitMsg();
            ShowAllGamesStats(gamesCount, cellCount);

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            int leftPos = 0;
            int topPos = 0;

            for (int i = 0; i < games.Count; i++)
            {
                if (i > 7)
                    break;

                if (i == 4)
                    leftPos = 0;
                
                if (i > 3)
                    topPos = games[i].Grid.GetLength(0) + 3;

                DrawGame(games[i].Grid, leftPos, topPos, games[i].IterationCount, games[i].CellCount);
                leftPos += games[i].Grid.GetLength(1);
            }
        }

        /// <summary>
        /// Draw one game cell grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="leftPos"></param>
        /// <param name="topPos"></param>
        /// <param name="iterationCount"></param>
        /// <param name="cellCount"></param>
        public void DrawGame(CellEnum[,] grid, int leftPos, int topPos, int iterationCount, int cellCount)
        {
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                try
                {
                    Console.SetCursorPosition(leftPos, i + 5 + topPos);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                
                Console.Write('|');

                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    var cell = grid[i, j] == CellEnum.Alive ? 'O' : ' ';
                    
                    Console.Write(cell);
                }

                Console.Write('|');
            }

            Console.SetCursorPosition(leftPos, topPos + 3);
            Console.WriteLine("Iteration: " + iterationCount);
            Console.SetCursorPosition(leftPos, topPos + 4);
            Console.WriteLine("Cells: " + cellCount);
        }

        /// <summary>
        /// Reads game ids input from user
        /// </summary>
        /// <returns></returns>
        public int[] GetGamesIds()
        {
            int[] gameIds = null;
            Console.WriteLine("Specify game ids to show, separated by comma (0-999):");

            try
            {
                gameIds = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }

            return gameIds;
        }

        /// <summary>
        /// Shows msg how to stop program
        /// </summary>
        public void ShowExitMsg()
        {
            Console.Clear();
            Console.WriteLine("Press any key to stop and save game state to a file");
        }

        private void ShowAllGamesStats(int gamesCount, int cellsCount)
        {
            Console.Write("Total games: " + gamesCount);
            Console.Write(". Total cells: " + cellsCount);
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
