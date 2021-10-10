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
            StartNewGameCommand = new RelayCommand(StartNewGame, () => ShowSecretCode);
            GiveUpCommand = new RelayCommand(GiveUpGame, () => !ShowSecretCode);
            AddColorCommand = new AsyncRelayCommand<string>(AddColor, x => PlayerCode.Length < 4 && !ShowSecretCode);
            BackspaceCommand = new RelayCommand(ClearLastColor, () => PlayerCode.Length > 0 && !ShowSecretCode);
            SubmitCodeCommand = new AsyncRelayCommand(SubmitCode, () => PlayerCode.Length == 4 && !ShowSecretCode);

            StartNewGame();
        }

        private void StartNewGame()
        {
            ShowSecretCode = false;
            _mastermindGame = App.Current.Services.GetService<IMastermindGame>();

            // guid check if this works
            GameId = _mastermindGame.GetGameId();
            PlayerCode = string.Empty;
            PlayerCodePattern = new ObservableCollection<ColorViewModel>();
            SubmittedCodePatternsWithResults = new ObservableCollection<CodePatternWithResultViewModel>();

            SecretCodePattern = new ObservableCollection<ColorViewModel>(
                _mastermindGame.GetCodeMakerPattern().PegColors.Select(x => ColorViewModel.Create(x)));
        }

        private void GiveUpGame()
        {
            ShowSecretCode = true;
        }

        public RelayCommand StartNewGameCommand { get; }
        public RelayCommand GiveUpCommand { get; }
        public AsyncRelayCommand<string> AddColorCommand { get; set; }
        public RelayCommand BackspaceCommand { get; }
        public AsyncRelayCommand SubmitCodeCommand { get; }

        private string _gameId;

        public string GameId
        {
            get => _gameId;
            set => SetProperty(ref _gameId, value);
        }

        private bool _showSecretCode;

        public bool ShowSecretCode
        {
            get => _showSecretCode;
            set
            {
                SetProperty(ref _showSecretCode, value);
                StartNewGameCommand.NotifyCanExecuteChanged();
                GiveUpCommand.NotifyCanExecuteChanged();
                AddColorCommand.NotifyCanExecuteChanged();
                BackspaceCommand.NotifyCanExecuteChanged();
                SubmitCodeCommand.NotifyCanExecuteChanged();
            }
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

        private ObservableCollection<ColorViewModel> _playerCodePattern;

        public ObservableCollection<ColorViewModel> PlayerCodePattern
        {
            get => _playerCodePattern;
            set => SetProperty(ref _playerCodePattern, value);
        }

        private ObservableCollection<ColorViewModel> _secretCodePattern;

        public ObservableCollection<ColorViewModel> SecretCodePattern
        {
            get => _secretCodePattern;
            set => SetProperty(ref _secretCodePattern, value);
        }

        private ObservableCollection<CodePatternWithResultViewModel> _submittedCodePatternsWithResults;

        public ObservableCollection<CodePatternWithResultViewModel> SubmittedCodePatternsWithResults
        {
            get => _submittedCodePatternsWithResults;
            set => SetProperty(ref _submittedCodePatternsWithResults, value);
        }

        private Task AddColor(string colorCode)
        {
            PlayerCode += colorCode;
            PlayerCodePattern.Add(ColorViewModel.Create(colorCode));
            return Task.CompletedTask;
        }

        private void ClearLastColor()
        {
            PlayerCode = PlayerCode.Remove(PlayerCode.Length - 1);
            PlayerCodePattern.RemoveAt(PlayerCodePattern.Count - 1);
        }

        private async Task SubmitCode()
        {
            var codePatternWithResult = await _mastermindGame.SubmitAndCheckCodeBreakerCodePatternAsync(
                PlayerCodePattern[0].PegColor, PlayerCodePattern[1].PegColor,
                PlayerCodePattern[2].PegColor, PlayerCodePattern[3].PegColor);

            var codePatternWithResultViewModel = CodePatternWithResultViewModel.Create(SubmittedCodePatternsWithResults.Count + 1, codePatternWithResult);

            SubmittedCodePatternsWithResults.Add(codePatternWithResultViewModel);

            // clear pattern for new entry
            PlayerCodePattern.Clear();
            PlayerCode = string.Empty;

            // check if won
            if(codePatternWithResult.Result.IsGameWon)
            {
                // show solution
                ShowSecretCode = true;

                System.Windows.MessageBox.Show(
                    "Congratulations!" + Environment.NewLine +
                    "You win" + Environment.NewLine +
                    "🎉🎊🎈🎊🎉", 
                    "Ultimate success");
            }

            // ckeck if lost
            if(_mastermindGame.IsGameLost())
            {
                // show solution
                ShowSecretCode = true;

                // disable all player inputs, except start new game
            }
        }
    }
}
