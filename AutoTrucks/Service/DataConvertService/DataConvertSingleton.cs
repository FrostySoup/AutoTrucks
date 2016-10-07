using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataFromView;
using Model.SendData;
using Model.SearchCRUD;

namespace Service.DataConvertService
{
    public class DataConvertSingleton
    {
        private static DataConvertSingleton instance;

        private DataConvertSingleton()
        { }



        public static DataConvertSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataConvertSingleton();
                }
                return instance;
            }
        }

        private GeoCriteria ToSearchRadius(StateProvince province, int dh)
        {
            return new GeoCriteria
            {
                Item = new SearchRadius
                {
                    place = new Place()
                    {
                        Item = new CityAndState()
                        {
                            stateProvince = province,
                            city = "mmm"
                        }
                    },
                    radius = new Mileage()
                    {
                        miles = dh,
                        method = MileageType.Air
                    }
                }
            };
        }

        private Dimensions createDimension(SearchDataFromView searchData)
        {
            Dimensions dimensions = new Dimensions();
            int parseResults = 0;

            if (searchData.length != null)
                if (Int32.TryParse(searchData.length, out parseResults))
                    dimensions.lengthFeet = parseResults;

            if (searchData.length != null)
                if (Int32.TryParse(searchData.weight, out parseResults))
                    dimensions.weightPounds = parseResults;

            return dimensions;
        }

        public SearchOperationParams ToSearchOperationParams(SearchDataFromView searchData, AssetType assetType)
        {
            var searchCriteria = new CreateSearchCriteria
            {
                ageLimitMinutes = 90,
                ageLimitMinutesSpecified = true,
                assetType = assetType,
                destination = ToSearchRadius(searchData.destinationProvince, searchData.dhd),
                equipmentClasses = new[] { EquipmentClass.Flatbeds, EquipmentClass.Reefers },
                includeFulls = searchData.includeFulls,
                includeLtls = searchData.includeLtls,
                origin = ToSearchRadius(searchData.originProvince, searchData.dho),
                limits = createDimension(searchData),
                availability = ToAvailability(searchData)
            };

            return new SearchOperationParams
            {
                criteria = searchCriteria,
                includeSearch = true,
                includeSearchSpecified = true,
                sortOrder = SortOrder.Closest,
                sortOrderSpecified = true
            };
        }

        private Availability ToAvailability(SearchDataFromView searchData)
        {
            return new Availability()
            {
                earliest = searchData.availFrom,
                latest = searchData.availTo
            };
        }
    }
}
