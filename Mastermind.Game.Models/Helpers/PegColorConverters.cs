using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.Models.Helpers
{
    public static class PegColorConverters
    {
        private static readonly ImmutableList<(string ColorChar, string ColorDisplayName, PegColor PegColor)> AllColorMappings =
            new List<(string, string, PegColor)>()
            {
                ( "Y", "Yellow", PegColor.Yellow ),
                ( "O", "Orange", PegColor.Orange ),
                ( "R", "Red", PegColor.Red ),
                ( "G", "Green", PegColor.Green ),
                ( "L", "Light Blue", PegColor.LightBlue ),
                ( "B", "Dark Blue", PegColor.DarkBlue )
            }
            .ToImmutableList();

        public static readonly ImmutableList<string> ValidChars;
        public static readonly ImmutableDictionary<PegColor, string> PegColorToCharMap;
        public static readonly ImmutableDictionary<string, PegColor> CharToPegColorMap;

        static PegColorConverters()
        {
            ValidChars = AllColorMappings.Select(x => x.ColorChar).ToImmutableList();

            PegColorToCharMap = AllColorMappings
                .Select(x => new { Key = x.PegColor, Value = x.ColorChar })
                .ToImmutableDictionary(x => x.Key, x => x.Value);

            CharToPegColorMap = AllColorMappings
                .Select(x => new { Key = x.ColorChar, Value = x.PegColor })
                .ToImmutableDictionary(x => x.Key, x => x.Value);
        }
    }
}
