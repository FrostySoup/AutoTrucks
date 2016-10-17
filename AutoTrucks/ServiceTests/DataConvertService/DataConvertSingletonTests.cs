using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DataFromView;
using Model.DataHelpers;
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
        DataColors dataColors;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataColors = new DataColors()
            {

            };
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
            ObservableCollection<SearchCreated> result = dataConvertSingleton.TrucksCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedNullTest()
        {
            CreateSearchSuccessData searchDataFromView = null;
            ObservableCollection<SearchCreated> result = dataConvertSingleton.TrucksCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedWithParamsNullTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.TrucksCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
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
            Assert.AreEqual(-1, result.criteria.limits.lengthFeet);
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
            Assert.AreEqual(-1, result.criteria.limits.weightPounds);
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
            ObservableCollection<SearchCreated> result = dataConvertSingleton.TrucksCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.IsTrue(0 < result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedWithAgeParamsSuccessTest()
        {
            DateTime date = DateTime.Now;
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
            ObservableCollection<SearchCreated> result = dataConvertSingleton.TrucksCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.AreEqual(date, result[0].Age);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.InvalidCastException))]
        public void TruckPassingWrongTypeTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.TrucksCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.IsTrue(0 < result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchCreatedWithInitialOParamsSuccessTest()
        {
            DateTime date = DateTime.Now;
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
            ObservableCollection<SearchCreated> result = dataConvertSingleton.TrucksCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.AreEqual(StateProvince.AB.ToString(), result[0].InitialO);
        }


        [TestMethod()]
        public void EquipmentCreateSearchSuccessDataToSearchCreatedTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.IsTrue(0 < result.Count);
        }
        
        [TestMethod()]
        [ExpectedException(typeof(System.InvalidCastException))]
        public void EqipmentPassingWrongTypeTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Shipment()
                }
            } };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.IsTrue(0 < result.Count);
        }

        [TestMethod()]
        public void EquipmentCreateSearchSuccessDataToSearchCreatedWithAgeParamsSuccessTest()
        {
            DateTime date = DateTime.Now;
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
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
            ObservableCollection<SearchCreated> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.AreEqual(date, result[0].Age);
        }

        [TestMethod()]
        public void EquipmentCreateSearchSuccessDataToSearchCreatedWithInitialOParamsSuccessTest()
        {
            DateTime date = DateTime.Now;
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
                    {

                    }
                },
                callback = new PostingCallback()
                {
                    postersStateProvince = StateProvince.AB
                }
            } };
            ObservableCollection<SearchCreated> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchCreated(searchDataFromView, dataColors);
            Assert.AreEqual(StateProvince.AB.ToString(), result[0].InitialO);
        }

        [TestMethod()]
        public void EquipmentPassNullMatchesTest()
        {
            CreateSearchSuccessData value = new CreateSearchSuccessData();
            ObservableCollection<SearchCreated> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchCreated(value, dataColors);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void EquipmentPassNullTest()
        {
            CreateSearchSuccessData value = null;
            ObservableCollection<SearchCreated> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchCreated(value, dataColors);
            Assert.AreEqual(0, result.Count);
        }
    }
}