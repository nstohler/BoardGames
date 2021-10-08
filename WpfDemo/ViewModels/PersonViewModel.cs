using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfDemo.ViewModels
{
    public class PersonViewModel : ObservableObject
    {
        public PersonViewModel()
        {
            IncrementLikeCounterCommand = new RelayCommand(IncrementLikeCounter);
            // demo data
            FirstName = "Hans";
            LastName = "Wurst";
            LikeCount = 42;

            var colorPatterns = new List<ColorPatternViewModel>
            {
                new ColorPatternViewModel { Color1 = "Orange", Color2 = "Orange", Color3="Orange", Color4="Orange" },
                new ColorPatternViewModel { Color1 = "Red", Color2 = "Yellow", Color3="Green", Color4="Black" },
                new ColorPatternViewModel { Color1 = "Blue", Color2 = "Orange", Color3="Red", Color4="Black" },
                new ColorPatternViewModel { Color1 = "Blue", Color2 = "Orange", Color3="LightBlue", Color4="Black" },
            };
            ColorPatterns = new ObservableCollection<ColorPatternViewModel>(colorPatterns);
        }

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private ObservableCollection<ColorPatternViewModel> _colorPatterns;

        public ObservableCollection<ColorPatternViewModel> ColorPatterns
        {
            get => _colorPatterns;
            set => SetProperty(ref _colorPatterns, value);
        }

        private int _likeCount;

        public int LikeCount
        {
            get => _likeCount;
            set => SetProperty(ref _likeCount, value);
        }

        public ICommand IncrementLikeCounterCommand { get; }

        private void IncrementLikeCounter() => LikeCount++;
    }
}
