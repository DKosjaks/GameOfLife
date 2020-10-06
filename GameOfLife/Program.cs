using System;

//add summary and comments for functions
//add new class game logic
//add new file class
//add new UI class
//move enum to separate file
//move game.cs logic somewhere, like game engine class
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
            gameEngine.InitGame();
        }
    }
}
