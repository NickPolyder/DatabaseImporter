using System.Threading.Tasks;
using System.Windows;

namespace DatabaseImporter.WPF.Infastructure.Services
{
    public class MessagingService:IMessagingService
    {
        public MessagingService()
        {
            
        }

        public void DisplayMessage(string title, string message)
        {
            MessageBox.Show(message, caption: title);
        }
    }
}