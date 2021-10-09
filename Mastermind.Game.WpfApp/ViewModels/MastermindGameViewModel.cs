using Mastermind.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Mastermind.Game.WpfApp.Helpers;
using Mastermind.Game.Models;
using System.Collections.ObjectModel;

namespace Mastermind.Game.WpfApp.ViewModels
{
    public class MastermindGameViewModel : ObservableObject
    {
        private IMastermindGame _mastermindGame;

        public MastermindGameViewModel()
        {
            StartNewGameCommand = new RelayCommand(StartNewGame);
            AddColorCommand = new AsyncRelayCommand<string>(AddColor, x => PlayerCode.Length < 4);
            BackspaceCommand = new RelayCommand(ClearLastColor, () => PlayerCode.Length > 0);
            SubmitCodeCommand = new RelayCommand(SubmitCode, () => PlayerCode.Length == 4);

            StartNewGame();
        }

        private void StartNewGame()
        {
            _mastermindGame = App.Current.Services.GetService<IMastermindGame>();

            //var ch = ColorConverters.PegColorToCharMap[PegColor.Green];
            //var pc = ColorConverters.CharToPegColorMap["Y"];
            //var x = ColorConverters.CharToXamlColorMap["L"];

            // guid check if this works
            GameId = _mastermindGame.GetGameId();
            PlayerCode = string.Empty;
            PlayerCodeX = new ObservableCollection<ColorViewModel>();
        }

        public RelayCommand StartNewGameCommand { get; }
        public AsyncRelayCommand<string> AddColorCommand { get; set; }
        public RelayCommand BackspaceCommand { get; }
        public RelayCommand SubmitCodeCommand { get; }

        private string _gameId;

        public string GameId
        {
            get => _gameId;
            set => SetProperty(ref _gameId, value);
        }

        private string _playerCode;

        public string PlayerCode
        {
            get => _playerCode;
            set
            {
                SetProperty(ref _playerCode, value);
                AddColorCommand.NotifyCanExecuteChanged();
                BackspaceCommand.NotifyCanExecuteChanged();
                SubmitCodeCommand.NotifyCanExecuteChanged();
            }
        }

        private ObservableCollection<ColorViewModel> _playerCodeX;

        public ObservableCollection<ColorViewModel> PlayerCodeX
        {
            get => _playerCodeX;
            set => SetProperty(ref _playerCodeX, value);
        }

        private Task AddColor(string colorCode)
        {
            PlayerCode += colorCode;
            PlayerCodeX.Add(CreatePlayerViewModel(colorCode));
            return Task.CompletedTask;
        }

        private void ClearLastColor()
        {
            PlayerCode = PlayerCode.Remove(PlayerCode.Length - 1);
            PlayerCodeX.RemoveAt(PlayerCodeX.Count - 1);
        }

        private void SubmitCode()
        {
            // TODO: submit code to game to have it added to the list and checked
        }

        private ColorViewModel CreatePlayerViewModel(string colorCode)
        {
            return new ColorViewModel
            {
                ColorChar = colorCode,
                ColorName = ColorConverters.CharToColorNameMap[colorCode],
                ColorDisplayName = ColorConverters.CharToColorDisplayNameMap[colorCode],
                Color = ColorConverters.CharToXamlColorMap[colorCode],
                PegColor = ColorConverters.CharToPegColorMap[colorCode]
            };
        }

    }
}
