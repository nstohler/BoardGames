using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mastermind.Game.WpfApp.Helpers
{
    public static class ColorConverters
    {
        private static readonly ImmutableList<(string ColorChar, string ColorDisplayName, PegColor PegColor, Color XamlColor)> AllColorMappings =
            new List<(string, string, PegColor, Color)>()
            {
                ( "Y", "Yellow", PegColor.Yellow, Colors.Yellow ),
                ( "O", "Orange", PegColor.Orange, Colors.Orange ),
                ( "R", "Red", PegColor.Red, Colors.Red ),
                ( "G", "Green", PegColor.Green, Colors.Green ),
                ( "L", "Light Blue", PegColor.LightBlue, Colors.LightBlue ),
                ( "B", "Dark Blue", PegColor.DarkBlue, Colors.DarkBlue )
            }
            .ToImmutableList();

        public static readonly ImmutableDictionary<PegColor, string> PegColorToCharMap;
        public static readonly ImmutableDictionary<string, PegColor> CharToPegColorMap;
        public static readonly ImmutableDictionary<string, Color> CharToXamlColorMap;
        public static readonly ImmutableDictionary<string, string> CharToColorNameMap;
        public static readonly ImmutableDictionary<string, string> CharToColorDisplayNameMap;

        static ColorConverters()
        {
            PegColorToCharMap = AllColorMappings
                .Select(x => new { Key = x.PegColor, Value = x.ColorChar })
                .ToImmutableDictionary(x => x.Key, x => x.Value);

            CharToPegColorMap = AllColorMappings
                .Select(x => new { Key = x.ColorChar, Value = x.PegColor })
                .ToImmutableDictionary(x => x.Key, x => x.Value);

            CharToXamlColorMap = AllColorMappings
                .Select(x => new { Key = x.ColorChar, Value = x.XamlColor })
                .ToImmutableDictionary(x => x.Key, x => x.Value);

            CharToColorNameMap = AllColorMappings
                .Select(x => new { Key = x.ColorChar, Value = x.PegColor.ToString() })
                .ToImmutableDictionary(x => x.Key, x => x.Value);

            CharToColorDisplayNameMap = AllColorMappings
                .Select(x => new { Key = x.ColorChar, Value = x.ColorDisplayName })
                .ToImmutableDictionary(x => x.Key, x => x.Value);
        }
    }
}
