using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SendData.PostAsset.Place
{
    public class CityAndState
    {
        //Length: 0–30
        public string city { get; set; }

        public StateProvince stateProvince { get; set; }
    }
}
