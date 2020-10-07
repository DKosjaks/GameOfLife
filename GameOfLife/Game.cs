namespace GameOfLife
{
    /// <summary>
    /// Represents game object
    /// </summary>
    public class Game
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public CellEnum[,] Grid { get; set; }
        public int IterationCount { get; set; }
        public int CellCount { get; set; }

        public Game(int rows, int columns, CellEnum[,] grid)
        {
            Rows = rows;
            Columns = columns;
            Grid = grid;
        }
    }
}
