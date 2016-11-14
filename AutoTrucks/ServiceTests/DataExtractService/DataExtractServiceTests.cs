using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Enums;
using Moq;
using Service.ColorListHolder;
using Service.DataConvertService.BaseAssetHelp;
using Service.DataExtractService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Service.DataExtractService.Tests
{
    [TestClass()]
    public class DataExtractServiceTests
    {
        DataExtractService dataExtractService;
        Mock<IAssetDisplayHelper> assetDisplayHelper;
        Mock<IColorListHolder> colorListHolder;

        [TestInitialize]
        public void SetInitialValues()
        {
            assetDisplayHelper = new Mock<IAssetDisplayHelper>();
            colorListHolder = new Mock<IColorListHolder>();
            colorListHolder.Setup(x => x.GetColorByReferenceId(It.IsAny<string>())).Returns(new Xceed.Wpf.Toolkit.ColorItem(Color.FromRgb(0, 0, 0), "Color"));
            dataExtractService = new DataExtractService(assetDisplayHelper.Object, colorListHolder.Object);
        }

        [TestMethod()]
        public void ExtractEquipmentFromDataTest()
        {
            LookupAssetSuccessData data = new LookupAssetSuccessData();
            var results = dataExtractService.ExtractEquipmentFromData(data, null);
            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void ExtractShipmentFromDataTest()
        {
            LookupAssetSuccessData data = new LookupAssetSuccessData();
            var results = dataExtractService.ExtractShipmentFromData(data, null);
            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void FullOrPartialFullTest()
        {
            LookupAssetSuccessData data = new LookupAssetSuccessData();
            data.assets = new Asset[] { new Asset()
            {
                Item = new Shipment(),
                ltl = false
            } };
            var results = dataExtractService.ExtractShipmentFromData(data, null);
            Assert.AreEqual(FullOrPartial.Full, results[0].fullOrPartial);
        }

        [TestMethod()]
        public void DimensionsTest()
        {
            int lengthValue = 20;
            int weightValue = 1500;
            Dimensions value = new Dimensions()
            {
                lengthFeet = lengthValue,
                weightPounds = weightValue
            };
            LookupAssetSuccessData data = new LookupAssetSuccessData();
            data.assets = new Asset[] { new Asset()
            {
                Item = new Shipment(),
                dimensions = value
            } };
            var results = dataExtractService.ExtractShipmentFromData(data, null);

            Assert.AreEqual(lengthValue, results[0].length);
            Assert.AreEqual(weightValue, results[0].weight);
        }

        [TestMethod()]
        public void AvailabilityTest()
        {
            DateTime value = DateTime.Now;
            Availability resultsExpected = new Availability()
            {
                earliest = value,
                latest = value
            };
            LookupAssetSuccessData data = new LookupAssetSuccessData();
            data.assets = new Asset[] { new Asset()
            {
                Item = new Shipment(),
                availability = resultsExpected
            } };            

            var results = dataExtractService.ExtractShipmentFromData(data, null);

            Assert.AreEqual(value, results[0].availFrom);
            Assert.AreEqual(value, results[0].availTo);
        }

        [TestMethod()]
        public void FullOrPartialPartialTest()
        {
            LookupAssetSuccessData data = new LookupAssetSuccessData();
            data.assets = new Asset[] { new Asset()
            {
                Item = new Shipment(),
                ltl = true
            } };
            var results = dataExtractService.ExtractShipmentFromData(data, null);
            Assert.AreEqual(FullOrPartial.Partial, results[0].fullOrPartial);
        }

        [TestMethod()]
        public void LocationOriginStateProvinceTest()
        {
            LookupAssetSuccessData data = new LookupAssetSuccessData();
            data.assets = new Asset[] { new Asset()
            {
                Item = new Shipment()
                {
                    origin = new Place()
                    {
                        Item = new NamedLatLon()
                        {
                            stateProvince = StateProvince.AG
                        }
                    }
                }
            } };
            var results = dataExtractService.ExtractShipmentFromData(data, null);
            Assert.AreEqual(StateProvince.AG, results[0].originState);
        }

        [TestMethod()]
        public void LocationDestinationStateProvinceTest()
        {
            LookupAssetSuccessData data = new LookupAssetSuccessData();
            data.assets = new Asset[] { new Asset()
            {
                Item = new Equipment()
                {
                    destination = new EquipmentDestination()
                    {
                        Item = new Place()
                        {
                            Item = new NamedLatLon()
                            {
                                stateProvince = StateProvince.AG
                            }
                        }
                    }
                }
            } };
            var results = dataExtractService.ExtractEquipmentFromData(data, null);
            Assert.AreEqual(StateProvince.AG, results[0].destinationState);
        }

    }
}