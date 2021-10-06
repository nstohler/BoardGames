using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.Models
{
    public class CodePatternWithResult
    {
        public readonly CodePattern CodePattern;
        public readonly CheckResult Result;

        public CodePatternWithResult(CodePattern codePattern, CheckResult result)
        {
            CodePattern = codePattern;
            Result = result;
        }
    }
}
