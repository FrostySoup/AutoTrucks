using Model.ReceiveData.AlarmMatch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.JSONConverter
{
    public class AssetConverter : JsonCreationConverter<MyAsset>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected override MyAsset Create(Type objectType, JObject jObject)
        {
            MyAsset asset = jObject.ToObject<MyAsset>();
            if (FieldExists("shipment", jObject))
                asset.Item = GetAsset(jObject);
            else
            {
                asset.Item = GetAsset(jObject);
            }
            return asset;         
        }

        private BaseAsset GetAsset(JObject jObject)
        {
            if (FieldExists("shipment", jObject))
            {
                JToken jShipment = jObject["shipment"];
                Shipment shipment = new Shipment();
                EquipmentType equipment;
                Enum.TryParse((string)jShipment["equipmentType"], out equipment);
                shipment.equipmentType = equipment;
                shipment.origin = new Place()
                {
                    Item = GetPlace(JObject.Parse(jShipment["origin"].ToString()))
                };
                shipment.destination = new Place()
                {
                    Item = GetPlace(JObject.Parse(jShipment["destination"].ToString()))
                };
                return shipment;
            }
            else
            {
                return new Equipment();
            }
        }

        private PlaceBase GetPlace(JObject jObject)
        {
            if (FieldExists("namedCoordinates", jObject))
            {
                return jObject["namedCoordinates"].ToObject<NamedLatLon>();
            }
            else if (FieldExists("namedPostalCode", jObject))
            {
                return jObject["namedPostalCode"].ToObject<NamedPostalCode>();
            }
            else if (FieldExists("postalCode", jObject))
            {
                return jObject["postalCode"].ToObject<FmPostalCode>();
            }
            else if (FieldExists("cityAndState", jObject))
            {
                return jObject["cityAndState"].ToObject<CityAndState>();
            }
            else
            {
                return jObject["latLon"].ToObject<LatLon>();
            }
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }

    public class CallbackConverter : JsonCreationConverter<PostingCallback>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected override PostingCallback Create(Type objectType, JObject jObject)
        {
            PostingCallback asset = jObject.ToObject<PostingCallback>();

            if (FieldExists("phone", jObject))
            {
                JToken jPhone = jObject["phone"];
                jPhone = jPhone["phone"];
                asset.Item = new CallbackPhoneNumber()
                {
                    phone = jPhone.ToObject<PhoneNumber>()
                };
            }
            else
            {
                JToken jEmail = jObject["email"];
                jEmail = jObject["email"];
                asset.Item = new CallbackEmailAddress()
                {
                    email = (string)jEmail["email"]
                };
            }
            return asset;
        }
        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }

    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
