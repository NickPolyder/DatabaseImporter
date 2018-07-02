using System.Windows.Input;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.WPF.Infastructure.Services;
using DatabaseImporter.WPF.Infastructure.Utilities;

namespace DatabaseImporter.WPF.Infastructure.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService, IMessagingService messagingService)
        : base(navigationService, messagingService)
        {
            Title = "Database Importer";
            OpenConnectionCommand = new Command(OpenConnectionCommandAction);
            RefreshCommand = new Command(RefreshCommandAction);
            ExitCommand = new Command(ExitCommandAction);
        }


        #region Open Connection Command

        private ICommand _openConnectionCommand;
        public ICommand OpenConnectionCommand
        {
            get => _openConnectionCommand;
            set
            {
                OnPropertyChanging();
                _openConnectionCommand = value;
                OnPropertyChanged();
            }
        }

        private async void OpenConnectionCommandAction(object parameter)
        {
            await NavigationService.NavigateTo<ConnectionViewModel>(CurrentContext.ServiceLocator.GetService<IDbConnectionConfigurator>());
        }

        #endregion

        #region Refresh Command

        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get => _refreshCommand;
            set
            {
                OnPropertyChanging();
                _refreshCommand = value;
                OnPropertyChanged();
            }
        }

        private void RefreshCommandAction(object parameter)
        {
            MessagingService.DisplayMessage("Refreshing", "Refreshing the connection!");
        }

        #endregion

        #region Exit Command

        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get => _exitCommand;
            set
            {
                OnPropertyChanging();
                _exitCommand = value;
                OnPropertyChanged();
            }
        }

        private void ExitCommandAction(object parameter)
        {
            NavigationService.Exit();
        }


        #endregion
    }
}