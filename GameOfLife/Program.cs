using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Load from file?(y/n):");
            var useFile = Console.ReadLine();

            if (useFile == "y")
            {
                Console.Clear();

                var game = new Game();
                game.Run(true);
            }
            else
            {
                Console.WriteLine("Enter rows:");
                var isValidRowsNumber = int.TryParse(Console.ReadLine(), out int rows);

                Console.WriteLine("Enter columns:");
                var isValidColumnsNumber = int.TryParse(Console.ReadLine(), out int columns);

                Console.Clear();

                if (!isValidRowsNumber || !isValidColumnsNumber)
                {
                    Console.WriteLine("Invalid number");
                }
                else
                {
                    var game = new Game(rows, columns);
                    game.Run();
                }
            }
        }
    }
}
