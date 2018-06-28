using System;
using System.ComponentModel.Design;
using System.Windows;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.Common.Infastructure;
using DatabaseImporter.WPF.Infastructure;
using DatabaseImporter.WPF.Infastructure.Database.Connection;

namespace DatabaseImporter.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceLocator ServiceLocator { get; } = new ServiceLocator();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DatabaseImporterContext.Initialize();
        }
    }
}
