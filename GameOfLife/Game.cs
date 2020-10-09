namespace GameOfLife
{
    /// <summary>
    /// Represents game object
    /// </summary>
    public class Game
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public CellEnum[,] Grid { get; set; }
        public int IterationCount { get; set; }
        public int CellCount { get; set; }
    }
}
