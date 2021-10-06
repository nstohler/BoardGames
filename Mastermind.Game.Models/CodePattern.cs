using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.Models
{
    public class CodePattern
    {
        public PegColor Color1 => PegColors[0];
        public PegColor Color2 => PegColors[1];
        public PegColor Color3 => PegColors[2];
        public PegColor Color4 => PegColors[3];

        public ImmutableArray<PegColor> PegColors;

        public CodePattern(PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            PegColors = ImmutableArray.Create<PegColor>(color1, color2, color3, color4);
        }
    }
}
