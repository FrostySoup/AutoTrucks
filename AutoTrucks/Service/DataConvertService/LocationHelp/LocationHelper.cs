﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfmiServices.TfmiAlarmService;

namespace Service.DataConvertService.LocationHelp
{
    public class LocationHelper : ILocationHelper
    {
        private string PlaceToString(Place place)
        {
            if (place == null)
                return "-";
            else
            {
                var namedCoordinates = place.Item as NamedLatLon;
                if (namedCoordinates != null)
                {
                    return string.Format("{0}, {1}", namedCoordinates.city, namedCoordinates.stateProvince);
                }
                var namesPostalCode = place.Item as NamedPostalCode;
                if (namesPostalCode != null)
                {
                    return string.Format("{0}, {1}", namesPostalCode.city, namesPostalCode.stateProvince);
                }
                var cityAndState = place.Item as CityAndState;
                if (cityAndState != null)
                {
                    return string.Format("{0}, {1}", cityAndState.city, cityAndState.stateProvince);
                }
                var latLon = place.Item as LatLon;
                if (latLon != null)
                {
                    return string.Format("{0}, {1}", latLon.latitude, latLon.longitude);
                }
                var postalCode = place.Item as FmPostalCode;
                if (postalCode != null)
                {
                    return string.Format("{0}, {1}", postalCode.code, postalCode.country);
                }              
            }

            return "-";
        }

        public string GeographicLocationToString(GeographicLocation geographicLocation)
        {
            if (geographicLocation == null)
                return "-";
            else
            {
                var place = geographicLocation as Place;
                if (place != null)
                    return PlaceToString(place);
                var open = geographicLocation as Open;
                if (open != null)
                    return "Anywhere";
                var area = geographicLocation as Area;
                if (area != null)
                    return AreaToString(area);
            }

            return "-";
        }

        private string AreaToString(Area area)
        {
            int counter = 0;
            string value = "";
            foreach (StateProvince province in area.stateProvinces)
            {
                value += province.ToString();
                if (counter < 5)
                {
                    counter++;
                }
                else
                    return value;
            }
            return value;
        }

        public string GeographicLocationToStringAlarmService(TfmiServices.TfmiAlarmService.GeographicLocation geographicLocation)
        {
            if (geographicLocation == null)
                return "-";
            else
            {
                var place = geographicLocation as TfmiServices.TfmiAlarmService.Place;
                if (place != null)
                    return PlaceToStringAlarmService(place);
                var open = geographicLocation as TfmiServices.TfmiAlarmService.Open;
                if (open != null)
                    return "Anywhere";
                var area = geographicLocation as TfmiServices.TfmiAlarmService.Area;
                if (area != null)
                    return AreaToStringAlarmService(area);
            }

            return "-";
        }

        private string AreaToStringAlarmService(TfmiServices.TfmiAlarmService.Area area)
        {
            int counter = 0;
            string value = "";
            foreach (TfmiServices.TfmiAlarmService.StateProvince province in area.stateProvinces)
            {
                value += province.ToString();
                if (counter < 5)
                {
                    counter++;
                }
                else
                    return value;
            }
            return value;
        }

        private string PlaceToStringAlarmService(TfmiServices.TfmiAlarmService.Place place)
        {
            if (place == null)
                return "-";
            else
            {
                var namedCoordinates = place.Item as TfmiServices.TfmiAlarmService.NamedLatLon;
                if (namedCoordinates != null)
                {
                    return string.Format("{0}, {1}", namedCoordinates.city, namedCoordinates.stateProvince);
                }
                var namesPostalCode = place.Item as TfmiServices.TfmiAlarmService.NamedPostalCode;
                if (namesPostalCode != null)
                {
                    return string.Format("{0}, {1}", namesPostalCode.city, namesPostalCode.stateProvince);
                }
                var cityAndState = place.Item as TfmiServices.TfmiAlarmService.CityAndState;
                if (cityAndState != null)
                {
                    return string.Format("{0}, {1}", cityAndState.city, cityAndState.stateProvince);
                }
                var latLon = place.Item as TfmiServices.TfmiAlarmService.LatLon;
                if (latLon != null)
                {
                    return string.Format("{0}, {1}", latLon.latitude, latLon.longitude);
                }
                var postalCode = place.Item as TfmiServices.TfmiAlarmService.FmPostalCode;
                if (postalCode != null)
                {
                    return string.Format("{0}, {1}", postalCode.code, postalCode.country);
                }
            }

            return "-";
        }
    }
}
