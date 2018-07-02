using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.WPF.Infastructure.Services;
using DatabaseImporter.WPF.Infastructure.Utilities;

namespace DatabaseImporter.WPF.Infastructure.ViewModels
{
    public class ConnectionViewModel : BaseViewModel
    {
        private IDbConnectionConfigurator _configurator;


        public ConnectionViewModel(INavigationService navigationService,
            IMessagingService messagingService) :
            base(navigationService, messagingService)
        {
            SaveConnectionStringCommand = new Command(SaveConnectionStringCommandAction);
        }

        private string _connectionString;

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                OnPropertyChanging();
                _connectionString = value;
                OnPropertyChanged();
            }
        }

        public override async Task Initialize(object data)
        {
            if (data is IDbConnectionConfigurator configurator)
            {
                _configurator = configurator;
                if (await _configurator.ConnectionStringExists())
                    ConnectionString = await _configurator.LoadConnectionString();

            }
        }


        #region Save Connection Command

        private ICommand _saveConnectionStringCommand;
        public ICommand SaveConnectionStringCommand
        {
            get => _saveConnectionStringCommand;
            set
            {
                OnPropertyChanging();
                _saveConnectionStringCommand = value;
                OnPropertyChanged();
            }
        }

        private async void SaveConnectionStringCommandAction(object parameter)
        {
            var loadedConnectionString = await _configurator.LoadConnectionString();
            if (!string.IsNullOrEmpty(ConnectionString) &&
                !ConnectionString.Equals(loadedConnectionString, StringComparison.InvariantCultureIgnoreCase))
            {
                await _configurator.WriteConnectionString(ConnectionString);
            }

            BackCommand.Execute(parameter);
        }

        #endregion
    }
}