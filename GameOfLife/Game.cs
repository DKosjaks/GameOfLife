namespace GameOfLife
{
    /// <summary>
    /// Represents game object
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Grid rows number
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Grid columns number
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// 2 dimentional array of cells
        /// </summary>
        public CellEnum[,] Grid { get; set; }

        /// <summary>
        /// Game iterations count
        /// </summary>
        public int IterationCount { get; set; }

        /// <summary>
        /// Alive cells count
        /// </summary>
        public int CellCount { get; set; }
    }
}
