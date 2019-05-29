using System;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.WPF.Infastructure.Services;
using DatabaseImporter.WPF.Infastructure.Utilities;

namespace DatabaseImporter.WPF.Infastructure.ViewModels
{
    public class ConnectionViewModel : BaseViewModel
    {
        private static Brush RedColorBrush = new SolidColorBrush(Colors.Red);
        private static Brush NeutralBrush = new SolidColorBrush(Colors.LightGray);
        private static Brush GreenColorBrush = new SolidColorBrush(Colors.Green);

        private IDbConnectionConfigurator _configurator;


        public ConnectionViewModel(INavigationService navigationService,
            IMessagingService messagingService) :
            base(navigationService, messagingService)
        {
            SaveConnectionStringCommand = new Command(SaveConnectionStringCommandAction);
            TestConnectionStringCommand = new Command(TestConnectionStringCommandAction);
            TestFillColor = NeutralBrush;
        }

        private Brush _testFillColor;

        public Brush TestFillColor
        {
            get => _testFillColor;
            set
            {
                OnPropertyChanging();
                _testFillColor = value;
                OnPropertyChanged();
            }
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

        #region Save Connection Command

        private ICommand _testConnectionStringCommand;
        public ICommand TestConnectionStringCommand
        {
            get => _testConnectionStringCommand;
            set
            {
                OnPropertyChanging();
                _testConnectionStringCommand = value;
                OnPropertyChanged();
            }
        }

        private async void TestConnectionStringCommandAction(object parameter)
        {
            try
            {
                await new SqlConnection(ConnectionString).OpenAsync();
                TestFillColor = GreenColorBrush;
            }
            catch (Exception ex)
            {
                MessagingService.DisplayMessage("Error", ex.Message);
                TestFillColor = RedColorBrush;
            }
        }

        #endregion
    }
}