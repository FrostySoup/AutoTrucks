﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            /* OnNotify
            int status = 200;
            searchTrucksViewModel.PropertyChanged += delegate{
                status = 234;
            };*/

            ICommand ic = searchTrucksViewModel.OpenSearchWindowCommand;
            searchWindowViewModel.Setup(x => x.saveData).Returns(true);
            //var searchTrucksViewModel = new SearchTrucksViewModel(mockWindowFactory.Object);
            ic.Execute(this);
        }

        [TestMethod()]
        public void OpenSearchWindowDontSaveDataWithDataTest()
        {
            ICommand ic = searchTrucksViewModel.OpenSearchWindowCommand;
            searchWindowViewModel.Setup(x => x.saveData).Returns(false);
            searchWindowViewModel.Setup(x => x.searchData).Returns(new SearchDataFromView());
            ic.Execute(this);
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
        }

        [TestMethod()]
        public void FailToSearchDueToNoDisplayDataTest()
        {
            List<ISessionFacade> sessions = new List<ISessionFacade>();
            sessions.Add(null);
            ICommand ic = searchTrucksViewModel.SearchForSelectedTruckCommand;
            sessionCacheSingleton.Setup(x => x.sessions).Returns(sessions);
            ic.Execute(this);
        }

        [TestMethod()]
        public void SearchSuccessTest()
        {           
            List<ISessionFacade> sessions = new List<ISessionFacade>();
            sessions.Add(null);
            ICommand ic = searchTrucksViewModel.SearchForSelectedTruckCommand;

            sessionCacheSingleton.Setup(x => x.sessions).Returns(sessions);
            searchTrucksViewModel.SearchesToDisplay.Add(new SearchAssetsSearches());
            searchTrucksViewModel.SearchesToDisplay[0].SearchData = new SearchDataFromView();
            ic.Execute(this);          
        }

    }
}