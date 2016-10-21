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
    public class DataConvertService : IDataConvertService
    {

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
                {
                    dimensions.lengthFeet = parseResults;
                    dimensions.lengthFeetSpecified = true;
                }
            if (searchData.weight != null)
                if (Int32.TryParse(searchData.weight, out parseResults))
                {
                    dimensions.weightPounds = parseResults;
                    dimensions.weightPoundsSpecified = true;
                }

            return dimensions;
        }



        public SearchOperationParams ToSearchOperationParams(SearchDataFromView searchData, AssetType assetType)
        {           

            if (searchData != null && searchData.equipmentClasses != null)
            {

                GeoCriteria originGen = CheckIfOpen(searchData.originProvince);

                GeoCriteria destinationGen = CheckIfOpen(searchData.destinationProvince);

                bool openDestiantion = true;

                if (searchData.destinationProvince == StateProvince.Any)
                {
                    openDestiantion = false;
                }            

                var searchCriteria = new CreateSearchCriteria
                {                   
                    ageLimitMinutes = 60 * searchData.searchBack,
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
                    availability = ToAvailability(searchData),
                    excludeOpenDestinationEquipment = openDestiantion
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

        private GeoCriteria CheckIfOpen(StateProvince province)
        {
            if (province == StateProvince.Any)
            {
                return new GeoCriteria() { Item = new SearchOpen() };
            }
            else
            {
                return new GeoCriteria() { Item = new SearchArea { stateProvinces = new[] { province } } };
            }
        }

        private Availability ToAvailability(SearchDataFromView searchData)
        {
            return new Availability()
            {
                earliest = searchData.availFrom,
                latest = searchData.availTo
            };
        }

        public ObservableCollection<SearchAssetsReceived> ShipmentCreateSearchSuccessDataToSearchAssetsReceived(CreateSearchSuccessData searchSuccessData, DataColors dataColors)
        {
            ObservableCollection<SearchAssetsReceived> shipments = new ObservableCollection<SearchAssetsReceived>();

            if (searchSuccessData != null && searchSuccessData.matches != null)
            {
                foreach (MatchingAsset match in searchSuccessData.matches)
                {
                    if (match.asset != null)
                    {
                        Shipment truck = match.asset.Item as Shipment;
                        if (truck == null)
                            return null;
                        DateTime age = DateTime.Now;
                        if (match.asset.status != null && match.asset.status.created != null)
                            age = match.asset.status.created.date;

                        string initialO = null;
                        if (match.callback != null)
                            initialO = match.callback.postersStateProvince.ToString();

                        shipments.Add(new SearchAssetsReceived()
                        {
                            BackgroundColor = dataColors.BackgroundColor,
                            ForegroundColor = dataColors.ForegroundColor,
                            Truck = truck.equipmentType,
                            Origin = truck.origin,
                            Avail = match.asset.availability,
                            FP = match.asset.ltl,
                            DHD = match.destinationDeadhead,
                            DHO = match.originDeadhead,
                            Destination = truck.destination,
                            CompanyName = ShowCompanyName(match.callback),
                            ContactPhone = PhoneNumber(match.callback),
                            Length = SetDimensionLength(match.asset),
                            Weigth = SetDimensionWeigth(match.asset),
                            Age = age,
                            InitialO = initialO
                        });
                    }                   
                }
            }
            return shipments;
        }

        public ObservableCollection<SearchAssetsReceived> EquipmentCreateSearchSuccessDataToSearchAssetsReceived(CreateSearchSuccessData searchSuccessData, DataColors dataColors)
        {
            ObservableCollection<SearchAssetsReceived> searches = new ObservableCollection<SearchAssetsReceived>();


            if (searchSuccessData != null && searchSuccessData.matches != null)
            {
                foreach (MatchingAsset match in searchSuccessData.matches)
                {
                    if (match.asset != null)
                    {
                        Equipment equipment = match.asset.Item as Equipment;
                        if (equipment == null)
                            return null;
                        DateTime age = DateTime.Now;
                        if (match.asset.status != null && match.asset.status.created != null)
                            age = match.asset.status.created.date;

                        string initialO = null;
                        if (match.callback != null)
                            initialO = match.callback.postersStateProvince.ToString();

                        searches.Add(new SearchAssetsReceived()
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
                            CompanyName = ShowCompanyName(match.callback),
                            ContactPhone = PhoneNumber(match.callback), 
                            Length = SetDimensionLength(match.asset),
                            Weigth = SetDimensionWeigth(match.asset),
                            InitialO = initialO
                        });
                    }
                }
            }
            return searches;
        }

        private string ShowCompanyName(PostingCallback callback)
        {
            if (callback == null)
                return "-";
            else
                return callback.displayCompany;
        }

        private string SetDimensionWeigth(Asset asset)
        {
            if (asset.dimensions != null && asset.dimensions.weightPounds > 0)
                return asset.dimensions.weightPounds.ToString();
            return "-";
        }

        private string SetDimensionLength(Asset asset)
        {
            if (asset.dimensions != null && asset.dimensions.lengthFeet > 0)
                return asset.dimensions.lengthFeet.ToString();
            return "-";
        }

        private string PhoneNumber(PostingCallback item)
        {
            if (item == null)
                return "-";

            CallbackPhoneNumber phone = item.Item as CallbackPhoneNumber;
            if (phone != null)
                return phone.phone.number;
            return "-";
        }
    }
}
