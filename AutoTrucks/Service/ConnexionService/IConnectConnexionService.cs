using Model.ReceiveData.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService
{
    public interface IConnectConnexionService
    {
        ReceivedLogin LoginToConnexion(string username, string password);
    }
}
