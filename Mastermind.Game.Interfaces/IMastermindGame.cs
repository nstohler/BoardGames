using Mastermind.Game.Models;
using System;
using System.Threading.Tasks;

namespace Mastermind.Game.Interfaces
{
    public interface IMastermindGame
    {
        /// <summary>
        /// Checks the provided colors against the secret code and returns the result. 
        /// Stores the pattern and the result internally.
        /// </summary>
        Task<CodePatternWithResult> SubmitAndCheckCodeBreakerCodePatternAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4);

        /// <summary>
        /// Returns true if the provided colors match exactly the secret code
        /// </summary>
        Task<bool> IsExactMatchAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4);

        /// <summary>
        /// Returns the solution so it can be displayed to the user after the game has been lost (or the user gave up).
        /// </summary>
        CodePattern GetCodeMakerPattern();

        /// <summary>
        /// A guid identifying the current game
        /// </summary>
        string GetGameId();

        /// <summary>
        /// The game is lost when the code has not been found after 10 attempts
        /// </summary>
        bool IsGameLost();
    }
}
