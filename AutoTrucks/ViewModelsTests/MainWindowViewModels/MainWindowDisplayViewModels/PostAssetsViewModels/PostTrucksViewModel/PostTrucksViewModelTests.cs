using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.MainWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Service.AddNewWindowFactory;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;
using System.ComponentModel;
using System.Windows.Input;
using Model.DataFromView;
using Service.ConnexionService;
using Service.DataExtractService;
using Service.DataConvertService;
using System.Collections.ObjectModel;
using Service.ConnexionService.AlarmService;
using Model.ReceiveData.AlarmMatch;
using System.Windows.Media;
using Service.SerializeServices;
using Service.ViewModelsHelpers;

namespace ViewModels.MainWindowViewModels.Tests
{
    [TestClass()]
    public class PostTrucksViewModelTests
    {
        Mock<IWindowFactory> windowFactory;
        Mock<IPostWindowViewModel> postWindowViewModel;
        Mock<IConnectConnexionService> connectConnexionService;
        Mock<ISessionCacheSingleton> sessionCacheSingleton;
        Mock<IDataExtractService> dataExtractService;
        Mock<IDataConvertPostAssetService> dataConvertService;
        Mock<IAssetsViewModelHelper> assetsViewModelHelper;
        Mock<IHttpService> httpService;

        PostTrucksViewModel postTrucksViewModel;
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
            dataConvertService = new Mock<IDataConvertPostAssetService>();
            httpService = new Mock<IHttpService>();
            assetsViewModelHelper = new Mock<IAssetsViewModelHelper>();
            sessionCacheSingleton.Setup(x => x.remoteURI).Returns(new RemoteConnection());

            postTrucksViewModel = new PostTrucksViewModel(windowFactory.Object, postWindowViewModel.Object, connectConnexionService.Object,
                sessionCacheSingleton.Object, dataExtractService.Object, dataConvertService.Object, httpService.Object, assetsViewModelHelper.Object);
            receivedEvents = new List<string>();
            postTrucksViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
        }

        [TestMethod()]
        public void OpenPostAssetWindowCommandDontSavePostDataNullSaveChangesFalseTest()
        {
            ICommand ic = postTrucksViewModel.OpenPostAssetWindowCommand;
            postWindowViewModel.Setup(x => x.saveChanges).Returns(false);
            ic.Execute(this);
            Assert.AreEqual(0, postTrucksViewModel.PostToDisplay.Count);
        }

        [TestMethod()]
        public void OpenPostAssetWindowCommandDontSavePostDataNotNullSaveChangesFalseTest()
        {
            ICommand ic = postTrucksViewModel.OpenPostAssetWindowCommand;
            postWindowViewModel.Setup(x => x.saveChanges).Returns(false);
            postWindowViewModel.Setup(x => x.postData).Returns(new PostDataFromView());
            ic.Execute(this);
            Assert.AreEqual(0, postTrucksViewModel.PostToDisplay.Count);
        }

        

        [TestMethod()]
        public void OpenPostAssetWindowCommandDontSavePostDataNullSaveChangesTrueTest()
        {
            ICommand ic = postTrucksViewModel.OpenPostAssetWindowCommand;
            postWindowViewModel.Setup(x => x.saveChanges).Returns(false);
            postWindowViewModel.Setup(x => x.postData).Returns(new PostDataFromView());
            ic.Execute(this);
            Assert.AreEqual(0, postTrucksViewModel.PostToDisplay.Count);
        }

        [TestMethod()]
        public void OpenPostAssetWindowCommandSavePostDataNotNullSaveChangesTrueTest()
        {
            Mock<ISessionFacade> sessionFacade = new Mock<ISessionFacade>();
            var value = new PostDataFromView();
            ICommand ic = postTrucksViewModel.OpenPostAssetWindowCommand;
            postWindowViewModel.Setup(x => x.saveChanges).Returns(true);
            postWindowViewModel.Setup(x => x.postData).Returns(new PostDataFromView());
            connectConnexionService.Setup(x => x.PostNewAsset(sessionFacade.Object, It.IsAny<PostAssetOperation>())).Returns("MyID");
            sessionCacheSingleton.Setup(x => x.sessions).Returns(new List<ISessionFacade>() { sessionFacade.Object, sessionFacade.Object });
            ic.Execute(this);
            Assert.AreEqual(1, postTrucksViewModel.PostToDisplay.Count);
        }

        [TestMethod()]
        public void ClearFoundAssetsTest()
        {
            ICommand ic = postTrucksViewModel.ClearFoundAssetsCommand;
            ic.Execute(this);
        }

        [TestMethod()]
        public void UpdateAssetsTest()
        {
            ICommand ic = postTrucksViewModel.AssetUpdatedCommand;
            ic.Execute(this);
        }

        [TestMethod()]
        public void AddToBlacklistCommandTest()
        {
            ICommand ic = postTrucksViewModel.AddToBlackistCommand;
            ic.Execute("name");
        }

        [TestMethod()]
        public void OnIsGroupSelectedChangeToTrueTest()
        {
            Mock<ISessionFacade> sessionFacade = new Mock<ISessionFacade>();
            sessionCacheSingleton.Setup(x => x.sessions).Returns(new List<ISessionFacade>() { sessionFacade.Object, sessionFacade.Object });
            postTrucksViewModel.IsGroupSelected = true;
            Assert.IsTrue(0 < receivedEvents.Count);
            Assert.AreEqual("IsGroupSelected", receivedEvents[0]);
            Assert.IsTrue(postTrucksViewModel.IsGroupSelected);
        }

        [TestMethod()]
        public void OnIsGroupSelectedChangeToFalseTest()
        {
            Mock<ISessionFacade> sessionFacade = new Mock<ISessionFacade>();
            sessionCacheSingleton.Setup(x => x.sessions).Returns(new List<ISessionFacade>() { sessionFacade.Object, sessionFacade.Object });
            postTrucksViewModel.IsGroupSelected = false;
            Assert.IsTrue(0 < receivedEvents.Count);
            Assert.AreEqual("IsGroupSelected", receivedEvents[0]);
            Assert.IsFalse(postTrucksViewModel.IsGroupSelected);
        }

        [TestMethod()]
        public void StopAlarmTest()
        {
            Mock<ISessionFacade> sessionFacade = new Mock<ISessionFacade>();
            sessionCacheSingleton.Setup(x => x.sessions).Returns(new List<ISessionFacade>() { sessionFacade.Object, sessionFacade.Object });
            ICommand ic = postTrucksViewModel.StopAlarmCommand;
            ic.Execute(this);
        }

        [TestMethod()]
        public void StartAlarmPostAssetsNullTest()
        {
            Mock<ISessionFacade> sessionFacade = new Mock<ISessionFacade>();
            sessionCacheSingleton.Setup(x => x.sessions).Returns(new List<ISessionFacade>() { sessionFacade.Object, sessionFacade.Object });
            ICommand ic = postTrucksViewModel.StartAlarmsCommand;
            ic.Execute(this);
        }

        //Just for code coverage, this code is default get
        [TestMethod()]
        public void UselessGetTests()
        {
            var postToDisplay = postTrucksViewModel.PostToDisplay;
            var groupSelected = postTrucksViewModel.IsGroupSelected;
            var foundAssets = postTrucksViewModel.FoundAssets;
        }
    }
}