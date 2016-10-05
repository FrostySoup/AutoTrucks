using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SearchCRUD
{
    public class CreateSearch
    {
        public AssetType criteria { get; set; }

        public EquipmentType[] equipmentClasses { get; set; }

        public int ageLimitMinutes { get; set; }

        public bool ageLimitMinutesFieldSpecified { get; set; }

        public StateProvince origin { get; set; }

        public StateProvince destination { get; set; }

        //private Availability availabilityField;

        public bool includeLtls { get; set; }

        public bool includeFulls { get; set; }

        //private Dimensions limitsField;

        public bool excludeOpenDestinationEquipment { get; set; }

    }
}
