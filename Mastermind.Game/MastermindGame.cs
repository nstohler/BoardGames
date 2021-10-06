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
        private readonly CodePattern _codeMakerCombination;
        private readonly List<CodePatternWithResult> _codeBreakerCombinationsWithResults;

        public int GetCodeBreakerCombinationCount => _codeBreakerCombinationsWithResults.Count;

        public MastermindGame(IRandomPegColorService randomPegColorService)
        {
            _codeBreakerCombinationsWithResults = new List<CodePatternWithResult>();
            _codeMakerCombination = new CodePattern(
                randomPegColorService.GetRandomPegColor(),
                randomPegColorService.GetRandomPegColor(),
                randomPegColorService.GetRandomPegColor(),
                randomPegColorService.GetRandomPegColor());
        }

        public Task<CodePatternWithResult> SubmitAndCheckCodeBreakerCodePatternAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            var playerCodePattern = new CodePattern(color1, color2, color3, color4);
            var result = _codeMakerCombination.GetCheckResult(playerCodePattern);
            var codePatternWithResult = new CodePatternWithResult(playerCodePattern, result);

            // submit player code pattern, store in list
            _codeBreakerCombinationsWithResults.Add(codePatternWithResult);

            // check result
            return Task.FromResult(codePatternWithResult);
        }

        public Task<bool> IsExactMatchAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            return Task.FromResult(_codeMakerCombination.MatchesOtherPattern(new CodePattern(color1, color2, color3, color4)));
        }

        public void StartNewGame()
        {
            throw new NotImplementedException();
        }

        public CodePattern GetCodeMakerPattern()
        {
            // only use after losing the game!
            return _codeMakerCombination;
        }
    }
}
