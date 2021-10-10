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
        private readonly IRandomPegColorService _randomPegColorService;
        private readonly ICodePatternCheckService _codePatternCheckService;

        private readonly CodePattern _codeMakerCombination;
        private readonly List<CodePatternWithResult> _codeBreakerCombinationsWithResults;
        private readonly int _maxPlayerAttempts = 10;
        private readonly Guid _gameId = Guid.NewGuid();

        public int MaxPlayerAttempts => _maxPlayerAttempts;
        public int GetCodeBreakerCombinationCount => _codeBreakerCombinationsWithResults.Count;

        public MastermindGame(IRandomPegColorService randomPegColorService, ICodePatternCheckService codePatternCheckService)
        {
            _randomPegColorService = randomPegColorService;
            _codePatternCheckService = codePatternCheckService;

            _codeBreakerCombinationsWithResults = new List<CodePatternWithResult>();
            _codeMakerCombination = new CodePattern(
                _randomPegColorService.GetRandomPegColor(),
                _randomPegColorService.GetRandomPegColor(),
                _randomPegColorService.GetRandomPegColor(),
                _randomPegColorService.GetRandomPegColor());
        }

        public Task<CodePatternWithResult> SubmitAndCheckCodeBreakerCodePatternAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            var playerCodePattern = new CodePattern(color1, color2, color3, color4);
            var result = _codePatternCheckService.GetCheckResult(_codeMakerCombination, playerCodePattern);
            var codePatternWithResult = new CodePatternWithResult(playerCodePattern, result);

            // submit player code pattern, store in list
            _codeBreakerCombinationsWithResults.Add(codePatternWithResult);

            // check result
            return Task.FromResult(codePatternWithResult);
        }

        public Task<bool> IsExactMatchAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            return Task.FromResult(_codePatternCheckService.AreMatchingCodePatterns(_codeMakerCombination, new CodePattern(color1, color2, color3, color4)));
        }

        public CodePattern GetCodeMakerPattern()
        {
            // only use after losing the game!
            return _codeMakerCombination;
        }

        public string GetGameId()
        {
            return _gameId.ToString();
        }

        public bool IsGameLost()
        {
            // the game is lost once the player has not attempts left
            return _codeBreakerCombinationsWithResults.Count() >= _maxPlayerAttempts;
        }

        public Hint GetHint()
        {
            // TODO: create hint system...
            throw new NotImplementedException();
        }
    }
}
