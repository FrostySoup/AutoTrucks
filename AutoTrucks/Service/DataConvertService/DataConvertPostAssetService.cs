using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataFromView;
using Model.Enums;

namespace Service.DataConvertService
{
    public class DataConvertPostAssetService : IDataConvertPostAssetService
    {
        public PostAssetOperation PostDataFromViewEquipmentToBaseAsset(PostDataFromView postData)
        {
            PostAssetOperation postAssetOperation = MapPostAssetOperation(postData);
            Equipment equipment = new Equipment();
            equipment.destination = new EquipmentDestination()
            {
                Item = CreatePlace(postData.destinationState, postData.cityDestination)
            };
            equipment.origin = CreatePlace(postData.originState, postData.cityOrigin);
            equipment.equipmentType = postData.equipmentType;
            postAssetOperation.Item = equipment;
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

            //new GeoCriteria() { Item = new SearchOpen() };
        }
    }
}
