using System.Windows.Input;
using DatabaseImporter.WPF.Infastructure.Utilities;

namespace DatabaseImporter.WPF.Infastructure.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            OpenConnectionItem = new Command(OpenConnectionItemCommand);
        }

        private ICommand _openConnectionItem;
        public ICommand OpenConnectionItem
        {
            get => _openConnectionItem;
            set
            {
                OnPropertyChanging();
                _openConnectionItem = value;
                OnPropertyChanged();
            }
        }

        private void OpenConnectionItemCommand(object parameter)
        {
            System.Diagnostics.Debug.WriteLine("geiaaaa!");
        }
    }
}