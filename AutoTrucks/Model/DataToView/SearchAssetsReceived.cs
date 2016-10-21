using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Model.ReceiveData.CreateSearch
{
    public class SearchAssetsReceived
    {
        public DateTime Age { get; set; }
        public Availability Avail { get; set; }
        public EquipmentType Truck { get; set; }

        public EquipmentClass[] Assets { get; set; }
        public bool FP { get; set; }
        public Place Origin { get; set; }
        public Mileage DHO { get; set; }
        public Place Destination { get; set; }
        public EquipmentDestination DestinationEquipment { get; set; }
        public Mileage DHD { get; set; }
        public Brush BackgroundColor { get; set; }
        public Brush ForegroundColor { get; set; }
        public string CompanyName { get; set; }
        public string ContactPhone { get; set; }
        public string Length { get; set; }
        public string Weigth { get; set; }
        public string InitialO { get; set; }


        public string AgeToString
        {
            get
            {
                if(Age == null)
                    return "-";
                int hours = (int)(DateTime.Now - Age).TotalHours;
                int minutes = (int)(DateTime.Now - Age).Minutes;
                return string.Format("{0}:{1:00}", hours, minutes);
            }
        }

        public string AssetsToString
        {
            get
            {
                if (Assets == null)
                    return "-";
                string formString = "";
                foreach (var asset in Assets)
                {
                    formString += asset.ToString();
                }
                return ((int)(DateTime.Now - Age).TotalHours).ToString() + "h";
            }
        }


        public string DHOToString
        {
            get
            {
                if (DHO != null)
                    return DHO.miles.ToString();
                else
                    return "-";
            }
        }
        public string DHDToString
        {
            get
            {
                if (DHD != null)
                    return DHD.miles.ToString();
                else
                    return "-";
            }
        }

        public string AvailabilityToString
        {
            get
            {
                if (Avail == null)
                    return "-";
                return Avail.latest.ToString("MM/dd");
            }
        }

        public string FullORPartial { get
            {
                if (FP == true)
                    return "P";
                return "F";
            }
        }

        public string OriginToString { get
            {
                if (Origin == null)
                    return "-";
                NamedLatLon cityAndState = Origin.Item as NamedLatLon;
                if (cityAndState == null)
                    return "-";
                return cityAndState.city + " " + cityAndState.stateProvince;
            }
        }

        public string DestinationToString
        {
            get
            {
                if (Destination != null)
                {
                    NamedLatLon cityAndState = Destination.Item as NamedLatLon;
                    if (cityAndState == null)
                        return "-";
                    return cityAndState.city + " " + cityAndState.stateProvince;
                }else if (DestinationEquipment != null)
                {
                    var destinationPlace = DestinationEquipment.Item as Place;
                    if (destinationPlace != null) {
                        var cityAndState = destinationPlace.Item as NamedLatLon;
                        if (cityAndState == null)
                            return "-";
                        return cityAndState.city + " " + cityAndState.stateProvince;
                    }else
                    {
                        var destinationArea = DestinationEquipment.Item as Area;
                        if (destinationArea != null)
                        {
                            return "-";
                        }
                    }
                }
                return "Anywhere";
            }
        }
    }
}
