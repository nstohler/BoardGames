using Mastermind.Game.Interfaces;
using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game
{
    public class MastermindGame : IMastermindGame
    {
        public Task<CheckResult> CheckCodeBreakerCodePattern(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            throw new NotImplementedException();
        }

        public void StartNewGame()
        {
            throw new NotImplementedException();
        }
    }
}
