using Mastermind.Game.Models;
using System;
using System.Threading.Tasks;

namespace Mastermind.Game.Interfaces
{
    public interface IMastermindGame
    {
        void StartNewGame();

        Task<CheckResult> CheckCodeBreakerCodePattern(PegColor color1, PegColor color2, PegColor color3, PegColor color4);


    }
}
