using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataFromView
{
    [Serializable]
    public class RemoteConnection
    {
        public string RemoteIp { get; set; }
        public string Port { get; set; }
    }
}
