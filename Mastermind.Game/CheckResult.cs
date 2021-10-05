using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game
{
    public class CheckResult
    {
        public readonly int ColorAndPositionExactCount;
        public readonly int ColorExactCount;

        public CheckResult(int colorAndPositionExactCount, int colorExactCount)
        {
            ColorAndPositionExactCount = colorAndPositionExactCount;
            ColorExactCount = colorExactCount;
        }
    }
}
