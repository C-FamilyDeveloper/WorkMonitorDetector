using MVVMUtilities.Core;
using MVVMUtilities.Abstractions;
using System.Collections.ObjectModel;
using WorkMonitorDetector.Models;
using System.Linq;
using System;

namespace WorkMonitorDetector.ViewModels
{
    public class ChooseSitesViewModel : BaseViewModel
    {
        private INavigationService navigationService;
        private IDialogService dialogService;
        private Uri url;
        public Uri URL { 
            get => url;
            set 
            {
                url = value;
                OnPropertyChanged();
            } 
        }
        public bool IsEnabledDeleting
        {
            get => Sites.Any(i => i.IsChecked);
        }
        public ObservableCollection<CheckedListItem> Sites { get; set; } = new();
        public RelayCommand AddSite { get; set; }
        public RelayCommand RemoveSelectedSites { get; set; }
        public RelayCommand SiteCheckedChanged { get; set; }
        public RelayCommand Save { get; set; }

        public ChooseSitesViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            URL = new Uri("https://google.com");
            AddSite = new(() =>
            {
                try
                {
                    if (Sites.Any(j => j.ItemValue == URL.Host))
                    {
                        throw new ArgumentException("Значение уже содержится в списке");
                    }
                    CheckedListItem item = new() { ItemValue = URL.Host, IsChecked = false };
                    Sites.Add(item);
                }
                catch (Exception ex)
                {
                    dialogService.ShowErrorMessage(ex.Message, "Ошибка");
                }
            });
            RemoveSelectedSites = new(() =>
            {
                for (int j = 0; j < Sites.Count; j++)
                {
                    if (Sites[j].IsChecked)
                    {
                        Sites.RemoveAt(j);
                    }
                }
                OnPropertyChanged(nameof(IsEnabledDeleting));
            });
            SiteCheckedChanged = new(() => OnPropertyChanged(nameof(IsEnabledDeleting)));
            Save = new(() => 
            {
                navigationService.Close<ChooseSitesViewModel>();
            });

        }
        
    }
}
