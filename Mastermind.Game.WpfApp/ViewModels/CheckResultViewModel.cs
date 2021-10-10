using Mastermind.Game.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.WpfApp.ViewModels
{
    public class CheckResultViewModel : ObservableObject
    {
        private int _colorAndPositionExactCount;

        public int ColorAndPositionExactCount
        {
            get => _colorAndPositionExactCount;
            set => SetProperty(ref _colorAndPositionExactCount, value);
        }

        private int _colorExactCount;

        public int ColorExactCount
        {
            get => _colorExactCount;
            set => SetProperty(ref _colorExactCount, value);
        }

        private bool _isGameWon;

        public bool IsGameWon
        {
            get => _isGameWon;
            set => SetProperty(ref _isGameWon, value);
        }

        public static CheckResultViewModel Create(CheckResult checkResult)
        {
            return new CheckResultViewModel
            {
                ColorAndPositionExactCount = checkResult.ColorAndPositionExactCount,
                ColorExactCount = checkResult.ColorExactCount,
                IsGameWon = checkResult.IsGameWon
            };
        }
    }
}
