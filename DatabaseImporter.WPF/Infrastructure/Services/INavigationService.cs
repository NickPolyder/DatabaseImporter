using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using DatabaseImporter.WPF.Infastructure.ViewModels;

namespace DatabaseImporter.WPF.Infastructure.Services
{
    public interface INavigationService
    {
        Task Initialize<TStartNavigate>() where TStartNavigate : BaseViewModel;

        Task NavigateTo<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        void Exit();

        Task Back(Guid windowId);
    }
}