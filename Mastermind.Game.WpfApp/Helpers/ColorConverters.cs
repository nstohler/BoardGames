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
        private static readonly ImmutableList<(string ColorChar, PegColor PegColor, Color XamlColor)> AllColorMappings =
            new List<(string, PegColor, Color)>()
            {
                ( "Y", PegColor.Yellow, Colors.Yellow),
                ( "O", PegColor.Orange, Colors.Orange),
                ( "R", PegColor.Red, Colors.Red),
                ( "G", PegColor.Green, Colors.Green),
                ( "L", PegColor.LightBlue, Colors.LightBlue),
                ( "B", PegColor.DarkBlue, Colors.DarkBlue)
            }
            .ToImmutableList();

        public static readonly ImmutableDictionary<PegColor, string> PegColorToCharMap;
        public static readonly ImmutableDictionary<string, PegColor> CharToPegColorMap;
        public static readonly ImmutableDictionary<string, Color> CharToXamlColorMap;
        
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
        }
    }
}
