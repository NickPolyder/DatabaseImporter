using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.Common.Database.Table;
using DatabaseImporter.WPF.Infastructure.Services;
using DatabaseImporter.WPF.Infastructure.Utilities;
using DatabaseImporter.WPF.Infastructure.ViewModels;

namespace DatabaseImporter.WPF.Infrastructure.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		public MainViewModel(INavigationService navigationService,
			IMessagingService messagingService)
		: base(navigationService, messagingService)
		{
			Title = "Database Data Importer";
			OpenConnectionCommand = new Command(OpenConnectionCommandAction);
			RefreshCommand = new Command(RefreshCommandAction);
			ExitCommand = new Command(ExitCommandAction);
			LoadTablesCommand = new Command(LoadTablesCommandAction);
			IsDbTableVisible = Visibility.Hidden;

		}

		#region Load Tables Command

		private System.Windows.Visibility _isDbTableVisible;
		public System.Windows.Visibility IsDbTableVisible
		{
			get => _isDbTableVisible;
			set
			{
				OnPropertyChanging();
				_isDbTableVisible = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<DbTable> _dbTables;

		public ObservableCollection<DbTable> DbTables
		{
			get => _dbTables;
			set
			{
				OnPropertyChanging();
				_dbTables = value;
				OnPropertyChanged();
			}
		}

		private DbTable _selectedTable;

		public DbTable SelectedTable
		{
			get => _selectedTable;
			set
			{
				OnPropertyChanging();
				_selectedTable = value;
				OnPropertyChanged();
			}
		}

		private ICommand _loadTablesCommand;
		public ICommand LoadTablesCommand
		{
			get => _loadTablesCommand;
			set
			{
				OnPropertyChanging();
				_loadTablesCommand = value;
				OnPropertyChanged();
			}
		}

		private async void LoadTablesCommandAction(object parameter)
		{
			var tableServices = CurrentContext.ServiceLocator.GetService<ITableServices>();
			var tablesList = await tableServices.GetTables();
			DbTables = new ObservableCollection<DbTable>(tablesList);
			SelectedTable = DbTables[0];
			IsDbTableVisible = Visibility.Visible;
		}


		#endregion


		#region Open Connection Command

		private ICommand _openConnectionCommand;
		public ICommand OpenConnectionCommand
		{
			get => _openConnectionCommand;
			set
			{
				OnPropertyChanging();
				_openConnectionCommand = value;
				OnPropertyChanged();
			}
		}

		private async void OpenConnectionCommandAction(object parameter)
		{
			await NavigationService.NavigateTo<ConnectionViewModel>(CurrentContext.ServiceLocator.GetService<IDbConnectionConfigurator>());
		}

		#endregion

		#region Refresh Command

		private ICommand _refreshCommand;
		public ICommand RefreshCommand
		{
			get => _refreshCommand;
			set
			{
				OnPropertyChanging();
				_refreshCommand = value;
				OnPropertyChanged();
			}
		}

		private async void RefreshCommandAction(object parameter)
		{
			var tableServices = base.CurrentContext.ServiceLocator.GetService<ITableServices>();
			var tables = await tableServices.GetTables();
			foreach (var table in tables)
			{
				System.Diagnostics.Debug.WriteLine($"Table: {table.Name}");
			}
		}

		#endregion

		#region Exit Command

		private ICommand _exitCommand;
		public ICommand ExitCommand
		{
			get => _exitCommand;
			set
			{
				OnPropertyChanging();
				_exitCommand = value;
				OnPropertyChanged();
			}
		}

		private void ExitCommandAction(object parameter)
		{
			NavigationService.Exit();
		}


		#endregion
	}
}