namespace GameOfLife
{
    /// <summary>
    /// Main entry point where game initialization happens
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var gameEngine = new GameEngine();
            gameEngine.RunGames();
        }
    }
}
