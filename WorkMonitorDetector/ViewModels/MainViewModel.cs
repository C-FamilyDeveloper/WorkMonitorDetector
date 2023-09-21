using MVVMUtilities.Abstractions;
using MVVMUtilities.Core;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkMonitorDetector.Models.Services;
using WorkMonitorTypes.Requests;

namespace WorkMonitorDetector.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;
        private readonly HttpService httpService;

        private ObservableCollection<string> clients;
        public ObservableCollection<string> Clients 
        {
            get => clients;
            set { clients = value; OnPropertyChanged(); }
        }
        public bool IsCommandsEnabled
        {
            get => !string.IsNullOrWhiteSpace(Client);
        }

        private string client;
        public string Client
        {  
            get => client;
            set { client = value; OnPropertyChanged(); }
        }
        public RelayCommand SetSites { get; set; }
        public RelayCommand Closed { get; set; }
        public RelayCommand SetApplications { get; set; }
        public RelayCommand UserChanged { get; set; }

        public MainViewModel(INavigationService navigationService, IDialogService dialogService, HttpService httpService)
        {
            Task.Run(async ()=> Clients = new (await httpService.GetClientNamesAsync()));
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.httpService = httpService;
            SetApplications = new(async () =>
            {
                navigationService.ShowDialog<ChooseApplicationsViewModel>();
                var applications = navigationService.GetDataContext<ChooseApplicationsViewModel>().Applications;
                foreach (var app in applications)
                {
                    await httpService.SendAsync(new AcceptedApp { AppName = app.ItemValue, UserName = client! });
                }
            });
            SetSites = new(async ()=>
            {
                navigationService.ShowDialog<ChooseSitesViewModel>();
                var sites = navigationService.GetDataContext<ChooseSitesViewModel>().Sites;
                foreach(var site in sites)
                {
                    await httpService.SendAsync(new AcceptedSite { SiteURL = site.ItemValue, UserName = client! });
                }
            });
            UserChanged = new(() =>
            {
                OnPropertyChanged(nameof(IsCommandsEnabled));
            });
            Closed = new(() =>
            {
                Environment.Exit(0);              
            });

        }
    }
}
