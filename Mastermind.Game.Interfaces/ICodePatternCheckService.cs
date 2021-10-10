using Mastermind.Game.Models;

namespace Mastermind.Game.Interfaces
{
    public interface ICodePatternCheckService
    {
        bool AreMatchingCodePatterns(CodePattern codePattern1, CodePattern codePattern2);

        CheckResult GetCheckResult(CodePattern codeMakerPattern, CodePattern codeBreakerPattern);
    }
}
