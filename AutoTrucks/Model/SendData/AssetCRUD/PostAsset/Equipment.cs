using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SendData.PostAsset
{
    public class Equipment
    {
        public EquipmentType equipmentType { get; set; }

        public Place origin { get; set; }

        public Place destination { get; set; }
    }
}
