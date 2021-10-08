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

namespace Mastermind.Game.WpfApp.ViewModels
{
    public class MastermindGameViewModel : ObservableObject
    {
        private IMastermindGame _mastermindGame;

        public MastermindGameViewModel()
        {
            StartNewGameCommand = new RelayCommand(StartNewGame);
            AddColorCommand = new AsyncRelayCommand<string>(AddColor, x => this.PlayerCode.Length < 4);

            StartNewGame();
        }

        private void StartNewGame()
        {
            _mastermindGame = App.Current.Services.GetService<IMastermindGame>();
            

            // guid check if this works
            GameId = _mastermindGame.GetGameId();
            PlayerCode = string.Empty;
        }

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
            }
        }

        public ICommand StartNewGameCommand { get; }

        public AsyncRelayCommand<string> AddColorCommand { get; set; }

        private Task AddColor(string colorCode)
        {
            PlayerCode += colorCode;
            return Task.CompletedTask;
        }
    }
}
