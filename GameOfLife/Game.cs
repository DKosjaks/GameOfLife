
namespace GameOfLife
{
    /// <summary>
    /// Represents game object
    /// </summary>
    public class Game
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Cell[,] Grid { get; set; }
        public int IterationCount { get; set; }
        public int CellCount { get; set; }

        public Game(int rows, int columns, Cell[,] grid)
        {
            Rows = rows;
            Columns = columns;
            Grid = grid;
        }
    }
}
