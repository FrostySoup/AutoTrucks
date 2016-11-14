using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Moq;
using ViewModels.PopUpWindowViewModels;
using Service.AddNewWindowFactory;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels.RemoteConnectionViewModels;

namespace ViewModels.MainWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class TopButtonsViewModelTests
    {
        Mock<IDataSourceViewModel> dataSourceViewModel;
        Mock<IRemoteConnectionViewModel> remoteConnectionViewModel;
        Mock<IWindowFactory> windowFactory;
        TopButtonsViewModel topButtonsViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataSourceViewModel = new Mock<IDataSourceViewModel>();
            windowFactory = new Mock<IWindowFactory>();
            remoteConnectionViewModel = new Mock<IRemoteConnectionViewModel>();
            topButtonsViewModel = new TopButtonsViewModel(windowFactory.Object, dataSourceViewModel.Object, remoteConnectionViewModel.Object);
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