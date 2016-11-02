using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataFromView;
using Model.Enums;

namespace Service.DataExtractService
{
    public class DataExtractService : IDataExtractService
    {
        public ObservableCollection<PostDataFromView> ExtractEquipmentFromData(LookupAssetSuccessData data, LookupAlarmSuccessData lookupAlarmSuccessData)
        {
            ObservableCollection<PostDataFromView> postDataFromViewCollection = new ObservableCollection<PostDataFromView>();

            PostDataFromView postDataFromView;
            if (data.assets != null)
            {
                foreach (var item in data.assets)
                {                   
                    Equipment equipment = item.Item as Equipment;
                    if (equipment != null)
                    {
                        postDataFromView = MapSameParams(item, equipment.origin, equipment.equipmentType);
                        postDataFromView.destinationState = GetLocationFromEquipmentDestination(equipment.destination);                       
                        postDataFromView.alarm = SetupAlarm(lookupAlarmSuccessData, item.assetId);
                        postDataFromViewCollection.Add(postDataFromView);
                    }
                }
            }
            return postDataFromViewCollection;
        }

        private Alarm SetupAlarm(LookupAlarmSuccessData lookupAlarmSuccessData, string id)
        {
            if (lookupAlarmSuccessData != null && lookupAlarmSuccessData.alarms != null)
                foreach (Alarm alarm in lookupAlarmSuccessData.alarms)
                {
                    if (alarm.basisAssetId.Equals(id))
                        return alarm;
                }
            return null;
        }

        public ObservableCollection<PostDataFromView> ExtractShipmentFromData(LookupAssetSuccessData data, LookupAlarmSuccessData lookupAlarmSuccessData)
        {
            ObservableCollection<PostDataFromView> postDataFromViewCollection = new ObservableCollection<PostDataFromView>();

            PostDataFromView postDataFromView;
            if (data.assets != null)
            {
                foreach (var item in data.assets)
                {
                    Shipment shipment = item.Item as Shipment;
                    if (shipment != null)
                    {
                        postDataFromView = MapSameParams(item, shipment.origin, shipment.equipmentType);
                        postDataFromView.destinationState = GetLocationFromPlace(shipment.destination);
                        postDataFromView.alarm = SetupAlarm(lookupAlarmSuccessData, item.assetId);
                        postDataFromViewCollection.Add(postDataFromView);
                    }
                }
            }
            return postDataFromViewCollection;
        }

        private PostDataFromView MapSameParams(Asset item, Place origin, EquipmentType equipmentType)
        {
            PostDataFromView data = new PostDataFromView();
            data = new PostDataFromView();
            if (item.availability != null)
            {
                data.availFrom = item.availability.earliest;
                data.availTo = item.availability.latest;
            }
            data.equipmentType = equipmentType;
            data.originState = GetLocationFromPlace(origin);           
            data.DHO = 150;
            data.DHD = 150;
            data.fullOrPartial = GetFullOrPartial(item.ltl);
            if (item.dimensions != null)
            {
                data.length = item.dimensions.lengthFeet;
                data.weight = item.dimensions.weightPounds;
            }
            data.TripMinValue = 0;
            data.TripMaxValue = 0;
            data.ID = item.assetId;
            return data;
        }

        private FullOrPartial GetFullOrPartial(bool ltl)
        {
            if (ltl)
                return FullOrPartial.Partial;
            else
                return FullOrPartial.Full;
        }

        private StateProvince GetLocationFromPlace(Place origin)
        {
            if (origin != null)
            {
                var namedLatLon = origin.Item as NamedLatLon;
                if (namedLatLon != null)
                    return namedLatLon.stateProvince;
            }
            return StateProvince.Any;
        }

        private StateProvince GetLocationFromEquipmentDestination(EquipmentDestination destination)
        {
            if (destination != null)
            {
                var destinationItem = destination.Item as Place;
                if (destinationItem != null)
                {
                    var namedLatLon = destinationItem.Item as NamedLatLon;
                    if (namedLatLon != null)
                        return namedLatLon.stateProvince;
                }else
                {
                    var destinationAreaItem = destination.Item as Area;
                    if (destinationAreaItem != null)
                    {
                        if (destinationAreaItem.stateProvinces.Length > 0)
                            return destinationAreaItem.stateProvinces[0];
                    }
                }
            }
            return StateProvince.Any;
        }
    }
}
