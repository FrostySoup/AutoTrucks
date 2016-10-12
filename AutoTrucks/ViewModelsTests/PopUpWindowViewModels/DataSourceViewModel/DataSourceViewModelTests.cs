using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.PopUpWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Service.AddNewWindowFactory;
using Moq;
using Service.SerializeServices;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Model.SendData;

namespace ViewModels.PopUpWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class DataSourceViewModelTests
    {
        Mock<ISerializeService> serializeService;
        Mock<ILoginViewModel> loginViewModel;
        Mock<IWindowFactory> windowFactory;
        DataSourceViewModel dataSourceViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            serializeService = new Mock<ISerializeService>();
            loginViewModel = new Mock<ILoginViewModel>();
            windowFactory = new Mock<IWindowFactory>();
            dataSourceViewModel = new DataSourceViewModel(windowFactory.Object, serializeService.Object, loginViewModel.Object);
        }

        [TestMethod()]
        public void RemoveSelectedDataSourceNoneTest()
        {
            dataSourceViewModel.DataSources = new ObservableCollection<DataSource>();
            ICommand ic = dataSourceViewModel.DeleteSelectedDataSourcesCommand;
            ic.Execute(this);
            Assert.AreEqual(0, dataSourceViewModel.DataSources.Count);
        }

        [TestMethod()]
        public void RemoveOneDataSourceTest()
        {
            DataSource dataSourceDontRemove = (new DataSource()
            {
                UserName = "Test"
            });
            dataSourceViewModel.DataSources = new ObservableCollection<DataSource>();
            dataSourceViewModel.DataSources.Add(dataSourceDontRemove);
            dataSourceViewModel.DataSources.Add(new DataSource()
            {
                Selected = true
            });
            ICommand ic = dataSourceViewModel.DeleteSelectedDataSourcesCommand;
            ic.Execute(this);
            Assert.AreEqual(dataSourceDontRemove, dataSourceViewModel.DataSources[0]);
        }

        [TestMethod()]
        public void OpenWindowCommandLoginCredentialsNullTest()
        {
            ICommand ic = dataSourceViewModel.OpenWindowCommand;
            loginViewModel.Setup(x => x.loginCredentials).Returns(new Login());
            ic.Execute(this);
        }

        [TestMethod()]
        public void OpenWindowCommandLoginEmptyAndInCompleteTest()
        {
            ICommand ic = dataSourceViewModel.OpenWindowCommand;
            loginViewModel.Setup(x => x.loginCredentials).Returns(new Login());
            ic.Execute(this);
        }

        [TestMethod()]
        public void OpenWindowCommandLoginEmptyAndCompleteSerializeFailTest()
        {
            ICommand ic = dataSourceViewModel.OpenWindowCommand;
            loginViewModel.Setup(x => x.loginCredentials).Returns(new Login());
            loginViewModel.Setup(x => x.loginCompleted).Returns(true);
            ic.Execute(this);
        }

        [TestMethod()]
        public void OpenWindowCommandLoginEmptyAndCompleteSerializeSuccessTest()
        {
            dataSourceViewModel.DataSources = new ObservableCollection<DataSource>();
            ICommand ic = dataSourceViewModel.OpenWindowCommand;
            loginViewModel.Setup(x => x.loginCredentials).Returns(new Login());
            loginViewModel.Setup(x => x.loginCompleted).Returns(true);
            serializeService.Setup(x => x.SerializeDataSource(It.IsAny<DataSource>())).Returns(true);
            ic.Execute(this);
            Assert.AreEqual(1, dataSourceViewModel.DataSources.Count);
        }

    }
}