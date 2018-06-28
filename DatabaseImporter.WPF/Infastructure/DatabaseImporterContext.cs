using System.Windows;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.Common.Infastructure;
using DatabaseImporter.WPF.Infastructure.Database.Connection;
using DatabaseImporter.WPF.Infastructure.ViewModels;

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
            ServiceLocator.AddSingleton<IDbConnectionConfigurator, AppSettingsDatabaseConfigurator>();

            InitializeViewModelLocator();
        }

        private void InitializeViewModelLocator()
        {
            ServiceLocator.AddTransient<MainWindow,MainViewModel>();
        }
        
    }
}