using Model.ReceiveData.AlarmMatch;
using Service.ColorListHolder;
using Service.DataConvertService.LocationHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfmiServices.TfmiAlarmService;

namespace Service.DataConvertService.BaseAssetHelp
{
    public class AssetDisplayHelper : IAssetDisplayHelper
    {
        private ILocationHelper locationHelper;

        public AssetDisplayHelper(ILocationHelper locationHelper)
        {
            this.locationHelper = locationHelper;
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

        private string GetPhoneNumberFromCallBack(TfmiServices.TfmiAlarmService.CallbackContact callback)
        {
            if (callback != null)
            {
                TfmiServices.TfmiAlarmService.CallbackPhoneNumber phoneNumber = callback as TfmiServices.TfmiAlarmService.CallbackPhoneNumber;
                if (phoneNumber != null && phoneNumber.phone != null)
                {
                    return phoneNumber.phone.extension + phoneNumber.phone.number;
                };
            }
            return "-";
        }

        private string GetAvailFromStatus(TfmiServices.TfmiAlarmService.FmeStatus status)
        {
            if (status != null && DateTime.Compare(status.endDate, DateTime.Now) >= 0)
            {
                return status.endDate.ToString("MM/dd");
            }
            return "-";
        }

        private DisplayFoundAsset GetBaseAsset(TfmiServices.TfmiAlarmService.BaseAsset baseAsset)
        {
            TfmiServices.TfmiAlarmService.Shipment shipment = baseAsset as TfmiServices.TfmiAlarmService.Shipment;
            if (shipment != null)
            {
                return new DisplayFoundAsset()
                {
                    Truck = shipment.equipmentType.ToString(),
                    Origin = locationHelper.GeographicLocationToStringAlarmService(shipment.origin),
                    Destination = locationHelper.GeographicLocationToStringAlarmService(shipment.destination)
                };
            }
            else
            {
                TfmiServices.TfmiAlarmService.Equipment equipment = baseAsset as TfmiServices.TfmiAlarmService.Equipment;
                if (equipment != null)
                {
                    return new DisplayFoundAsset()
                    {
                        Truck = equipment.equipmentType.ToString(),
                        Origin = locationHelper.GeographicLocationToStringAlarmService(equipment.origin),
                        Destination = locationHelper.GeographicLocationToStringAlarmService(equipment.destination.Item as TfmiServices.TfmiAlarmService.GeographicLocation)
                    };
                }
            }

            return null;
        }

        public DisplayFoundAsset ConvertAssetIntoDisplayFoundAsset(TfmiServices.TfmiAlarmService.BaseAsset baseAsset, TfmiServices.TfmiAlarmService.FmeStatus status, TfmiServices.TfmiAlarmService.PostingCallback callback, bool ltl, TfmiServices.TfmiAlarmService.Dimensions dimensions, string basisAssetId)
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
            if (dimensions != null)
            {
                displayFoundAsset.Length = dimensions.lengthFeet.ToString();
                displayFoundAsset.Weight = dimensions.weightPounds.ToString();
            }
            displayFoundAsset.AssetId = basisAssetId;
            return displayFoundAsset;
        }
    }
}
