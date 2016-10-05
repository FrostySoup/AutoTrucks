using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReceiveData.CreateSearch
{
    public class SearchCreated
    {
        public int Age { get; set; }
        public Availability Avail { get; set; }
        public EquipmentType Truck { get; set; }
        public string FP { get; set; }
        public Place Origin { get; set; }
        public string DHO { get; set; }
        public Place Destination { get; set; }
        public string DHD { get; set; }
        public string InitialO { get; set; }


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
