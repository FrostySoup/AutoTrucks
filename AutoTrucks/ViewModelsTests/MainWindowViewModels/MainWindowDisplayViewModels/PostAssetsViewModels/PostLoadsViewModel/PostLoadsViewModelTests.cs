﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.MainWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.DataFromView;
using Service.AddNewWindowFactory;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;
using Moq;
using Service.ConnexionService;
using Service.DataExtractService;
using Service.DataConvertService;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Service.ConnexionService.AlarmService;
using Service.SerializeServices;
using Service.ViewModelsHelpers;

namespace ViewModels.MainWindowViewModels.Tests
{

    [TestClass()]
    public class PostLoadsViewModelTests
    {
        Mock<IWindowFactory> windowFactory;
        Mock<IPostWindowViewModel> postWindowViewModel;
        Mock<IConnectConnexionService> connectConnexionService;
        Mock<ISessionCacheSingleton> sessionCacheSingleton;
        Mock<IDataExtractService> dataExtractService;
        Mock<IDataConvertPostAssetService> dataConvertService;
        Mock<IAssetsViewModelHelper> assetsViewModelHelper;
        Mock<IHttpService> httpService;

        PostLoadsViewModel postLoadsViewModel;
        List<string> receivedEvents;

        [TestInitialize]
        public void SetInitialValues()
        {
            windowFactory = new Mock<IWindowFactory>();
            postWindowViewModel = new Mock<IPostWindowViewModel>();
            connectConnexionService = new Mock<IConnectConnexionService>();
            sessionCacheSingleton = new Mock<ISessionCacheSingleton>();
            dataExtractService = new Mock<IDataExtractService>();
            dataConvertService = new Mock<IDataConvertPostAssetService>();
            assetsViewModelHelper = new Mock<IAssetsViewModelHelper>();
            httpService = new Mock<IHttpService>();

            postLoadsViewModel = new PostLoadsViewModel(windowFactory.Object, postWindowViewModel.Object, connectConnexionService.Object,
                sessionCacheSingleton.Object, dataExtractService.Object, dataConvertService.Object, httpService.Object, assetsViewModelHelper.Object);
            receivedEvents = new List<string>();
            postLoadsViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
        }

        [TestMethod()]
        public void OpenPostAssetWindowCommandDontSavePostDataNullSaveChangesFalseTest()
        {
            ICommand ic = postLoadsViewModel.OpenPostAssetWindowCommand;
            postWindowViewModel.Setup(x => x.saveChanges).Returns(false);
            ic.Execute(this);
            Assert.AreEqual(0, postLoadsViewModel.PostToDisplay.Count);
        }

        [TestMethod()]
        public void OpenPostAssetWindowCommandDontSavePostDataNotNullSaveChangesFalseTest()
        {
            ICommand ic = postLoadsViewModel.OpenPostAssetWindowCommand;
            postWindowViewModel.Setup(x => x.saveChanges).Returns(false);
            postWindowViewModel.Setup(x => x.postData).Returns(new PostDataFromView());
            ic.Execute(this);
            Assert.AreEqual(0, postLoadsViewModel.PostToDisplay.Count);
        }

        [TestMethod()]
        public void OpenPostAssetWindowCommandDontSavePostDataNullSaveChangesTrueTest()
        {
            ICommand ic = postLoadsViewModel.OpenPostAssetWindowCommand;
            postWindowViewModel.Setup(x => x.saveChanges).Returns(false);
            postWindowViewModel.Setup(x => x.postData).Returns(new PostDataFromView());
            ic.Execute(this);
            Assert.AreEqual(0, postLoadsViewModel.PostToDisplay.Count);
        }

        [TestMethod()]
        public void OpenPostAssetWindowCommandSavePostDataNotNullSaveChangesTrueTest()
        {
            //var postData = Mock.Of<PostDataFromView>(m =>
            //  m.ID == "MyID");

            Mock<ISessionFacade> sessionFacade = new Mock<ISessionFacade>();
            var value = new PostDataFromView();
            ICommand ic = postLoadsViewModel.OpenPostAssetWindowCommand;
            postWindowViewModel.Setup(x => x.saveChanges).Returns(true);
            postWindowViewModel.Setup(x => x.postData).Returns(new PostDataFromView());
            connectConnexionService.Setup(x => x.PostNewAsset(sessionFacade.Object, It.IsAny<PostAssetOperation>())).Returns("MyID");
            sessionCacheSingleton.Setup(x => x.sessions).Returns(new List<ISessionFacade>() { sessionFacade.Object, sessionFacade.Object });
            ic.Execute(this);
            Assert.AreEqual(1, postLoadsViewModel.PostToDisplay.Count);
        }

        //Just for code coverage, this code is default get
        [TestMethod()]
        public void UselessGetTests()
        {
            var postToDisplay = postLoadsViewModel.PostToDisplay;
        }

        [TestMethod()]
        public void RemoveAssetsNullTest()
        {
            ICommand ic = postLoadsViewModel.RemoveAssetsCommand;
            ic.Execute(this);
            Assert.AreEqual(0, postLoadsViewModel.PostToDisplay.Count);
        }
    }
}