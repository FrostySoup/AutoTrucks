

namespace Service.ConnexionService
{

    //Currently not in use
    public interface IConnectConnexionService
    {
        bool CheckIfValidLoginToConnexion(string username, string password);

        ISessionFacade LoginToConnexion(string user, string password);
        CreateSearchSuccessData SearchConnexion(ISessionFacade session, CreateSearchOperation searchDataProvided);
        LookupAssetSuccessData QueryAllMyAssets(ISessionFacade session);
        Data DeleteAssetsById(ISessionFacade session, string[] Ids);
        string PostNewAsset(ISessionFacade session, PostAssetOperation item);
    }
}
