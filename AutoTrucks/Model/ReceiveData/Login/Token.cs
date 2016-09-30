using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReceiveData.Login
{
    public class Token
    {
        public Int64 Primary { get; set; }
        public Int64 Secondary { get; set; }
        public DateTime Expiration { get; set; }
    }
}
