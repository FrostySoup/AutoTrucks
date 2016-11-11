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
            httpService = new Mock<IHttpService>();

            postTrucksViewModel = new PostTrucksViewModel(windowFactory.Object, postWindowViewModel.Object, connectConnexionService.Object,
                sessionCacheSingleton.Object, dataExtractService.Object, dataConvertService.Object, httpService.Object);
            receivedEvents = new List<string>();
            postTrucksViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
        }
        //Not implemented yet
        [TestMethod()]
        public void PostTruckCommandTest()
        {
            //ICommand ic = postTrucksViewModel.PostTruckCommand;
           // ic.Execute(this);
            //Assert.AreEqual(false, postTrucksViewModel.);
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
        public void RemoveAssetsRemoveOneAssetTestTest()
        {

            PostDataFromView value = new PostDataFromView()
            {
                Marked = true,
                ID = "ID"
            };
            ICommand ic = postTrucksViewModel.RemoveAssetsCommand;

            Mock<ISessionFacade> session = new Mock<ISessionFacade>();

            sessionCacheSingleton.Setup(x => x.sessions).Returns(new List<ISessionFacade>() { session.Object });
            connectConnexionService.Setup(x => x.QueryAllMyAssets(It.IsAny<ISessionFacade>())).Returns(new LookupAssetSuccessData());
            dataExtractService.Setup(x => x.ExtractEquipmentFromData(It.IsAny<LookupAssetSuccessData>(), null)).Returns(new ObservableCollection<PostDataFromView>()
            { value });
            postTrucksViewModel.PostToDisplay = new ObservableCollection<PostDataFromView>()
            {
                new PostDataFromView(),
                value,
                value
            };

            ic.Execute(this);
            Assert.AreEqual(1, postTrucksViewModel.PostToDisplay.Count);
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
            //var postData = Mock.Of<PostDataFromView>(m =>
             //  m.ID == "MyID");

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

        //Just for code coverage, this code is default get
        [TestMethod()]
        public void UselessGetTests()
        {
            var postToDisplay = postTrucksViewModel.PostToDisplay;
        }
    }
}