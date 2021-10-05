using Mastermind.Game.Interfaces;
using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game
{
    public class RandomPegColorService : IRandomPegColorService
    {
        private readonly Random _random = new Random();
        private readonly List<PegColor> _pegColors = Enum.GetValues<PegColor>().ToList();

        public PegColor GetRandomPegColor()
        {
            var index = _random.Next(0, _pegColors.Count());
            return _pegColors[index];
        }
    }
}
