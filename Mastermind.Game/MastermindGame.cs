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
        private readonly List<CodePattern> _codeBreakerCombinations;

        public int GetCodeBreakerCombinationCount => _codeBreakerCombinations.Count;

        public MastermindGame(IRandomPegColorService randomPegColorService)
        {
            _codeBreakerCombinations = new List<CodePattern>();
            _codeMakerCombination = new CodePattern(
                randomPegColorService.GetRandomPegColor(),
                randomPegColorService.GetRandomPegColor(),
                randomPegColorService.GetRandomPegColor(),
                randomPegColorService.GetRandomPegColor());
        }

        public Task<CheckResult> SubmitAndCheckCodeBreakerCodePatternAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            var playerCodePattern = new CodePattern(color1, color2, color3, color4);

            // submit player code pattern, store in list
            _codeBreakerCombinations.Add(playerCodePattern);

            // check result
            return Task.FromResult(_codeMakerCombination.GetCheckResult(playerCodePattern));
        }

        public Task<bool> IsExactMatchAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            return Task.FromResult(_codeMakerCombination.MatchesOtherPattern(new CodePattern(color1, color2, color3, color4)));
        }

        public void StartNewGame()
        {
            throw new NotImplementedException();
        }
    }
}
