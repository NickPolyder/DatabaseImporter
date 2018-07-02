using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using DatabaseImporter.Common.Infastructure;
using DatabaseImporter.WPF.Infastructure.ViewModels;

namespace DatabaseImporter.WPF.Infastructure.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ReadOnlyDictionary<Type, Func<Window>> _navigationMap;
        private readonly IServiceLocator _serviceLocator;
        private Dictionary<Guid, Window> _windowsOpened;
        public NavigationService(IDictionary<Type, Func<Window>> navigationMap, IServiceLocator serviceLocator)
        {
            if (navigationMap == null || navigationMap.Count == 0)
                throw new ArgumentException($"The {nameof(navigationMap)} must be non empty Dictionary");
            _navigationMap = new ReadOnlyDictionary<Type, Func<Window>>(navigationMap);

            _serviceLocator = serviceLocator ?? DatabaseImporterContext.Current.ServiceLocator;
            _windowsOpened = new Dictionary<Guid, Window>(5);
        }

        public Task Initialize<TStartNavigate>() where TStartNavigate : BaseViewModel
        {
            return NavigateTo<TStartNavigate>();
        }

        public Task NavigateTo<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            async Task GenerateViewModel(Window window, Guid windowId, object o)
            {
                var viewModel = _serviceLocator.GetService<TViewModel>();
                viewModel.WindowId = windowId;
                window.DataContext = viewModel;
                await viewModel.Initialize(o);
            }

            var taskCompletionSource = new TaskCompletionSource<bool>();
            Application.Current.Dispatcher.Invoke(async () =>
            {
                try
                {
                    var windowId = Guid.NewGuid();
                    var window = CreateWindow(typeof(TViewModel));

                    await GenerateViewModel(window, windowId, parameter);
                    window.Show();

                    _windowsOpened.Add(windowId, window);
                    taskCompletionSource.TrySetResult(true);
                }
                catch (TaskCanceledException ex) when (taskCompletionSource.TrySetCanceled(ex.CancellationToken))
                { }
                catch (OperationCanceledException ex) when (taskCompletionSource.TrySetCanceled(ex.CancellationToken))
                { }
                catch (Exception ex) when (taskCompletionSource.TrySetException(ex))
                { }
            });

            return taskCompletionSource.Task;
        }

        private Window CreateWindow(Type viewModel)
        {
            if (_navigationMap.ContainsKey(viewModel))
            {
                return _navigationMap[viewModel].Invoke();
            }

            throw new NavigationNotFoundException(viewModel);
        }

        public Task Back(Guid windowId)
        {
            if (_windowsOpened.ContainsKey(windowId))
            {
                var lastWindowOpened = _windowsOpened[windowId];
                if (lastWindowOpened != Application.Current.MainWindow)
                {
                    lastWindowOpened.Hide();
                }
            }

            return Task.CompletedTask;
        }
        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}