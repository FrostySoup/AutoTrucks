using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DataFromView;
using Model.ReceiveData.CreateSearch;
using Model.SearchCRUD;
using Model.SendData;
using Service.DataConvertService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataConvertService.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class DataConvertSingletonTests
    {
        DataConvertSingleton dataConvertSingleton;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataConvertSingleton = DataConvertSingleton.Instance;
        }

        [TestMethod()]
        public void ToSearchOperationEmptyParamsTest()
        {
            AssetType assetType = AssetType.Shipment;
            SearchDataFromView searchDataFromView = new SearchDataFromView();
            SearchOperationParams result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            Assert.IsNotNull(result.criteria.assetType);
            Assert.IsNotNull(result.criteria.origin);
            Assert.IsNotNull(result.criteria.destination);
            Assert.IsNotNull(result.criteria.equipmentClasses);
        }

        [TestMethod()]
        public void ToSearchOperationNullShipmentTest()
        {
            AssetType assetType = AssetType.Equipment;
            SearchDataFromView searchDataFromView = null;
            SearchOperationParams result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedEmptyTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            ObservableCollection<SearchCreated> result = dataConvertSingleton.CreateSearchSuccessDataToSearchCreated(searchDataFromView);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedNullTest()
        {
            CreateSearchSuccessData searchDataFromView = null;
            ObservableCollection<SearchCreated> result = dataConvertSingleton.CreateSearchSuccessDataToSearchCreated(searchDataFromView);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedWithParamsNullTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.CreateSearchSuccessDataToSearchCreated(searchDataFromView);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateDimensionsLengthWithValueIntTest()
        {
            AssetType assetType = AssetType.Equipment;
            SearchDataFromView searchDataFromView = new SearchDataFromView()
            {
                length = "50"
            };
            SearchOperationParams result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            Assert.AreEqual(50, result.criteria.limits.lengthFeet);
        }

        [TestMethod()]
        public void CreateDimensionsLengthWithBadValueTest()
        {
            AssetType assetType = AssetType.Equipment;
            SearchDataFromView searchDataFromView = new SearchDataFromView()
            {
                length = "ss5a0"
            };
            SearchOperationParams result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            Assert.AreEqual(0, result.criteria.limits.lengthFeet);
        }

        [TestMethod()]
        public void CreateDimensionsWeigthWithValueIntTest()
        {
            AssetType assetType = AssetType.Equipment;
            SearchDataFromView searchDataFromView = new SearchDataFromView()
            {
                weight = "50"
            };
            SearchOperationParams result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            Assert.AreEqual(50, result.criteria.limits.weightPounds);
        }

        [TestMethod()]
        public void CreateDimensionsWeigthWithBadValueTest()
        {
            AssetType assetType = AssetType.Equipment;
            SearchDataFromView searchDataFromView = new SearchDataFromView()
            {
                weight = "ss5a0"
            };
            SearchOperationParams result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            Assert.AreEqual(0, result.criteria.limits.weightPounds);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedWithParamsSuccessTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Shipment()
                }
            } };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.CreateSearchSuccessDataToSearchCreated(searchDataFromView);
            Assert.IsTrue(0 < result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedWithAgeParamsSuccessTest()
        {
            DateTime date = DateTime.Now;
            string initialO = "test";
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Shipment()
                    {

                    },
                    status = new FmeStatus()
                    {
                        created = new UserTimeStamp()
                        {
                            date = date
                        }
                    }
                }
            } };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.CreateSearchSuccessDataToSearchCreated(searchDataFromView);
            Assert.AreEqual(date, result[0].Age);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedWithInitialOParamsSuccessTest()
        {
            DateTime date = DateTime.Now;
            StateProvince initialO = StateProvince.AB;
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Shipment()
                    {

                    }
                },
                callback = new PostingCallback()
                {
                    postersStateProvince = StateProvince.AB
                }
            } };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.CreateSearchSuccessDataToSearchCreated(searchDataFromView);
            Assert.AreEqual(StateProvince.AB.ToString(), result[0].InitialO);
        }
    }
}