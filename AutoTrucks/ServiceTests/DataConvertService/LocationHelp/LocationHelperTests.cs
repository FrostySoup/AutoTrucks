using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.DataConvertService.LocationHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataConvertService.LocationHelp.Tests
{
    [TestClass()]
    public class LocationHelperTests
    {
        LocationHelper locationHelper;

        [TestInitialize]
        public void SetInitialValues()
        {
            locationHelper = new LocationHelper();
        }

        [TestMethod()]
        public void GeoLocationNullTest()
        {
            var result = locationHelper.GeographicLocationToString(null);
            Assert.AreEqual("-", result);
        }

        [TestMethod()]
        public void GeoLocationPlaceNamedLatLonTest()
        {
            var city = "Kaunas";
            var state = StateProvince.AL;
            var value = new Place()
            {
                Item = new NamedLatLon()
                {
                    city = city,
                    stateProvince = state
                }
            };
            var result = locationHelper.GeographicLocationToString(value);
            Assert.AreEqual(string.Format("{0}, {1}", city, state), result);
        }

        [TestMethod()]
        public void GeoLocationPlaceNamedPostalCodeTest()
        {
            var city = "Kaunas";
            var state = StateProvince.AL;
            var value = new Place()
            {
                Item = new NamedPostalCode()
                {
                    city = city,
                    stateProvince = state
                }
            };
            var result = locationHelper.GeographicLocationToString(value);
            Assert.AreEqual(string.Format("{0}, {1}", city, state), result);
        }

        [TestMethod()]
        public void GeoLocationPlaceCityAndStateTest()
        {
            var city = "Kaunas";
            var state = StateProvince.AL;
            var value = new Place()
            {
                Item = new CityAndState()
                {
                    city = city,
                    stateProvince = state
                }
            };
            var result = locationHelper.GeographicLocationToString(value);
            Assert.AreEqual(string.Format("{0}, {1}", city, state), result);
        }

        [TestMethod()]
        public void GeoLocationOpenTest()
        {
            var value = new Open() {};
            var result = locationHelper.GeographicLocationToString(value);
            Assert.AreEqual("Anywhere", result);
        }

        [TestMethod()]
        public void GeoLocationAreaTest()
        {
            StateProvince[] results = new StateProvince[] { StateProvince.CA, StateProvince.CL };
            var value = new Area() {
                stateProvinces = new StateProvince[] { results[0], results[1] }
            };
            var result = locationHelper.GeographicLocationToString(value);
            Assert.AreEqual("CACL", result);
        }

        [TestMethod()]
        public void GeoLocationPlaceLatLonTest()
        {
            var value = new Place()
            {
                Item = new LatLon()
                {
                    latitude = 10,
                    longitude = 10
                }
            };
            var result = locationHelper.GeographicLocationToString(value);
            Assert.AreEqual(string.Format("{0}, {1}", 10, 10), result);
        }

        [TestMethod()]
        public void GeoLocationPlaceFmPostalCodeTest()
        {
            var value = new Place()
            {
                Item = new FmPostalCode()
                {
                    code = "10",
                    country = CountryCode.US
                }
            };
            var result = locationHelper.GeographicLocationToString(value);
            Assert.AreEqual(string.Format("{0}, {1}", "10", CountryCode.US), result);
        }

    }
}