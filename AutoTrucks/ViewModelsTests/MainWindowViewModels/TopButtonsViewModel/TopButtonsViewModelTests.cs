using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.MainWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Moq;
using ViewModels.PopUpWindowViewModels;
using Service.AddNewWindowFactory;
using System.Windows.Input;

namespace ViewModels.MainWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class TopButtonsViewModelTests
    {
        Mock<IDataSourceViewModel> dataSourceViewModel;
        Mock<IWindowFactory> windowFactory;
        TopButtonsViewModel topButtonsViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataSourceViewModel = new Mock<IDataSourceViewModel>();
            windowFactory = new Mock<IWindowFactory>();
            topButtonsViewModel = new TopButtonsViewModel(windowFactory.Object, dataSourceViewModel.Object);
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