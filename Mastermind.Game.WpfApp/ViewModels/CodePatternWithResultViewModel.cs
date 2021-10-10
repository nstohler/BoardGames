using Mastermind.Game.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.WpfApp.ViewModels
{
    public class CodePatternWithResultViewModel : ObservableObject
    {

        private ObservableCollection<ColorViewModel> _codePattern;

        public ObservableCollection<ColorViewModel> CodePattern
        {
            get => _codePattern;
            set => SetProperty(ref _codePattern, value);
        }

        private CheckResultViewModel _Rresult;

        public CheckResultViewModel Result
        {
            get => _Rresult;
            set => SetProperty(ref _Rresult, value);
        }

        private int _number;

        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        internal static CodePatternWithResultViewModel Create(int number, CodePatternWithResult codePatternWithResult)
        {
            return new CodePatternWithResultViewModel
            {
                Number = number,
                CodePattern = new ObservableCollection<ColorViewModel>(codePatternWithResult.CodePattern.PegColors.Select(x => ColorViewModel.Create(x))),
                Result = CheckResultViewModel.Create(codePatternWithResult.Result)
            };
        }
    }
}
