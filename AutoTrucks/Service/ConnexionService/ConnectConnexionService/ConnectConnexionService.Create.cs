using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService
{
    public partial class ConnectConnexionServices
    {
        public string PostNewAsset(ISessionFacade session, PostAssetOperation item)
        {
            PostAssetRequest postAssetRequest = new PostAssetRequest()
            {
                postAssetOperations = new PostAssetOperation[] { item }
            };
            return session.PostNewAsset(postAssetRequest);
        }
    }
}
