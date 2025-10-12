using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace GroupCalendar
{
    /// <summary>
    /// community toolkit - mvvm toolkit
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            Startup += OnStartup; 
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var main = Current.Services.GetService<MainWindow>();
            main?.Show();
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

            //views
            services.AddSingleton<MainWindow>();

            return services.BuildServiceProvider();
        }
    }
}
