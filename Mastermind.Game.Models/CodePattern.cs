using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.Models
{
    public class CodePattern
    {
        private readonly PegColor[] PegColors;

        public PegColor Color1 => PegColors[0];
        public PegColor Color2 => PegColors[1];
        public PegColor Color3 => PegColors[2];
        public PegColor Color4 => PegColors[3];

        public PegColor[] ReadPegColorsCopy => PegColors.ToArray();

        public CodePattern(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            PegColors = new PegColor[]
            {
                color1, color2, color3, color4
            };
        }

        /// <summary>
        /// Returns true if all pegs match in color and position
        /// </summary>
        public bool MatchesOtherPattern(CodePattern other)
        {
            return PegColors.SequenceEqual(other.PegColors);
        }

        public CheckResult GetCheckResult(CodePattern codeBreakerPattern)
        {
            var colorAndPositionExactCount = 0;
            var colorExactCount = 0;

            if(MatchesOtherPattern(codeBreakerPattern))
            {
                return new CheckResult(4, 0);
            }

            // build arrays without exact matches for rest
            var makerNonfullPattern = new List<PegColor>();
            var breakerNonfullPattern = new List<PegColor>();

            // find full matches, update map
            for (int i = 0; i < 4; i++)
            {
                var makerColor = PegColors[i];
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
