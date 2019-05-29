using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DatabaseImporter.WPF.Infastructure.Services;
using DatabaseImporter.WPF.Infastructure.Utilities;

namespace DatabaseImporter.WPF.Infastructure.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {

        protected readonly INavigationService NavigationService;
        protected readonly IMessagingService MessagingService;

        protected BaseViewModel() : this(null, null)
        { }

        protected BaseViewModel(INavigationService navigationService, IMessagingService messagingService)
        {
            NavigationService = navigationService ?? CurrentContext.ServiceLocator.GetService<INavigationService>();
            MessagingService = messagingService ?? CurrentContext.ServiceLocator.GetService<IMessagingService>();
            BackCommand = new Command(BackCommandAction);
        }
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected DatabaseImporterContext CurrentContext => DatabaseImporterContext.Current;

        private Guid _windowId;

        internal Guid WindowId
        {
            get => _windowId;
            set
            {
                OnPropertyChanging();
                _windowId = value;
                OnPropertyChanged();
            }
        }

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

        public virtual Task Initialize(object data)
        {
            return Task.CompletedTask;
        }

        #region Exit Command

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get => _backCommand;
            set
            {
                OnPropertyChanging();
                _backCommand = value;
                OnPropertyChanged();
            }
        }

        private void BackCommandAction(object parameter)
        {
            NavigationService.Back(WindowId);
        }


        #endregion

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