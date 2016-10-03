using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReceiveData.Login
{
    [Serializable]
    public class ReceivedLogin
    {
        public Token Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
