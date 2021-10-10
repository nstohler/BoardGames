using Mastermind.Game.Models;
using Mastermind.Game.WpfApp.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mastermind.Game.WpfApp.ViewModels
{
    public class ColorViewModel : ObservableObject
    {
        public ColorViewModel()
        {
        }

        private Color _color;

        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        private string _colorName;

        public string ColorName
        {
            get => _colorName;
            set => SetProperty(ref _colorName, value);
        }

        private string _colorChar;

        public string ColorChar
        {
            get => _colorChar;
            set => SetProperty(ref _colorChar, value);
        }

        private string _colorDisplayName;

        public string ColorDisplayName
        {
            get => _colorDisplayName;
            set => SetProperty(ref _colorDisplayName, value);
        }

        private PegColor _pegColor;

        public PegColor PegColor
        {
            get => _pegColor;
            set => SetProperty(ref _pegColor, value);
        }

        internal static ColorViewModel Create(PegColor pegColor)
        {
            return Create(ColorConverters.PegColorToCharMap[pegColor]);
        }

        public static ColorViewModel Create(string colorChar)
        {
            return new ColorViewModel
            {
                ColorChar = colorChar,
                ColorName = ColorConverters.CharToColorNameMap[colorChar],
                ColorDisplayName = ColorConverters.CharToColorDisplayNameMap[colorChar],
                Color = ColorConverters.CharToXamlColorMap[colorChar],
                PegColor = ColorConverters.CharToPegColorMap[colorChar]
            };
        }
    }
}
