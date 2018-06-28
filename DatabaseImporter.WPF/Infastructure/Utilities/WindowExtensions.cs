using System.Windows;
using DatabaseImporter.Common.Infastructure;

namespace DatabaseImporter.WPF.Infastructure.Utilities
{
    public static class WindowExtensions
    {
        public static void LoadWindowContext(this Window window)
        {
            window.DataContext = DatabaseImporterContext.Current.ServiceLocator.GetServiceOrDefault(window.GetType().Name);
        }
    }
}