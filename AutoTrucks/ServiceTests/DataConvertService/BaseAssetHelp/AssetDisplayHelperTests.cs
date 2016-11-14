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
        BaseAsset baseAsset;
        FmeStatus fmeStatus;
        PostingCallback postingCallback;
        bool ltl;
        Dimensions dimensions;
        string assetId;

        [TestInitialize]
        public void SetInitialValues()
        {
            baseAsset = new Shipment()
            {
                destination = new Place(),
                equipmentType = EquipmentType.Container,
                origin = new Place()
            };
            locationHelper = new Mock<ILocationHelper>();
            assetDisplayHelper = new AssetDisplayHelper(locationHelper.Object);
        }

        [TestMethod()]
        public void ConvertAssetAllNullsTest()
        {
            baseAsset = null;
            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);           
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ConvertAssetShipmentTest()
        {
            var value = EquipmentType.Container;
            baseAsset = new Shipment()
            {
                destination = new Place(),
                equipmentType = value,
                origin = new Place()
            };

            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value.ToString(), result.Truck);
        }

        [TestMethod()]
        public void ConvertAssetEquipmentTest()
        {
            var value = EquipmentType.Container;
            baseAsset = new Equipment()
            {
                destination = new EquipmentDestination(),
                equipmentType = value,
                origin = new Place()
            };

            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value.ToString(), result.Truck);
        }

        [TestMethod()]
        public void ConvertAssetPartialTest()
        {
            var value = "P";

            ltl = true;

            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value, result.FullOrPartial);
        }

        [TestMethod()]
        public void ConvertAssetPhoneTest()
        {
            var value = "8550";

            postingCallback = new PostingCallback()
            {
                Item = new CallbackPhoneNumber() {
                    phone = new PhoneNumber()
                    {
                        number = value
                    }
                }
            };

            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value, result.PhoneNumber);
        }

        [TestMethod()]
        public void ConvertAssetPhoneNullTest()
        {
            var value = "-";

            postingCallback = new PostingCallback()
            {
                Item = new CallbackPhoneNumber()
            };

            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value, result.PhoneNumber);
        }

        [TestMethod()]
        public void AvailabilityNullStatusTest()
        {
            var value = "-";

            fmeStatus = new FmeStatus(){};

            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value, result.Avail);
        }

        [TestMethod()]
        public void AvailabilityFullDateStatusTest()
        {
            var value = DateTime.Now.AddDays(1);

            fmeStatus = new FmeStatus()
            {
                endDate = value
            };

            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value.ToString("MM/dd"), result.Avail);
        }

        [TestMethod()]
        public void DimensionsTest()
        {
            var value = 50;

            dimensions = new Dimensions()
            {
                lengthFeet = value,
                weightPounds = value
            };

            var result = assetDisplayHelper.ConvertAssetToDisplayFoundAsset(baseAsset, fmeStatus, postingCallback, ltl, dimensions, assetId);
            Assert.AreEqual(value.ToString(), result.Length);
            Assert.AreEqual(value.ToString(), result.Weight);
        }
    }
}