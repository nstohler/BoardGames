using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ViewModels
{
    public class ColorPatternViewModel : ObservableObject
    {
        public ColorPatternViewModel()
        {
            //Color1 = "Red";
            //Color2 = "Green";
            //Color3 = "Blue";
            //Color4 = "Orange";
        }

        private string _color1;

        public string Color1
        {
            get => _color1;
            set => SetProperty(ref _color1, value);
        }

        private string _color2;

        public string Color2
        {
            get => _color2;
            set => SetProperty(ref _color2, value);
        }

        private string _color3;

        public string Color3
        {
            get => _color3;
            set => SetProperty(ref _color3, value);
        }

        private string _color4;

        public string Color4
        {
            get => _color4;
            set => SetProperty(ref _color4, value);
        }
    }
}
