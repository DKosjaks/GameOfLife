using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
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
