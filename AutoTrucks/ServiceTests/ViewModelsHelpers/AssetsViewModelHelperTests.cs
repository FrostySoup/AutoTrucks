using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DataFromView;
using Model.ReceiveData.AlarmMatch;
using Moq;
using Service.ConnexionService;
using Service.ConnexionService.AlarmService;
using Service.SerializeServices;
using Service.ViewModelsHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Service.ViewModelsHelpers.Tests
{
    [TestClass()]
    public class AssetsViewModelHelperTests
    {

        Mock<ISerializeService> serializeService;
        Mock<ISessionFacade> session;
        Mock<IConnectConnexionService> connectConnexionService;
        Mock<IHttpService> httpService;
        AlarmSearchCriteria alarmSearchCriteria;

        AssetsViewModelHelper assetsViewModelHelper;

        [TestInitialize]
        public void SetInitialValues()
        {
            alarmSearchCriteria = new AlarmSearchCriteria()
            {
                maxMatches = 30,
                maxMatchesSpecified = true,
                originRadius = new Mileage()
                {
                    method = MileageType.Road,
                    miles = 50
                },
                destinationRadius = new Mileage()
                {
                    method = MileageType.Road,
                    miles = 0
                }
            };

            httpService = new Mock<IHttpService>();           
            serializeService = new Mock<ISerializeService>();
            session = new Mock<ISessionFacade>();
            connectConnexionService = new Mock<IConnectConnexionService>();

            setupHttpService();
            setupSerializer();            

            assetsViewModelHelper = new AssetsViewModelHelper(serializeService.Object);
        }

        private void setupHttpService()
        {
            DisplayFoundAsset displayFoundAsset = new DisplayFoundAsset()
            {
                AssetId = "AssetId1"
            };
            var postDataFromView = new ObservableCollection<DisplayFoundAsset>();
            postDataFromView.Add(displayFoundAsset);

            displayFoundAsset.AssetId = "AssetId2";
            postDataFromView.Add(displayFoundAsset);

            DisplayFoundAsset displayFoundAsset2 = new DisplayFoundAsset()
            {
                AssetId = "AssetId1",
                CompanyName = "IGN"
            };

            postDataFromView.Add(displayFoundAsset2);

            httpService.Setup(x => x.GetAssets())
                .Returns(postDataFromView);
        }

        private void setupSerializer()
        {
            List<string> blacklistedCompanies = new List<string>();
            blacklistedCompanies.Add("IGN");

            serializeService.Setup(x => x.DeserializeCompanyName())
                .Returns(blacklistedCompanies);
        }

        [TestMethod()]
        public void RemoveAssetsRemoveThreeAssetTest()
        {
            var postDataFromViewList = new ObservableCollection<PostDataFromView>();

            PostDataFromView value = new PostDataFromView()
            {
                Marked = true,
                ID = "ID"
            };

            postDataFromViewList.Add(value);
            postDataFromViewList.Add(value);
            postDataFromViewList.Add(value);

            value = new PostDataFromView()
            {
                Marked = false,
                ID = "ID2"
            };
            postDataFromViewList.Add(value);
            postDataFromViewList.Add(value);

            Mock<ISessionFacade> session = new Mock<ISessionFacade>();
            Mock<IConnectConnexionService> connectConnexionService = new Mock<IConnectConnexionService>();

            var results = assetsViewModelHelper.RemoveSelectedAssets(session.Object, postDataFromViewList, connectConnexionService.Object);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod()]
        public void RemoveAssetsRemoveEmptyAssetTest()
        {

            var postDataFromViewList = new ObservableCollection<PostDataFromView>();

            var results = assetsViewModelHelper.RemoveSelectedAssets(session.Object, postDataFromViewList, connectConnexionService.Object);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod()]
        public void AddAlarmForAssetDHO0ValueTest()
        {
            setupAlarmForAssetCreation(0);
        }

        [TestMethod()]
        public void AddAlarmForAssetDHO100DHD100ValueTest()
        {
            setupAlarmForAssetCreation(100);
        }

        private void setupAlarmForAssetCreation(int value)
        {
            var postDataValue = new PostDataFromView()
            {
                DHO = value,
                DHD = value
            };

            alarmSearchCriteria.originRadius = new Mileage()
            {
                method = MileageType.Road,
                miles = value
            };

            alarmSearchCriteria.destinationRadius = new Mileage()
            {
                method = MileageType.Road,
                miles = value
            };

            var alarmSearchCriteriaResult = new AlarmSearchCriteria();

            Mock<ISessionFacade> session = new Mock<ISessionFacade>();

            connectConnexionService.Setup(x => x.CreateAlarm(It.IsAny<ISessionFacade>(), It.IsAny<string>(), It.IsAny<AlarmSearchCriteria>()))
                .Callback<ISessionFacade, string, AlarmSearchCriteria>((x, y, obj) => alarmSearchCriteriaResult = obj)
                .Returns(new Alarm());

            var result = assetsViewModelHelper.AddAlarmForAsset(session.Object, postDataValue, connectConnexionService.Object);

            connectConnexionService.Verify(c => c.CreateAlarm(It.IsAny<ISessionFacade>(), It.IsAny<string>(), It.IsAny<AlarmSearchCriteria>()), Times.Once());

            Assert.AreEqual(alarmSearchCriteria.originRadius.miles, alarmSearchCriteriaResult.originRadius.miles);
            Assert.AreEqual(alarmSearchCriteria.destinationRadius.miles, alarmSearchCriteriaResult.destinationRadius.miles);
        }

        [TestMethod()]
        public void GetAssetsFomHttpServiceRemoveBlacklistedTest()
        {
            string assetIdValue = "assetId1";
            PostDataFromView basicPostDataFromView = new PostDataFromView()
            {
                ID = assetIdValue
            };
            var postAssets = new ObservableCollection<PostDataFromView>();
            postAssets.Add(basicPostDataFromView);
            postAssets.Add(basicPostDataFromView);

            var result = assetsViewModelHelper.GetAssetsFromHttpService(httpService.Object, postAssets);

            Assert.AreEqual(2, result.Count);
        }

    }
}