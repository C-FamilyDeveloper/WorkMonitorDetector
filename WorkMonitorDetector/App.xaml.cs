using Microsoft.Extensions.DependencyInjection;
using MVVMUtilities.Abstractions;
using MVVMUtilities.Services;
using System.Diagnostics;
using System;
using System.Windows;
using WorkMonitorDetector.Models;
using WorkMonitorDetector.ViewModels;
using WorkMonitorDetector.Views;

namespace WorkMonitorDetector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {           
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            if (!serviceProvider.GetRequiredService<ICheckInstanceService>().IsOneApplicationInstance())
            {
                serviceProvider.GetRequiredService<IDialogService>().ShowWarningMessage(
                    "Work Monitor is already running." + Environment.NewLine + Environment.NewLine
                        + "You can only run one instance at a time.", "Work Monitor Error");
                Process.GetCurrentProcess().Kill();
                Environment.Exit(0);
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICheckInstanceService, CheckInstanceService>();
            services.AddSingleton<IDialogService,  DialogService>();
            services.AddSingleton<IFileDialogService<FileOpenAction>, OpenFileDialogService>((serviceProvider) => 
            { 
                return new OpenFileDialogService("Приложения", "*.exe"); 
            });
            services.AddSingleton<INavigationService, NavigationService>(serviceProvider =>
            {
                var navigationService = new NavigationService(serviceProvider);
                //navigationService.Initialize(serviceProvider);
                navigationService.ConfigureWindow<MainViewModel, MainView>();
                navigationService.ConfigureWindow<ChooseSitesViewModel, ChooseSitesView>();
                navigationService.ConfigureWindow<ChooseApplicationsViewModel, ChooseApplicationsView>();
                return navigationService;
            });
            services.AddScoped<MainViewModel>();
            services.AddScoped<ChooseSitesViewModel>();
            services.AddScoped<ChooseApplicationsViewModel>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var service = serviceProvider.GetRequiredService<INavigationService>();
            service.Show<MainViewModel>();
        }
    }
}
