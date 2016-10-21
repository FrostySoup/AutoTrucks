using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DataFromView;
using Model.DataHelpers;
using Model.ReceiveData.CreateSearch;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Service.DataConvertService.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class DataConvertSingletonTests
    {
        DataConvertService dataConvertSingleton;
        DataColors dataColors;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataColors = new DataColors()
            {

            };
            dataConvertSingleton = new DataConvertService();
        }

        [TestMethod()]
        public void ToSearchOperationEmptyParamsTest()
        {
            AssetType assetType = AssetType.Shipment;
            SearchDataFromView searchDataFromView = new SearchDataFromView();
            CreateSearchOperation result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
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
            CreateSearchOperation result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchAssetsReceivedEmptyTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchShipmentReceivedNullTest()
        {
            CreateSearchSuccessData searchDataFromView = null;
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.ShipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchShipmentEmptyMatchesTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.ShipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchAssetsReceivedNullTest()
        {
            CreateSearchSuccessData searchDataFromView = null;
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchAssetsReceivedWithParamsNullTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
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
            CreateSearchOperation result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
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
            CreateSearchOperation result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
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
            CreateSearchOperation result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
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
            CreateSearchOperation result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            Assert.AreEqual(-1, result.criteria.limits.weightPounds);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchAssetsReceivedWithParamsSuccessTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.IsTrue(0 < result.Count);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchAssetsReceivedWithAgeParamsSuccessTest()
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
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(date, result[0].Age);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchShipmentReceivedWithAgeParamsSuccessTest()
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
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.ShipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(date, result[0].Age);
        }

        [TestMethod()]
        public void OpenProvinceTest()
        {
            AssetType assetType = AssetType.Equipment;
            SearchDataFromView searchDataFromView = new SearchDataFromView()
            {
                originProvince = StateProvince.AB
            };
            CreateSearchOperation result = dataConvertSingleton.ToSearchOperationParams(searchDataFromView, assetType);
            var destination = result.criteria.destination.Item as SearchOpen;
            Assert.IsNotNull(destination);
        }

        [TestMethod()]
        public void ShipmentPassingWrongTypeTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.ShipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ShipmentCreateSearchSuccessDataToSearchAssetsReceivedWithInitialOParamsSuccessTest()
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
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.ShipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(StateProvince.AB.ToString(), result[0].InitialO);
        }

        [TestMethod()]
        public void CreateSearchSuccessDataToSearchAssetsReceivedWithInitialOParamsSuccessTest()
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
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(StateProvince.AB.ToString(), result[0].InitialO);
        }


        [TestMethod()]
        public void EquipmentCreateSearchSuccessDataToSearchAssetsReceivedTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.IsTrue(0 < result.Count);
        }
        
        [TestMethod()]
        public void EqipmentPassingWrongTypeTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Shipment()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void PhoneNumberNotNullReturnValueTest()
        {
            string phoneValue = "805040";
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
                },
                callback = new PostingCallback()
                {
                    Item = new CallbackPhoneNumber()
                    {
                        phone = new PhoneNumber()
                        {
                            number = phoneValue
                        }
                    }
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(phoneValue, result[0].ContactPhone);
        }

        [TestMethod()]
        public void PostingCallBackWrongTypeTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    Item = new Equipment()
                },
                callback = new PostingCallback()
                {
                    Item = new CallbackEmailAddress()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual("-", result[0].ContactPhone);
        }

        [TestMethod()]
        public void SetDimensionWeigthPoundsLessThenZeroTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    dimensions = new Dimensions()
                    {
                        weightPounds = -100
                    },
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual("-", result[0].Weigth);
        }

        [TestMethod()]
        public void SetDimensionWeigtReturnWithValueTest()
        {
            var value = 100;
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    dimensions = new Dimensions()
                    {
                        weightPounds = value
                    },
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(value.ToString(), result[0].Weigth);
        }

        [TestMethod()]
        public void SetDimensionLengthPoundsLessThenZeroTest()
        {
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    dimensions = new Dimensions()
                    {
                        lengthFeet = -100
                    },
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual("-", result[0].Length);
        }

        [TestMethod()]
        public void SetDimensionLengthReturnWithValueTest()
        {
            var value = 100;
            CreateSearchSuccessData searchDataFromView = new CreateSearchSuccessData();
            searchDataFromView.matches = new MatchingAsset[] { new MatchingAsset() {
                asset = new Asset()
                {
                    dimensions = new Dimensions()
                    {
                        lengthFeet = value
                    },
                    Item = new Equipment()
                }
            } };
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(value.ToString(), result[0].Length);
        }

        [TestMethod()]
        public void EquipmentCreateSearchSuccessDataToSearchAssetsReceivedWithAgeParamsSuccessTest()
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
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(date, result[0].Age);
        }

        [TestMethod()]
        public void EquipmentCreateSearchSuccessDataToSearchAssetsReceivedWithInitialOParamsSuccessTest()
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
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchDataFromView, dataColors);
            Assert.AreEqual(StateProvince.AB.ToString(), result[0].InitialO);
        }

        [TestMethod()]
        public void EquipmentPassNullMatchesTest()
        {
            CreateSearchSuccessData value = new CreateSearchSuccessData();
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(value, dataColors);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void EquipmentPassNullTest()
        {
            CreateSearchSuccessData value = null;
            ObservableCollection<SearchAssetsReceived> result = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(value, dataColors);
            Assert.AreEqual(0, result.Count);
        }
    }
}