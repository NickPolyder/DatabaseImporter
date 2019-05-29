using DatabaseImporter.WPF.Infastructure.Services;
using DatabaseImporter.WPF.Infrastructure.ViewModels;
using Moq;
using Xunit;

namespace DatabaseImporter.WPF.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public void Exit_Command_Should_Call_Exit_On_NavigationService()
        {
            var mockMessageService = new Mock<IMessagingService>();
            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Verify(tt => tt.Exit(), Times.AtMost(1));

            var viewModel = new MainViewModel(mockNavigationService.Object, mockMessageService.Object);

            viewModel.ExitCommand.Execute(null);

            mockNavigationService.Verify();
        }
    }
}