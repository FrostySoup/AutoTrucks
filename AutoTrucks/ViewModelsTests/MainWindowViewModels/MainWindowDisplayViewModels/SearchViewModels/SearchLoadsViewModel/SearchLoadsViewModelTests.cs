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
using System.Collections.ObjectModel;
using Model.ReceiveData.CreateSearch;
using System.Windows.Input;
using Model.DataToView;
using Model.DataFromView;
using Model.DataHelpers;

namespace ViewModels.MainWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class SearchLoadsViewModelTests
    {

        Mock<IDataConvertService> dataConvertSingleton;
        Mock<ISessionCacheSingleton> sessionCacheSingleton;
        Mock<ISearchWindowViewModel> searchWindowViewModel;
        Mock<IConnectConnexionService> connectConnexionService;
        Mock<IWindowFactory> windowFactory;
        SearchLoadsViewModel searchLoadsViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataConvertSingleton = new Mock<IDataConvertService>();
            sessionCacheSingleton = new Mock<ISessionCacheSingleton>();
            searchWindowViewModel = new Mock<ISearchWindowViewModel>();
            connectConnexionService = new Mock<IConnectConnexionService>();
            windowFactory = new Mock<IWindowFactory>();
            searchLoadsViewModel = new SearchLoadsViewModel(windowFactory.Object, dataConvertSingleton.Object,
                sessionCacheSingleton.Object, searchWindowViewModel.Object, connectConnexionService.Object);
        }
        [TestMethod()]
        public void LoadsPropertyChangeTest()
        {
            var value = new ObservableCollection<SearchAssetsReceived>()
            {
                new SearchAssetsReceived()
            };
            searchLoadsViewModel.Assets = value;
            Assert.AreEqual(value, searchLoadsViewModel.Assets);
        }

        [TestMethod()]
        public void SearchSuccessTest()
        {
            List<ISessionFacade> sessions = new List<ISessionFacade>();
            sessions.Add(null);
            ICommand ic = searchLoadsViewModel.SearchForSelectedTruckCommand;

            sessionCacheSingleton.Setup(x => x.sessions).Returns(sessions);
            searchLoadsViewModel.SearchesToDisplay.Add(new SearchAssetsSearches());
            searchLoadsViewModel.SearchesToDisplay[0].SearchData = new SearchDataFromView();
            ic.Execute(this);
            Assert.AreEqual(1, searchLoadsViewModel.SearchesToDisplay.Count);
        }

        [TestMethod()]
        public void OpenSearchWindowSaveDataWithNullTest()
        {
            ICommand ic = searchLoadsViewModel.OpenSearchWindowCommand;
            searchWindowViewModel.Setup(x => x.saveData).Returns(true);
            ic.Execute(this);
            Assert.AreEqual(0, searchLoadsViewModel.SearchesToDisplay.Count);
        }

        [TestMethod()]
        public void OpenSearchWindowSaveDataWithDataTest()
        {
            ICommand ic = searchLoadsViewModel.OpenSearchWindowCommand;
            searchWindowViewModel.Setup(x => x.saveData).Returns(true);
            searchWindowViewModel.Setup(x => x.searchData).Returns(new SearchDataFromView());
            ic.Execute(this);
            Assert.AreEqual(1, searchLoadsViewModel.SearchesToDisplay.Count);
        }

        [TestMethod()]
        public void SearchSuccessEuipmentTest()
        {
            List<ISessionFacade> sessions = new List<ISessionFacade>();
            sessions.Add(null);
            ICommand ic = searchLoadsViewModel.SearchForSelectedTruckCommand;
            dataConvertSingleton.Setup(x => x.ShipmentCreateSearchSuccessDataToSearchAssetsReceived(It.IsAny<CreateSearchSuccessData>(), It.IsAny<DataColors>()))
                .Returns(new ObservableCollection<SearchAssetsReceived>() { new SearchAssetsReceived() });
            sessionCacheSingleton.Setup(x => x.sessions).Returns(sessions);
            searchLoadsViewModel.SearchesToDisplay.Add(new SearchAssetsSearches()
            {
                Marked = true
            });
            searchLoadsViewModel.SearchesToDisplay[0].SearchData = new SearchDataFromView();
            ic.Execute(this);
            Assert.IsTrue(searchLoadsViewModel.Assets.Count > 0);
        }

        [TestMethod()]
        public void SearchEquipmentSuccessTest()
        {
            List<ISessionFacade> sessions = new List<ISessionFacade>();
            sessions.Add(null);
            ICommand ic = searchLoadsViewModel.SearchForSelectedTruckCommand;

            sessionCacheSingleton.Setup(x => x.sessions).Returns(sessions);
            searchLoadsViewModel.SearchesToDisplay.Add(new SearchAssetsSearches());
            searchLoadsViewModel.SearchesToDisplay[0].SearchData = new SearchDataFromView();
            ic.Execute(this);
            Assert.IsTrue(searchLoadsViewModel.Assets.Count == 0);
        }
    }
}