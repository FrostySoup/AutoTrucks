using Model.ReceiveData.Login;
using Model.SearchCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService
{
    public interface IConnectConnexionService
    {
        bool CheckIfValidLoginToConnexion(string username, string password);

        SessionFacade LoginToConnexion(string user, string password);
        void SearchConnexion(SessionFacade session, CreateSearch searchDataProvided);
    }
}
