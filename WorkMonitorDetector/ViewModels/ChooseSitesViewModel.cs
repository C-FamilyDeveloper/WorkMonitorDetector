using MVVMUtilities.Core;
using MVVMUtilities.Abstractions;

namespace WorkMonitorDetector.ViewModels
{
    public class ChooseSitesViewModel : BaseViewModel
    {
        private INavigationService navigationService;
        private readonly IDialogService dialogService;

        public RelayCommand OK { get; set; }

        public ChooseSitesViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            OK = new(() =>
            {
                navigationService.Close<ChooseSitesViewModel>();                               
            });
        }
        
    }
}
