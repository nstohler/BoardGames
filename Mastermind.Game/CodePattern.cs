using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game
{
    public class CodePattern
    {
        private readonly PegColor[] PegColors;

        public PegColor Color1 { get => PegColors[0]; }
        public PegColor Color2 { get => PegColors[1]; }
        public PegColor Color3 { get => PegColors[2]; }
        public PegColor Color4 { get => PegColors[3]; }

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

            var fullMatchCountMap = Enum.GetValues<PegColor>().ToDictionary(k => k, v => 0);

            if(MatchesOtherPattern(codeBreakerPattern))
            {
                return new CheckResult(4, 0);
            }

            // find full matches, update map
            for (int i = 0; i < 4; i++)
            {
                var makerColor = PegColors[i];
                var breakerColor = codeBreakerPattern.PegColors[i];
                if (makerColor == breakerColor)
                {
                    colorAndPositionExactCount++;
                    fullMatchCountMap[makerColor]++;
                }
                else if (PegColors.Any(x => x == breakerColor))
                {
                    colorExactCount++;
                }
            }

            //// find color matches, use map
            //for (int i = 0; i < 4; i++)
            //{
            //    var makerColor = PegColors[i];
            //    var breakerColor = codeBreakerPattern.PegColors[i];

            //    // all matched already
            //    if (makerColor != breakerColor)
            //    {
            //        if (codeBreakerPattern.PegColors.Count(x => x == breakerColor) > fullMatchCountMap[breakerColor])
            //        {

            //        }
            //        else
            //      if (PegColors.Any(x => x == breakerColor))
            //        {
            //            colorExactCount++;
            //        }
            //    }
            //}

            return new CheckResult(colorAndPositionExactCount, colorExactCount);
        }
    }
}
