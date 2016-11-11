

using System.Collections.Generic;

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
        bool[] RetrieveUserCapabilities(ISessionFacade session, CapabilityType[] capabilities);
        Alarm CreateAlarm(ISessionFacade session, string assetID, AlarmSearchCriteria receivedCriteria);
        LookupAssetSuccessData QueryAllMyGroupAssets(ISessionFacade sessionFacade);
        LookupAlarmSuccessData QueryAllMyAlarms(ISessionFacade sessionFacade);
        LookupAlarmSuccessData QueryAllMyGroupAlarms(ISessionFacade sessionFacade);
        LookupAlarmUrlSuccessData LookupAlarmUrl(ISessionFacade sessionFacade);
        bool DeleteAlarms(List<string> ids, ISessionFacade session);

        LookupAlarmSuccessData QueryAllMyByIdAlarms(ISessionFacade session, string[] alarmsIds);
    }
}
