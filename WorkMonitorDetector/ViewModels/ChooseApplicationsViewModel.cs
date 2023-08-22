using System;
using System.Collections.ObjectModel;
using System.Linq;
using WorkMonitorDetector.Models;
using MVVMUtilities.Abstractions;
using MVVMUtilities.Core;
using MVVMUtilities.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace WorkMonitorDetector.ViewModels
{
    public class ChooseApplicationsViewModel :  BaseViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;
        private readonly IFileDialogService<FileOpenAction> fileDialog;
        public ObservableCollection<CheckedListItem> Applications { get; set; } = new();
        public bool IsEnabledDeleting
        {
            get => Applications.Any(i=>i.IsChecked);     
        }
        public RelayCommand ProgramCheckedChanged { get; set; }
        public RelayCommand AddProgram { get; set; }
        public RelayCommand RemoveSelectedPrograms { get; set; }
        public RelayCommand Save { get; set; }

        public ChooseApplicationsViewModel(INavigationService navigationService, IDialogService dialogService,
            IFileDialogService<FileOpenAction> fileDialog)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.fileDialog = fileDialog;
            AddProgram = new(() =>
            {
                try
                {
                    string name = fileDialog.GetFileName();
                    if (Applications.Any(j=>j.ItemValue == name))
                    {
                        throw new ArgumentException("Значение уже содержится в списке");
                    }
                    CheckedListItem item = new() { ItemValue = name, IsChecked = false };
                    Applications.Add(item);
                }
                catch (Exception ex)
                {
                    dialogService.ShowErrorMessage(ex.Message, "Ошибка");
                }               
            });
            RemoveSelectedPrograms = new(() =>
            {
                for (int j = 0; j < Applications.Count; j++) 
                {
                    if (Applications[j].IsChecked) 
                    {
                        Applications.RemoveAt(j);
                    }
                }
                OnPropertyChanged(nameof(IsEnabledDeleting));
            });
            Save = new(() =>
            {
                navigationService.Close<ChooseApplicationsViewModel>();
            });
            ProgramCheckedChanged = new(() =>
            {
                OnPropertyChanged(nameof(IsEnabledDeleting));
            });
        }
    }
    
}
