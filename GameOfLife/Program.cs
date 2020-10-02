using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public enum Status
    {
        Dead,
        Alive
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter rows:");
            var rows = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter columns:");
            var columns = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            var game = new Game(rows, columns);
            game.Run();
        }
    }
}
