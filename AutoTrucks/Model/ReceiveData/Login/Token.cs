using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReceiveData.Login
{
    [Serializable]
    public class Token
    {
        public Byte[] Primary { get; set; }
        public Byte[] Secondary { get; set; }
        public DateTime Expiration { get; set; }
    }
}
