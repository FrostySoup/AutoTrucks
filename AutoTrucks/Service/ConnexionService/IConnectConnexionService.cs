using Model.ReceiveData.Login;
using Model.SearchCRUD;
using Model.SendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService
{

    //Currently not in use
    public interface IConnectConnexionService
    {
        bool CheckIfValidLoginToConnexion(string username, string password);

        SessionFacade LoginToConnexion(string user, string password);
        CreateSearchSuccessData SearchConnexion(SessionFacade session, SearchOperationParams searchDataProvided);
    }
}
