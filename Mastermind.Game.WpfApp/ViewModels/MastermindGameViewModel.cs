using Mastermind.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.WpfApp.ViewModels
{
    public class MastermindGameViewModel
    {
        private readonly IMastermindGame _mastermindGame;

        public MastermindGameViewModel(IMastermindGame mastermindGame)
        {
            _mastermindGame = mastermindGame;
        }
    }
}
