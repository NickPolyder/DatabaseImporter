using System.Threading.Tasks;
using System.Windows;

namespace DatabaseImporter.WPF.Infastructure.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}