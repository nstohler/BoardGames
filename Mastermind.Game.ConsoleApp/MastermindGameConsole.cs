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
            await Task.Delay(0); // remove this line

            throw new NotImplementedException();
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
