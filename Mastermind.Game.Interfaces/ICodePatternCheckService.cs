using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.Interfaces
{
    public interface ICodePatternCheckService
    {
        bool AreMatchingCodePatterns(CodePattern codePattern1, CodePattern codePattern2);

        CheckResult GetCheckResult(CodePattern codeMakerPattern, CodePattern codeBreakerPattern);
    }
}
