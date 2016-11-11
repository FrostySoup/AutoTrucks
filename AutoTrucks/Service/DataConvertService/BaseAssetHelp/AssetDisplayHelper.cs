using Model.ReceiveData.AlarmMatch;
using Service.ColorListHolder;
using Service.DataConvertService.LocationHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataConvertService.BaseAssetHelp
{
    public class AssetDisplayHelper : IAssetDisplayHelper
    {
        private ILocationHelper locationHelper;

        public AssetDisplayHelper(ILocationHelper locationHelper)
        {
            this.locationHelper = locationHelper;
        }

        public DisplayFoundAsset ConvertAssetToDisplayFoundAsset(BaseAsset baseAsset, FmeStatus status, PostingCallback callback, bool ltl, Dimensions dimensions, string assetId)
        {
            DisplayFoundAsset displayFoundAsset = GetBaseAsset(baseAsset);
            if (displayFoundAsset == null)
                return null;
            displayFoundAsset.Avail = GetAvailFromStatus(status);
            if (callback != null)
            {
                displayFoundAsset.PhoneNumber = GetPhoneNumberFromCallBack(callback.Item);
                displayFoundAsset.CompanyName = callback.displayCompany;               
            }
            displayFoundAsset.FullOrPartial = GetFullOrPartial(ltl);
            if (dimensions != null) {
                displayFoundAsset.Length = dimensions.lengthFeet.ToString();
                displayFoundAsset.Weight = dimensions.weightPounds.ToString();
            }
            displayFoundAsset.AssetId = assetId;
            return displayFoundAsset;
        }

        private string GetFullOrPartial(bool ltl)
        {
            if (ltl)
            {
                return "P";
            }
            else
                return "F";
        }

        private string GetPhoneNumberFromCallBack(CallbackContact callback)
        {
            if (callback != null)
            {
                CallbackPhoneNumber phoneNumber = callback as CallbackPhoneNumber;
                if (phoneNumber != null && phoneNumber.phone != null)
                {
                    return phoneNumber.phone.extension + phoneNumber.phone.number;
                };
            }
            return "-";
        }

        private string GetAvailFromStatus(FmeStatus status)
        {
            if (status != null && status.endDate != null)
            {
                return status.endDate.ToString("MM/dd");
            }
            return "-";
        }

        private DisplayFoundAsset GetBaseAsset(BaseAsset baseAsset)
        {
            Shipment shipment = baseAsset as Shipment;
            if (shipment != null)
            {
                return new DisplayFoundAsset()
                {
                    Truck = shipment.equipmentType.ToString(),
                    Origin = locationHelper.GeographicLocationToString(shipment.origin),
                    Destination = locationHelper.GeographicLocationToString(shipment.destination)
                };
            }
            else
            {
                Equipment equipment = baseAsset as Equipment;
                if (equipment != null)
                {
                    return new DisplayFoundAsset()
                    {
                        Truck = equipment.equipmentType.ToString(),
                        Origin = locationHelper.GeographicLocationToString(equipment.origin),
                        Destination = locationHelper.GeographicLocationToString(equipment.destination.Item as GeographicLocation)
                    };
                }
            }

            return null;
        }
    }
}
