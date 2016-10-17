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
using Model.DataHelpers;

namespace Service.DataConvertService
{
    public class DataConvertSingleton : IDataConvertSingleton
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
        /*
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
        }*/

        private Dimensions createDimension(SearchDataFromView searchData)
        {
            Dimensions dimensions = new Dimensions()
            {
                heightInches = -1,
                lengthFeet = -1,
                volumeCubicFeet = -1,
                weightPounds = -1
            };
            int parseResults = 0;

            if (searchData.length != null)
                if (Int32.TryParse(searchData.length, out parseResults))
                    dimensions.lengthFeet = parseResults;
            if (searchData.weight != null)
                if (Int32.TryParse(searchData.weight, out parseResults))
                    dimensions.weightPounds = parseResults;

            return dimensions;
        }

        public SearchOperationParams ToSearchOperationParams(SearchDataFromView searchData, AssetType assetType)
        {           

            if (searchData != null && searchData.equipmentClasses != null)
            {
                var originGen = new GeoCriteria() { Item = new SearchArea { stateProvinces = new[] { searchData.originProvince } } };

                var destinationGen = new GeoCriteria() { Item = new SearchArea { stateProvinces = new[] { searchData.destinationProvince } } };

                var searchCriteria = new CreateSearchCriteria
                {
                    ageLimitMinutes = 90,
                    ageLimitMinutesSpecified = true,
                    assetType = assetType,
                    destination = destinationGen,
                    equipmentClasses = searchData.equipmentClasses.ToArray(),
                    //equipmentTypes = searchData.equipmentType.ToArray(),
                    includeFulls = searchData.includeFulls,
                    includeLtls = searchData.includeLtls,
                    origin = originGen,
                    //origin = ToSearchRadius(searchData.originProvince, searchData.dho, searchData.cityOrigin),
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
            return null;
        }

        private Availability ToAvailability(SearchDataFromView searchData)
        {
            return new Availability()
            {
                earliest = searchData.availFrom,
                latest = searchData.availTo
            };
        }

        public ObservableCollection<SearchCreated> TrucksCreateSearchSuccessDataToSearchCreated(CreateSearchSuccessData searchSuccessData, DataColors dataColors)
        {
            ObservableCollection<SearchCreated> trucks = new ObservableCollection<SearchCreated>();

            if (searchSuccessData != null && searchSuccessData.matches != null)
            {
                foreach (MatchingAsset match in searchSuccessData.matches)
                {
                    if (match.asset != null)
                    {
                        Shipment truck = (Shipment)match.asset.Item;

                        DateTime age = DateTime.Now;
                        if (match.asset.status != null && match.asset.status.created != null)
                            age = match.asset.status.created.date;

                        string initialO = null;
                        if (match.callback != null)
                            initialO = match.callback.postersStateProvince.ToString();

                        trucks.Add(new SearchCreated()
                        {
                            BackgroundColor = dataColors.BackgroundColor,
                            ForegroundColor = dataColors.ForegroundColor,
                            Destination = truck.destination,
                            Truck = truck.equipmentType,
                            Origin = truck.origin,
                            Avail = match.asset.availability,
                            FP = match.asset.ltl,
                            DHD = match.destinationDeadhead,
                            DHO = match.originDeadhead,
                            Age = age,
                            InitialO = initialO
                        });
                    }
                }
            }
            return trucks;
        }

        public ObservableCollection<SearchCreated> EquipmentCreateSearchSuccessDataToSearchCreated(CreateSearchSuccessData searchSuccessData, DataColors dataColors)
        {
            ObservableCollection<SearchCreated> searches = new ObservableCollection<SearchCreated>();


            if (searchSuccessData != null && searchSuccessData.matches != null)
            {
                foreach (MatchingAsset match in searchSuccessData.matches)
                {
                    if (match.asset != null)
                    {
                        Equipment equipment = (Equipment)match.asset.Item;

                        DateTime age = DateTime.Now;
                        if (match.asset.status != null && match.asset.status.created != null)
                            age = match.asset.status.created.date;

                        string initialO = null;
                        if (match.callback != null)
                            initialO = match.callback.postersStateProvince.ToString();

                        searches.Add(new SearchCreated()
                        {
                            BackgroundColor = dataColors.BackgroundColor,
                            ForegroundColor = dataColors.ForegroundColor,
                            DestinationEquipment = equipment.destination,
                            Truck = equipment.equipmentType,
                            Origin = equipment.origin,
                            Avail = match.asset.availability,
                            FP = match.asset.ltl,
                            DHD = match.destinationDeadhead,
                            DHO = match.originDeadhead,
                            Age = age,
                            InitialO = initialO
                        });
                    }
                }
            }
            return searches;
        }
    }
}
