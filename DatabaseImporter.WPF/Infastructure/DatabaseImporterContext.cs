using System;
using System.Collections.Generic;
using System.Windows;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.Common.Database.Table;
using DatabaseImporter.Common.Infastructure;
using DatabaseImporter.Database.Connection;
using DatabaseImporter.Database.Table;
using DatabaseImporter.WPF.Infastructure.Database.Connection;
using DatabaseImporter.WPF.Infastructure.Services;
using DatabaseImporter.WPF.Infastructure.ViewModels;
using DatabaseImporter.WPF.Views;

namespace DatabaseImporter.WPF.Infastructure
{
    public class DatabaseImporterContext
    {
        private static DatabaseImporterContext _current;
        public static DatabaseImporterContext Current => _current;
        public IServiceLocator ServiceLocator { get; private set; }

        public static void Initialize()
        {
            _current = new DatabaseImporterContext();
            _current.InitializeServiceLocator();
        }

        private void InitializeServiceLocator()
        {
            ServiceLocator = new ServiceLocator();

            InitializeServices();
            InitializeViewModel();
        }

        private void InitializeServices()
        {
            ServiceLocator.AddSingleton<IDbConnectionConfigurator, AppSettingsDatabaseConfigurator>();
            ServiceLocator.AddSingleton<IMessagingService, MessagingService>();
            ServiceLocator.AddSingleton<INavigationService>((svcLocator) => new NavigationService(GetNavigationMap(), svcLocator));
            ServiceLocator.AddSingleton<ITableServices>((svcLoc) => new MsSqlTableServices(svcLoc.GetService<IDbConnectionConfigurator>(),
                DefaultDbConnectionFactory.Default));
        }

        private void InitializeViewModel()
        {
            ServiceLocator.AddTransient((svcLocator) => new MainViewModel(svcLocator.GetService<INavigationService>(),
                                                  svcLocator.GetService<IMessagingService>()));

            ServiceLocator.AddTransient((svcLocator) => new ConnectionViewModel(svcLocator.GetService<INavigationService>(),
                svcLocator.GetService<IMessagingService>()));
        }


        private static IDictionary<Type, Func<Window>> GetNavigationMap()
        {
            return new Dictionary<Type, Func<Window>>
            {
                [typeof(MainViewModel)] = () => new MainWindow(),
                [typeof(ConnectionViewModel)] = () => new ConnectionWindow(),

            };
        }

    }
}