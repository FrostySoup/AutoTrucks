using Model.ReceiveData.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class DataSource
    {
        public bool Selected { get; set; }
        public string Source { get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }
        public ReceivedLogin LoginData { get; set; }
    }
}
