using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.DataFromView;
using Model.Enums;
using Model.ReceiveData.AlarmMatch;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.DataConvertService.BaseAssetHelp;
using Service.JSONConverter;
using Service.ColorListHolder;

namespace Service.DataExtractService
{
    public class DataExtractService : IDataExtractService
    {
        private IAssetDisplayHelper assetDisplayHelper;

        private IColorListHolder colorListHolder;

        public DataExtractService(IAssetDisplayHelper assetDisplayHelper, IColorListHolder colorListHolder)
        {
            this.colorListHolder = colorListHolder;
            this.assetDisplayHelper = assetDisplayHelper;
        }

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
            data.backgroundColor = (System.Windows.Media.Color)colorListHolder.GetColorByReferenceId(item.postersReferenceId).Color;
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

        public DisplayFoundAsset ConvertContextToDisplayFoundAsset(Stream inputStream)
        {
            var value = new StreamReader(inputStream);
            string something = value.ReadToEnd();

            XNamespace ns = "http://www.tcore.com/TfmiAlarmMatch.xsd";
            XDocument doc = XDocument.Parse(something);

            List<string> removeWords = new List<string> { "tfm:", "tfm1:", "tcor:" };

            IEnumerable<XElement> responses = doc.Descendants(ns + "alarmMatchNotification");

            string json = JsonConvert.SerializeXNode(responses.FirstOrDefault());
            if (json == null || json.Equals("null"))
                return null;
            foreach (string badWord in removeWords)
            {
                json = json.Replace(badWord, string.Empty);
            }        
            JObject jObject = JObject.Parse(json);
            JToken jAlarmMatch = jObject["alarmMatchNotification"];
            JToken jMatch = jAlarmMatch["match"];
            JToken jAsset = jMatch["asset"];

            MyAsset myAsset = JsonConvert.DeserializeObject<MyAsset>(jAsset.ToString(), new AssetConverter());
            string basicAssetId = (string)jAlarmMatch["basisAssetId"];
            string alarmId = (string)jAlarmMatch["alarmId"];

            PostingCallback posting = JsonConvert.DeserializeObject<PostingCallback>(jMatch["callback"].ToString(), new CallbackConverter());

            return assetDisplayHelper.ConvertAssetToDisplayFoundAsset(myAsset.Item, myAsset.status, posting, myAsset.ltl, myAsset.dimensions, basicAssetId);
        }
    }
}
