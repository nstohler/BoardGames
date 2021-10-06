using Mastermind.Game.Interfaces;
using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game
{
    public class CodePatternCheckService : ICodePatternCheckService
    {
        /// <summary>
        /// Returns true if all pegs match in color and position
        /// </summary>
        public bool AreMatchingCodePatterns(CodePattern codePattern1, CodePattern codePattern2)
        {
            return codePattern1.PegColors.SequenceEqual(codePattern2.PegColors);
        }

        public CheckResult GetCheckResult(CodePattern codeMakerPattern, CodePattern codeBreakerPattern)
        {
            var colorAndPositionExactCount = 0;
            var colorExactCount = 0;

            if (AreMatchingCodePatterns(codeMakerPattern, codeBreakerPattern))
            {
                return new CheckResult(4, 0);
            }

            // build arrays without exact matches for rest
            var makerNonfullPattern = new List<PegColor>();
            var breakerNonfullPattern = new List<PegColor>();

            // find full matches, update map
            for (int i = 0; i < 4; i++)
            {
                var makerColor = codeMakerPattern.PegColors[i];
                var breakerColor = codeBreakerPattern.PegColors[i];
                if (makerColor == breakerColor)
                {
                    colorAndPositionExactCount++;
                }
                else
                {
                    makerNonfullPattern.Add(makerColor);
                    breakerNonfullPattern.Add(breakerColor);
                }
            }

            var breakerReductionPattern = breakerNonfullPattern.ToList();
            for (int i = 0; i < makerNonfullPattern.Count; i++)
            {
                var makerColor = makerNonfullPattern[i];
                var breakerColor = breakerNonfullPattern[i];
                if (breakerReductionPattern.Any(x => x == makerColor))
                {
                    colorExactCount++;

                    // Remove found entry from reduction list.
                    // This is required to create valid results when the same color is multiple times in the maker code
                    // Example: BOYB vs. YBOO => must return 0, 3 (and not 0, 4)!
                    var index = breakerReductionPattern.FindLastIndex(x => x == makerColor);
                    if (index > -1)
                    {
                        breakerReductionPattern.RemoveAt(index);
                    }
                }
            }

            return new CheckResult(colorAndPositionExactCount, colorExactCount);
        }
    }
}
