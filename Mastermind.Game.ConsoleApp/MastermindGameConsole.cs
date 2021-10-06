using Mastermind.Game.Interfaces;
using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.ConsoleApp
{
    public class MastermindGameConsole
    {
        public async Task RunAsync()
        {
            var charToPegColorMap = new Dictionary<char, PegColor>()
            {
                { 'Y', PegColor.Yellow },
                { 'O', PegColor.Orange },
                { 'G', PegColor.Green },
                { 'R', PegColor.Red },
                { 'L', PegColor.LightBlue },
                { 'B', PegColor.DarkBlue },
            };
            var validColorCodeChars = charToPegColorMap.Keys.ToHashSet();

            IMastermindGame mastermindGame = new MastermindGame(new RandomPegColorService());

            // main game loop here
            Console.WriteLine("The CodeMaker made his choice.");

            // Y: Yellow, O: Orange, G: Green, R: Red, L: LightBlue, B: DarkBlue
            Console.WriteLine("Color codes:");
            foreach (var charToPeg in charToPegColorMap)
            {
                Console.BackgroundColor = GetConsoleColor(charToPeg.Value);
                Console.Write($"  {charToPeg.Key}  ");
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine();

            var isGameWon = false;
            var maxTries = 10;
            var currentTry = 1;
            while (currentTry < maxTries)
            {
                var isValidCode = false;
                string playerCodeString = string.Empty;
                while (!isValidCode)
                {
                    Console.WriteLine($"Enter your code now {currentTry}/{maxTries}:");
                    playerCodeString = Console.ReadLine().ToUpper();
                    if(!string.IsNullOrEmpty(playerCodeString) && playerCodeString.Length == 4)
                    {
                        var containsValidChars = playerCodeString.ToList().TrueForAll(x => validColorCodeChars.Contains(x));
                        isValidCode = containsValidChars;
                    }

                    if(!isValidCode)
                    {
                        Console.WriteLine("--> invalid code entry, try again!");
                    }
                }

                // convert string to colors
                var color1 = charToPegColorMap[playerCodeString[0]];
                var color2 = charToPegColorMap[playerCodeString[1]];
                var color3 = charToPegColorMap[playerCodeString[2]];
                var color4 = charToPegColorMap[playerCodeString[3]];

                // output in color for better visualization
                for (int i = 0; i < 4; i++)
                {
                    Console.BackgroundColor = GetConsoleColor(charToPegColorMap[playerCodeString[i]]);
                    // Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"  {playerCodeString[i]}  ");

                    Console.ResetColor();
                }
                Console.WriteLine();

                // check if won, break
                var codeWithResult = await mastermindGame.SubmitAndCheckCodeBreakerCodePatternAsync(color1, color2, color3, color4);

                if(codeWithResult.Result.IsGameWon)
                {
                    isGameWon = true;
                    break;
                }
                else
                {
                    // show feedback
                    Console.WriteLine($"=> Result: correct color & position: {codeWithResult.Result.ColorAndPositionExactCount} | correct color: {codeWithResult.Result.ColorExactCount}");
                }

                Console.WriteLine();
                currentTry++;
            }

            if(isGameWon)
            {
                Console.WriteLine($"You win. It took you {currentTry} tries to win!");
            }
            else
            {
                Console.WriteLine("You loose.");
            }
        }

        private ConsoleColor GetConsoleColor(PegColor color)
        {
            var map = new Dictionary<PegColor, ConsoleColor>()
            {
                { PegColor.Yellow, ConsoleColor.Yellow },
                { PegColor.Orange, ConsoleColor.DarkYellow },
                { PegColor.Green, ConsoleColor.Green },
                { PegColor.Red, ConsoleColor.Red },
                { PegColor.LightBlue, ConsoleColor.Blue },
                { PegColor.DarkBlue, ConsoleColor.DarkBlue },
            };
            return map[color];
        }
    }
}
