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
            if (UIManager.ShowInitMsg())
                gameEngine.InitAllGames();
            else
                gameEngine.InitOneGame();
        }
    }
}
