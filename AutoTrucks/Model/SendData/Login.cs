using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Model.SendData
{
    public class Login
    {
        // Length 4-16
        public string loginId { get; set; }

        // Length 4-30
        public string password { get; set; }

        // Length 0-32 
        public string thirdPartyId { get; set; }

        // Might be null
        public int apiVersion { get; set; }
    }
}
