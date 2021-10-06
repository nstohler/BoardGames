using System;
using System.Threading.Tasks;

namespace Mastermind.Game.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Lets play Mastermind!");

            var game = new MastermindGameConsole();
            await game.RunAsync();

            Console.WriteLine("done.");
            Console.ReadLine();
        }
    }
}
