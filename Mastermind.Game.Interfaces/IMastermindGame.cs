using Mastermind.Game.Models;
using System;
using System.Threading.Tasks;

namespace Mastermind.Game.Interfaces
{
    public interface IMastermindGame
    {
        void StartNewGame();

        Task<CodePatternWithResult> SubmitAndCheckCodeBreakerCodePatternAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4);

        Task<bool> IsExactMatchAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4);

        CodePattern GetCodeMakerPattern();
    }
}
