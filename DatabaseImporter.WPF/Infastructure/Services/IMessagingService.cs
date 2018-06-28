using System.Threading.Tasks;

namespace DatabaseImporter.WPF.Infastructure.Services
{
    public interface IMessagingService
    {
        void DisplayMessage(string title, string message);
    }
}