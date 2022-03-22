using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OvermindDogFacts.Interfaces;
using OvermindDogFacts.Models;
using OvermindDogFacts.Repositories;
using OvermindDogFacts.Services;
using OvermindDogFacts.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OvermindDogFacts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();  
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<IMainViewModel, MainViewModel>();
            services.AddTransient<IDogFactService, DogFactService>();
            services.AddTransient<IRepositorio, Repositorio>();
            services.AddSingleton<ConfigurationFact>();
            services.AddSingleton<MainWindow>();            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            serviceProvider.GetService<MainWindow>().Show();
        }
    }
}
