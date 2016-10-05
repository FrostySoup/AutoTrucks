using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReceiveData.CreateSearch
{
    public class SearchCreated
    {
        public DateTime Age { get; set; }
        public Availability Avail { get; set; }
        public EquipmentType Truck { get; set; }
        public bool FP { get; set; }
        public Place Origin { get; set; }
        public Mileage DHO { get; set; }
        public Place Destination { get; set; }
        public Mileage DHD { get; set; }
        public string InitialO { get; set; }


        public string AgeToString
        {
            get
            {              
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
                NamedLatLon cityAndState = (NamedLatLon)Origin.Item;
                return cityAndState.city + " " + cityAndState.stateProvince;
            }
        }

        public string DestinationToString
        {
            get
            {
                NamedLatLon cityAndState = (NamedLatLon)Destination.Item;
                return cityAndState.city + " " + cityAndState.stateProvince;
            }
        }
    }
}
