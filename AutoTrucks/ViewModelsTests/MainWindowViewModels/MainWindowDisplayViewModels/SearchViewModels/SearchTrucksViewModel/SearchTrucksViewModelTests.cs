using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.MainWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Service.AddNewWindowFactory;
using Model.DataFromView;
using Model.DataToView;
using Moq;
using FluentAssertions;
using Service.DataConvertService;
using Service.ConnexionService;
using ViewModels.PopUpWindowViewModels;
using Model.ReceiveData.CreateSearch;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using Model.DataHelpers;

namespace ViewModels.MainWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class SearchTrucksViewModelTests
    {
        Mock<IDataConvertSingleton> dataConvertSingleton;
        Mock<ISessionCacheSingleton> sessionCacheSingleton;
        Mock<ISearchWindowViewModel> searchWindowViewModel;
        Mock<IConnectConnexionService> connectConnexionService;
        Mock<IWindowFactory> windowFactory;
        SearchTrucksViewModel searchTrucksViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {           
            dataConvertSingleton = new Mock<IDataConvertSingleton>();
            sessionCacheSingleton = new Mock<ISessionCacheSingleton>();
            searchWindowViewModel = new Mock<ISearchWindowViewModel>();
            connectConnexionService = new Mock<IConnectConnexionService>();
            windowFactory = new Mock<IWindowFactory>();
            searchTrucksViewModel = new SearchTrucksViewModel(windowFactory.Object, dataConvertSingleton.Object,
                sessionCacheSingleton.Object, searchWindowViewModel.Object, connectConnexionService.Object);
        }

        [TestMethod()]
        public void TrucksModelGetSetTest()
        {
            SearchCreated truck = new SearchCreated()
            {
                Age = DateTime.Now,
                DHO = new Mileage()
                {
                    miles = 10
                }
            };
            searchTrucksViewModel.Trucks = new ObservableCollection<SearchCreated>();
            searchTrucksViewModel.Trucks.Add(truck);

            Assert.AreEqual(truck, searchTrucksViewModel.Trucks[0]);
        }

        [TestMethod()]
        public void OpenSearchWindowSaveDataWithNullTest()
        {
            ICommand ic = searchTrucksViewModel.OpenSearchWindowCommand;
            searchWindowViewModel.Setup(x => x.saveData).Returns(true);
            ic.Execute(this);
            Assert.AreEqual(0, searchTrucksViewModel.SearchesToDisplay.Count);
        }

        [TestMethod()]
        public void OpenSearchWindowDontSaveDataWithDataTest()
        {
            ICommand ic = searchTrucksViewModel.OpenSearchWindowCommand;
            searchWindowViewModel.Setup(x => x.saveData).Returns(false);
            searchWindowViewModel.Setup(x => x.searchData).Returns(new SearchDataFromView());
            ic.Execute(this);
            Assert.AreEqual(0, searchTrucksViewModel.SearchesToDisplay.Count);
        }

        [TestMethod()]
        public void OpenSearchWindowSaveDataWithDataTest()
        {
            ICommand ic = searchTrucksViewModel.OpenSearchWindowCommand;
            searchWindowViewModel.Setup(x => x.saveData).Returns(true);
            searchWindowViewModel.Setup(x => x.searchData).Returns(new SearchDataFromView());
            ic.Execute(this);
            Assert.AreEqual(1, searchTrucksViewModel.SearchesToDisplay.Count);
        }

        [TestMethod()]
        public void FailToSearchDueToNoSessionTest()
        {
            ICommand ic = searchTrucksViewModel.SearchForSelectedTruckCommand;
            sessionCacheSingleton.Setup(x => x.sessions).Returns(new List<ISessionFacade>());
            searchTrucksViewModel.SearchesToDisplay.Add(new SearchAssetsSearches());
            ic.Execute(this);
            Assert.IsNull(searchTrucksViewModel.Trucks);
        }

        [TestMethod()]
        public void FailToSearchDueToNoDisplayDataTest()
        {
            List<ISessionFacade> sessions = new List<ISessionFacade>();
            sessions.Add(null);
            ICommand ic = searchTrucksViewModel.SearchForSelectedTruckCommand;
            sessionCacheSingleton.Setup(x => x.sessions).Returns(sessions);
            ic.Execute(this);
            Assert.IsNull(searchTrucksViewModel.Trucks);
        }

        [TestMethod()]
        public void SearchSuccessTest()
        {           
            List<ISessionFacade> sessions = new List<ISessionFacade>();
            sessions.Add(null);
            ICommand ic = searchTrucksViewModel.SearchForSelectedTruckCommand;
            dataConvertSingleton.Setup(x => x.EquipmentCreateSearchSuccessDataToSearchCreated(It.IsAny<CreateSearchSuccessData>(), It.IsAny<DataColors>()))
                .Returns(new ObservableCollection<SearchCreated>() { new SearchCreated() });
            sessionCacheSingleton.Setup(x => x.sessions).Returns(sessions);
            searchTrucksViewModel.SearchesToDisplay.Add(new SearchAssetsSearches()
            {
                Marked = true
            });
            searchTrucksViewModel.SearchesToDisplay[0].SearchData = new SearchDataFromView();
            ic.Execute(this);
            Assert.IsTrue(searchTrucksViewModel.Trucks.Count > 0);       
        }

        [TestMethod()]
        public void OnTrucksChangeTest()
        {
            var receivedEvents = new List<string>();
            searchTrucksViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
            var value = new ObservableCollection<SearchCreated>();
            searchTrucksViewModel.Trucks = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("Trucks", receivedEvents[0]);
            Assert.AreEqual(value, searchTrucksViewModel.Trucks);
        }

    }
}