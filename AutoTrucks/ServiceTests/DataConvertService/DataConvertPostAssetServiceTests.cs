using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DataFromView;
using Model.Enums;
using Service.DataConvertService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Service.DataConvertService.Tests
{
    [TestClass()]
    public class DataConvertPostAssetServiceTests
    {

        DataConvertPostAssetService dataConvertPostAssetService;

        [TestInitialize]
        public void SetInitialValues()
        {
            dataConvertPostAssetService = new DataConvertPostAssetService();
        }

        [TestMethod()]
        public void PostDataDimensionsTest()
        {
            var lengthValue = 200;
            var weightValue = 3000;
            Dimensions expectedResults = new Dimensions()
            {
                lengthFeet = lengthValue,
                weightPounds = weightValue
            };
            PostDataFromView initialValue = new PostDataFromView();
            initialValue.length = lengthValue;
            initialValue.weight = weightValue;
            var actualResults = dataConvertPostAssetService.PostDataFromViewEquipmentToBaseAsset(initialValue);
            Assert.AreEqual(expectedResults.lengthFeet, actualResults.dimensions.lengthFeet);
            Assert.AreEqual(expectedResults.weightPounds, actualResults.dimensions.weightPounds);
        }

        [TestMethod()]
        public void PostDataFullTest()
        {
            var value = FullOrPartial.Full;

            PostDataFromView initialValue = new PostDataFromView();
            initialValue.fullOrPartial = value;
            var actualResults = dataConvertPostAssetService.PostDataFromViewEquipmentToBaseAsset(initialValue);
            Assert.AreEqual(false, actualResults.ltl);
        }

        [TestMethod()]
        public void PostDataPartialTest()
        {
            var value = FullOrPartial.Partial;

            PostDataFromView initialValue = new PostDataFromView();
            initialValue.fullOrPartial = value;
            var actualResults = dataConvertPostAssetService.PostDataFromViewEquipmentToBaseAsset(initialValue);
            Assert.AreEqual(true, actualResults.ltl);
        }

        [TestMethod()]
        public void PostDataCommentsTest()
        {
            var value = new string[] { "Comment1" };
            PostDataFromView initialValue = new PostDataFromView();
            initialValue.commentOne = value[0];
            var actualResults = dataConvertPostAssetService.PostDataFromViewEquipmentToBaseAsset(initialValue);
            Assert.AreEqual(value[0], actualResults.comments[0]);
        }
        [TestMethod()]
        public void PostDataCommentsForShipmentTest()
        {
            var value = new string[] { "Comment1" };
            PostDataFromView initialValue = new PostDataFromView();
            initialValue.commentOne = value[0];
            var actualResults = dataConvertPostAssetService.PostDataFromViewShipmentToBaseAsset(initialValue);
            Assert.AreEqual(value[0], actualResults.comments[0]);
        }

        [TestMethod()]
        public void PostDataPlaceTest()
        {
            StateProvince state = default(StateProvince);
            string cityProvided = "City";

            var expectedResults = new Place()
            {
                Item = new CityAndState()
                {
                    stateProvince = state,
                    city = cityProvided
                }
            };
            PostDataFromView initialValue = new PostDataFromView();
            initialValue.originState = state;
            initialValue.cityOrigin = cityProvided;
            var resultsFromFunction = dataConvertPostAssetService.PostDataFromViewEquipmentToBaseAsset(initialValue);
            var resultsToCheck = resultsFromFunction.Item as Equipment;
            var js = new JavaScriptSerializer();
            Assert.AreEqual(js.Serialize(expectedResults), js.Serialize(resultsToCheck.origin));
        }
        
    }
}