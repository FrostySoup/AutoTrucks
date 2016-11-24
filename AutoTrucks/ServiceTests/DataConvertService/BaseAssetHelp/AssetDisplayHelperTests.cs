using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.DataConvertService.BaseAssetHelp;
using Service.DataConvertService.LocationHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataConvertService.BaseAssetHelp.Tests
{
    [TestClass()]
    public class AssetDisplayHelperTests
    {
        AssetDisplayHelper assetDisplayHelper;
        Mock<ILocationHelper> locationHelper;
        TfmiServices.TfmiAlarmService.BaseAsset baseAsset;
        TfmiServices.TfmiAlarmService.FmeStatus fmeStatus;
        TfmiServices.TfmiAlarmService.PostingCallback postingCallback;
        bool ltl;
        TfmiServices.TfmiAlarmService.Dimensions dimensions;
        string assetId;

        [TestInitialize]
        public void SetInitialValues()
        {
            assetId = "id";
            baseAsset = new TfmiServices.TfmiAlarmService.Shipment()
            {
                destination = new TfmiServices.TfmiAlarmService.Place(),
                equipmentType = TfmiServices.TfmiAlarmService.EquipmentType.Container,
                origin = new TfmiServices.TfmiAlarmService.Place()
            };
            locationHelper = new Mock<ILocationHelper>();
            assetDisplayHelper = new AssetDisplayHelper(locationHelper.Object);
        }

        [TestMethod()]
        public void ConvertAssetAllNullsTest()
        {
            baseAsset = null;
            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);           
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ConvertAssetShipmentTest()
        {
            var value = TfmiServices.TfmiAlarmService.EquipmentType.Container;
            baseAsset = new TfmiServices.TfmiAlarmService.Shipment()
            {
                destination = new TfmiServices.TfmiAlarmService.Place(),
                equipmentType = value,
                origin = new TfmiServices.TfmiAlarmService.Place()
            };

            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value.ToString(), result.Truck);
        }

        [TestMethod()]
        public void ConvertAssetEquipmentTest()
        {
            var value = TfmiServices.TfmiAlarmService.EquipmentType.Container;
            baseAsset = new TfmiServices.TfmiAlarmService.Equipment()
            {
                destination = new TfmiServices.TfmiAlarmService.EquipmentDestination(),
                equipmentType = value,
                origin = new TfmiServices.TfmiAlarmService.Place()
            };

            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value.ToString(), result.Truck);
        }

        [TestMethod()]
        public void ConvertAssetPartialTest()
        {
            var value = "P";

            ltl = true;

            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value, result.FullOrPartial);
        }

        [TestMethod()]
        public void ConvertAssetPhoneTest()
        {
            var value = "8550";

            postingCallback = new TfmiServices.TfmiAlarmService.PostingCallback()
            {
                Item = new TfmiServices.TfmiAlarmService.CallbackPhoneNumber() {
                    phone = new TfmiServices.TfmiAlarmService.PhoneNumber()
                    {
                        number = value
                    }
                }
            };

            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value, result.PhoneNumber);
        }

        [TestMethod()]
        public void ConvertAssetPhoneNullTest()
        {
            var value = "-";

            postingCallback = new TfmiServices.TfmiAlarmService.PostingCallback()
            {
                Item = new TfmiServices.TfmiAlarmService.CallbackPhoneNumber()
            };

            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value, result.PhoneNumber);
        }

        [TestMethod()]
        public void AvailabilityNullStatusTest()
        {
            var value = "-";

            fmeStatus = new TfmiServices.TfmiAlarmService.FmeStatus(){};

            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value, result.Avail);
        }

        [TestMethod()]
        public void AvailabilityFullDateStatusTest()
        {
            var value = DateTime.Now.AddDays(1);

            fmeStatus = new TfmiServices.TfmiAlarmService.FmeStatus()
            {
                endDate = value
            };

            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value.ToString("MM/dd"), result.Avail);
        }

        [TestMethod()]
        public void DimensionsTest()
        {
            var value = 50;

            dimensions = new TfmiServices.TfmiAlarmService.Dimensions()
            {
                lengthFeet = value,
                weightPounds = value
            };

            var result = assetDisplayHelper.ConvertAssetIntoDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value.ToString(), result.Length);
            Assert.AreEqual(value.ToString(), result.Weight);
        }
    }
}