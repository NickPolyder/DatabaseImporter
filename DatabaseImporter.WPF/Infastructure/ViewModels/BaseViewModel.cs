using System.ComponentModel;
using System.Runtime.CompilerServices;
using DatabaseImporter.WPF.Infastructure.Services;

namespace DatabaseImporter.WPF.Infastructure.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        protected readonly INavigationService NavigationService;
        protected readonly IMessagingService MessagingService;

        protected BaseViewModel():this(null,null)
        { }
        
        protected BaseViewModel(INavigationService navigationService, IMessagingService messagingService)
        {
            NavigationService = navigationService ?? CurrentContext.ServiceLocator.GetService<INavigationService>(); ;
            MessagingService = messagingService ?? CurrentContext.ServiceLocator.GetService<IMessagingService>();
        }
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected DatabaseImporterContext CurrentContext => DatabaseImporterContext.Current;

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                OnPropertyChanging();
                _title = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}