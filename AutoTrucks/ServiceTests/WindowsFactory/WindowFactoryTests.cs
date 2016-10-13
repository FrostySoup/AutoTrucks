using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.AddNewWindowFactory;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.PopUpWindowViewModels;

namespace Service.AddNewWindowFactory.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class WindowFactoryTests
    {
        
        WindowFactory windowFactory;
        Mock<IDataSourceViewModel> dataSourceViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            windowFactory = new WindowFactory();
            dataSourceViewModel = new Mock<IDataSourceViewModel>();
        }
        [TestMethod()]
        public void CloseSearchWindowTest()
        {
            windowFactory.CloseSearchWindow();
        }

        [TestMethod()]
        public void CreateNewDataSourceWindowNullTest()
        {
            windowFactory.CreateNewDataSourceWindow(null);
        }

        [TestMethod()]
        public void CreateNewLoginWindowNullTest()
        {
            windowFactory.CreateNewLoginWindow(null);
        }

        [TestMethod()]
        public void CreateNewSearchWindowNullTest()
        {
            windowFactory.CreateNewSearchWindow(null);
        }

        [TestMethod()]
        public void CreateNewDataSourceWindowEmptyTest()
        {
            windowFactory.CreateNewDataSourceWindow(dataSourceViewModel.Object);
        }

        [TestMethod()]
        public void CreateNewLoginWindowEmptyTest()
        {
            windowFactory.CreateNewLoginWindow(dataSourceViewModel.Object);
        }

        [TestMethod()]
        public void CreateNewSearchWindowEmptyTest()
        {
            windowFactory.CreateNewSearchWindow(dataSourceViewModel.Object);
        }
    }
}