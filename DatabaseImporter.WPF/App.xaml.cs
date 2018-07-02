using System;
using System.ComponentModel.Design;
using System.Windows;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.Common.Infastructure;
using DatabaseImporter.WPF.Infastructure;
using DatabaseImporter.WPF.Infastructure.Database.Connection;
using DatabaseImporter.WPF.Infastructure.Services;
using DatabaseImporter.WPF.Infastructure.Utilities;
using DatabaseImporter.WPF.Infastructure.ViewModels;

namespace DatabaseImporter.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DatabaseImporterContext.Initialize();
            // Create the startup window
            DatabaseImporterContext.Current.ServiceLocator.GetService<INavigationService>().Initialize<MainViewModel>();
        }
       
    }
}
