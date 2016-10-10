using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataFromView;
using Model.SendData;
using Model.SearchCRUD;
using Model.ReceiveData.CreateSearch;
using System.Collections.ObjectModel;

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

        private GeoCriteria ToSearchRadius(StateProvince province, int dh, string cityProvided)
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
                            city = cityProvided
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
                destination = ToSearchRadius(searchData.destinationProvince, searchData.dhd, searchData.cityDestination),
                equipmentClasses = new[] { EquipmentClass.Flatbeds, EquipmentClass.Reefers },
                includeFulls = searchData.includeFulls,
                includeLtls = searchData.includeLtls,
                origin = ToSearchRadius(searchData.originProvince, searchData.dho, searchData.cityOrigin),
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

        public ObservableCollection<SearchCreated> CreateSearchSuccessDataToSearchCreated(CreateSearchSuccessData searchSuccessData)
        {
            ObservableCollection<SearchCreated> trucks = new ObservableCollection<SearchCreated>();
            if (searchSuccessData != null && searchSuccessData.matches != null)
            {                
                foreach (MatchingAsset match in searchSuccessData.matches)
                {
                    Shipment truck = (Shipment)match.asset.Item;
                    trucks.Add(new SearchCreated()
                    {
                        Destination = truck.destination,
                        Truck = truck.equipmentType,
                        Origin = truck.origin,
                        Avail = match.asset.availability,
                        FP = match.asset.ltl,
                        DHD = match.destinationDeadhead,
                        DHO = match.originDeadhead,
                        Age = match.asset.status.created.date,
                        InitialO = match.callback.postersStateProvince.ToString()
                    });
                }
            }

            return trucks;
        }
    }
}
