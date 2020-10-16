namespace GameOfLife
{
    /// <summary>
    /// Main entry point where game initialization happens
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts game engine and runs games
        /// </summary>
        public static void Main()
        {
            var gameEngine = new GameEngine();
            gameEngine.RunGames();
        }
    }
}
