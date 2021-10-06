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

        // TODO:
        // - CodeBreaker-Pattern mit CodeMaker-Pattern vergleichen,
        // - Resultat und CodeMaker-Pattern an _codeBreakerCombinationsWithResults anhängen
        // - Resultat zurückgeben.
        public Task<CodePatternWithResult> SubmitAndCheckCodeBreakerCodePatternAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            throw new NotImplementedException();
        }

        // TODO:
        // - true/false zurückgeben, wenn Patterns 100% matchen (oder nicht)
        public Task<bool> IsExactMatchAsync(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            return Task.FromResult(_codePatternCheckService.AreMatchingCodePatterns(_codeMakerCombination, new CodePattern(color1, color2, color3, color4)));
        }

        // TODO: wird nicht gebraucht, entfernen
        public void StartNewGame()
        {
            throw new NotImplementedException();
        }

        // TODO: CodeMaker-Pattern zurückgeben, so dass bei Verlust der versteckte Code dargestellt werden kann.
        public CodePattern GetCodeMakerPattern()
        {
            // only use after losing the game!
            throw new NotImplementedException();
        }
    }
}
