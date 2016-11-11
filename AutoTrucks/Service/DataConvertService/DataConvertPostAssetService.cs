using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataFromView;
using Model.Enums;
using Model.ReceiveData.AlarmMatch;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Service.DataConvertService.LocationHelp;
using Service.ColorListHolder;

namespace Service.DataConvertService
{
    public class DataConvertPostAssetService : IDataConvertPostAssetService
    {
        private ILocationHelper locationHelper;
        private IColorListHolder colorListHolder;

        public DataConvertPostAssetService(ILocationHelper locationHelper, IColorListHolder colorListHolder)
        {
            this.locationHelper = locationHelper;
            this.colorListHolder = colorListHolder;
        }

        public PostAssetOperation PostDataFromViewEquipmentToBaseAsset(PostDataFromView postData)
        {
            PostAssetOperation postAssetOperation = MapPostAssetOperation(postData);            
            Equipment equipment = new Equipment();       
            equipment.destination = CreateEquipmentDestination(postData.destinationState, postData.cityDestination);
            equipment.origin = CreatePlace(postData.originState, postData.cityOrigin);
            equipment.equipmentType = postData.equipmentType;
            postAssetOperation.Item = equipment;
            postAssetOperation.postersReferenceId = colorListHolder.SetReferenceByColor(postData.backgroundColor);
            return postAssetOperation;
        }      

        public PostAssetOperation PostDataFromViewShipmentToBaseAsset(PostDataFromView postData)
        {
            PostAssetOperation postAssetOperation = MapPostAssetOperation(postData);
            Shipment shipment = new Shipment();
            shipment.destination = CreatePlace(postData.destinationState, postData.cityDestination);
            shipment.origin = CreatePlace(postData.originState, postData.cityOrigin);
            shipment.equipmentType = postData.equipmentType;
            postAssetOperation.Item = shipment;
            postAssetOperation.postersReferenceId = colorListHolder.SetReferenceByColor(postData.backgroundColor);
            return postAssetOperation;
        }

        private PostAssetOperation MapPostAssetOperation(PostDataFromView postData)
        {
            PostAssetOperation postAssetOperation = new PostAssetOperation();
            postAssetOperation.availability = GetAvailability(postData.availFrom, postData.availTo);
            postAssetOperation.comments = GetComments(postData.commentOne);
            postAssetOperation.dimensions = GetDimensions(postData.length, postData.weight);
            postAssetOperation.ltl = GetLtl(postData.fullOrPartial);
            return postAssetOperation;
        }

        private bool GetLtl(FullOrPartial fullOrPartial)
        {
            if (fullOrPartial == FullOrPartial.Partial)
                return true;
            else
                return false;
        }

        private Dimensions GetDimensions(int length, int weight)
        {
            Dimensions dimension = new Dimensions();
            dimension.lengthFeet = length;
            dimension.weightPounds = weight;
            return dimension;
        }

        private string[] GetComments(string commentOne)
        {
            if (commentOne != null)
                return new string[] { commentOne };
            return null;
        }

        private Availability GetAvailability(DateTime availFrom, DateTime availTo)
        {
            if (availFrom == null && availTo == null)
                return null;
            return new Availability()
            {
                earliest = availFrom,
                earliestSpecified = true,
                latest = availTo,
                latestSpecified = true
            };
        }

        private EquipmentDestination CreateEquipmentDestination(StateProvince destinationState, string cityDestination)
        {
            if (destinationState == StateProvince.Any)
                return new EquipmentDestination()
                {
                    Item = new Open()
                };
            else if (string.IsNullOrEmpty(cityDestination))
                return new EquipmentDestination()
                {
                    Item = new Area()
                    {
                        stateProvinces = new StateProvince[] { destinationState }
                    }
                };
            else return new EquipmentDestination()
            {
                Item = new Place()
                {
                    Item = new CityAndState()
                    {
                        stateProvince = destinationState,
                        city = cityDestination
                    }
                }
            };

        }

        private Place CreatePlace(StateProvince state, string cityProvided)
        {
            return new Place()
            {
                Item = new CityAndState()
                {
                    stateProvince = state,
                    city = cityProvided
                }
            };
        }
    }
}
