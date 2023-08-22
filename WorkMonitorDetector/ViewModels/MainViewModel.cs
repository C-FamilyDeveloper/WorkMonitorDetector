using MVVMUtilities.Abstractions;
using MVVMUtilities.Core;
using System;
using System.Linq;

namespace WorkMonitorDetector.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;

        public RelayCommand SetSites { get; set; }
        public RelayCommand Closed { get; set; }
        public RelayCommand SetApplications { get; set; }
        public MainViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            SetApplications = new(() =>
            {
                navigationService.ShowDialog<ChooseApplicationsViewModel>();
            });
            SetSites = new(()=>
            {
                navigationService.ShowDialog<ChooseSitesViewModel>();
            });
            Closed = new(() =>
            {
                Environment.Exit(0);              
            });
        }
    }
}
