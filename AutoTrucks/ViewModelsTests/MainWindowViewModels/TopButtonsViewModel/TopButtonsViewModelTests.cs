using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Moq;
using ViewModels.PopUpWindowViewModels;
using Service.AddNewWindowFactory;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels.RemoteConnectionViewModels;
using Service.SerializeServices;
using ViewModels.PopUpWindowViewModels.BlacklistViewModel;
using Service.ConnexionService;

namespace ViewModels.MainWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class TopButtonsViewModelTests
    {
        Mock<IDataSourceViewModel> dataSourceViewModel;
        Mock<IRemoteConnectionViewModel> remoteConnectionViewModel;
        Mock<IWindowFactory> windowFactory;
        Mock<ISerializeService> serializeService;
        Mock<IBlacklistViewModel> blacklistViewModel;
        Mock<ISessionCacheSingleton> sessionCacheSingleton;
        TopButtonsViewModel topButtonsViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataSourceViewModel = new Mock<IDataSourceViewModel>();
            windowFactory = new Mock<IWindowFactory>();
            remoteConnectionViewModel = new Mock<IRemoteConnectionViewModel>();
            blacklistViewModel = new Mock<IBlacklistViewModel>();
            sessionCacheSingleton = new Mock<ISessionCacheSingleton>();
            serializeService = new Mock<ISerializeService>();
            topButtonsViewModel = new TopButtonsViewModel(windowFactory.Object, dataSourceViewModel.Object, remoteConnectionViewModel.Object, serializeService.Object, sessionCacheSingleton.Object, blacklistViewModel.Object);
        }

        [TestMethod()]
        public void OpenNewWindowTest()
        {
            ICommand ic = topButtonsViewModel.OpenWindowCommand;
            ic.Execute(this);
        }

        [TestMethod()]
        public void AddCommandNullValuesTest()
        {
            topButtonsViewModel.AddCommand(null, null);
            ICommand ic = topButtonsViewModel.OpenWindowCommand;
            Assert.AreEqual(null, topButtonsViewModel.ChangePostLoadsViewModelCommand);
            Assert.AreEqual(null, topButtonsViewModel.ChangePostTrucksViewModelCommand);
        }
    }
}