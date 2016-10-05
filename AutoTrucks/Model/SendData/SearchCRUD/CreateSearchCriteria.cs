using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SearchCRUD
{
    [Serializable]
    public class CreateSearchCriteria
    {
        public AssetType assetType { get; set; }

        public EquipmentClass[] equipmentClasses { get; set; }

        public int ageLimitMinutes { get; set; }

        public bool ageLimitMinutesSpecified { get; set; }

        public GeoCriteria origin { get; set; }

        public GeoCriteria destination { get; set; }

        public Availability availability { get; set; }

        public bool includeLtls { get; set; }

        public bool includeFulls { get; set; }

        public Dimensions limits { get; set; }

        public bool excludeOpenDestinationEquipment { get; set; }

    }
}
