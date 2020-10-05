using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace GameOfLife
{
    public enum Cell
    {
        Dead,
        Alive
    }

    public class Game
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private int cellCount;
        private int iterationCount;
        private Cell[,] grid;

        public Game(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new Cell[Rows, Columns];
        }

        public void Run()
        {
            Init();

            while (true)
            {
                Draw(grid);
                grid = Iterate(grid);
                iterationCount++;
            }
        }

        private void Draw(Cell[,] grid, int timeout = 1000)
        {
            cellCount = 0;
            var stringBuilder = new StringBuilder();

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    var cell = grid[row, column];
                    stringBuilder.Append(cell == Cell.Alive ? "O" : " ");

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
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"/current_state.txt",
                stringBuilder.ToString());

            Thread.Sleep(timeout);
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

        private void Init()
        {
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
