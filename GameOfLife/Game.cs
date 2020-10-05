using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public class Game
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private enum Cell
        {
            Dead,
            Alive
        }

        private int iterationCount;
        private Cell[,] grid;
        private string currentStateFile = AppDomain.CurrentDomain.BaseDirectory + @"/current_state.txt";

        public Game()
        {
        }

        public Game(int rows, int columns) : this()
        {
            Rows = rows;
            Columns = columns;
        }

        public void Run(bool fromFile = false)
        {
            if (fromFile)
                InitFromFile();
            else
                InitRandom();

            while (!Console.KeyAvailable)
            {
                Draw(grid);
                grid = Iterate(grid);
                iterationCount++;
            }
        }

        private void Draw(Cell[,] grid)
        {
            var cellCount = 0;
            var stringBuilder = new StringBuilder();

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    var cell = grid[row, column];
                    stringBuilder.Append(cell == Cell.Alive ? 'O' : ' ');

                    if (cell == Cell.Alive)
                        cellCount++;
                }
                stringBuilder.Append("\n");
            }

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Iteration: {iterationCount}");
            Console.WriteLine($"Cells: {cellCount}");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Press any key to stop");

            File.WriteAllText(currentStateFile, stringBuilder.ToString());

            Thread.Sleep(1000);
        }

        private void InitFromFile()
        {
            var fileContents = File.ReadAllLines(currentStateFile);
            Rows = fileContents.Count();
            Columns = fileContents.First().Count();
            grid = new Cell[Rows, Columns];

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    grid[row, column] = (Cell)(fileContents[row][column].Equals('O') ? 1 : 0);
                }
            }
        }

        private Cell[,] Iterate(Cell[,] currentGrid)
        {
            var nextGrid = new Cell[Rows, Columns];

            for (var row = 1; row < Rows - 1; row++)
                for (var column = 1; column < Columns - 1; column++)
                {
                    var aliveNeighbors = 0;
                    for (var i = -1; i <= 1; i++)
                    {
                        for (var j = -1; j <= 1; j++)
                        {
                            if (i != 0 || j != 0)
                            {
                                aliveNeighbors += currentGrid[row + i, column + j] == Cell.Alive ? 1 : 0;
                            }
                        }
                    }

                    var currentCell = currentGrid[row, column];
 
                    if (currentCell == Cell.Alive && aliveNeighbors < 2)
                    {
                        nextGrid[row, column] = Cell.Dead;
                    }
                    else if (currentCell == Cell.Alive && aliveNeighbors > 3)
                    {
                        nextGrid[row, column] = Cell.Dead;
                    }
                    else if (currentCell == Cell.Dead && aliveNeighbors == 3)
                    {
                        nextGrid[row, column] = Cell.Alive;
                    }
                    else
                    {
                        nextGrid[row, column] = currentCell;
                    }
                }

            return nextGrid;
        }

        private void InitRandom()
        {
            grid = new Cell[Rows, Columns];

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    grid[row, column] = (Cell)RandomNumberGenerator.GetInt32(0, 2);
                }
            }
        }
    }
}
