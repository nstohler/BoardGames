using Mastermind.Game.Models.Helpers;
using System;
using System.Collections.Immutable;

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

        public static CodePattern CreateFromCharString(string colorCharString)
        {
            colorCharString = colorCharString.ToUpper();

            if (colorCharString.Length != 4)
            {
                throw new ArgumentException("colorCharString has not length 4", nameof(colorCharString));
            }

            foreach (var colorChar in colorCharString)
            {
                if(!PegColorConverters.ValidChars.Contains(colorChar.ToString()))
                {
                    throw new ArgumentException("Invalid color characters found in colorCharString", nameof(colorCharString));
                }
            }

            return new CodePattern(
                PegColorConverters.CharToPegColorMap[colorCharString[0].ToString()],
                PegColorConverters.CharToPegColorMap[colorCharString[1].ToString()],
                PegColorConverters.CharToPegColorMap[colorCharString[2].ToString()],
                PegColorConverters.CharToPegColorMap[colorCharString[3].ToString()]
                );
        }
    }
}
