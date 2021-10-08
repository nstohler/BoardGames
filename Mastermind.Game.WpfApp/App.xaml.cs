using Mastermind.Game.Interfaces;
using Mastermind.Game.WpfApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mastermind.Game.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services
            services.AddSingleton<IRandomPegColorService, RandomPegColorService>();
            services.AddSingleton<ICodePatternCheckService, CodePatternCheckService>();

            // Game
            services.AddTransient<IMastermindGame, MastermindGame>();

            // ViewModels
            services.AddTransient<MastermindGameViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
