using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.MainWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DataConvertService;
using Moq;
using ViewModels.PopUpWindowViewModels;
using Service.AddNewWindowFactory;
using Service.ConnexionService;
using System.Diagnostics.CodeAnalysis;

namespace ViewModels.MainWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class SearchLoadsViewModelTests
    {

        Mock<IDataConvertSingleton> dataConvertSingleton;
        Mock<ISessionCacheSingleton> sessionCacheSingleton;
        Mock<ISearchWindowViewModel> searchWindowViewModel;
        Mock<IConnectConnexionService> connectConnexionService;
        Mock<IWindowFactory> windowFactory;

        SearchLoadsViewModel searchTrucksViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataConvertSingleton = new Mock<IDataConvertSingleton>();
            sessionCacheSingleton = new Mock<ISessionCacheSingleton>();
            searchWindowViewModel = new Mock<ISearchWindowViewModel>();
            connectConnexionService = new Mock<IConnectConnexionService>();
            windowFactory = new Mock<IWindowFactory>();
            searchTrucksViewModel = new SearchLoadsViewModel(dataConvertSingleton.Object, sessionCacheSingleton.Object, 
                searchWindowViewModel.Object, connectConnexionService.Object);
        }
        [TestMethod()]
        public void SearchLoadsViewModelTest()
        {

        }
    }
}