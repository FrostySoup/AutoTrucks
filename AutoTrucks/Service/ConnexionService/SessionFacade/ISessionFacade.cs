
using System;

namespace Service.ConnexionService
{
    public interface ISessionFacade
    {
       Uri EndpointUrl { get; }

       CreateSearchSuccessData Search(CreateSearchRequest searchRequest);

       LookupAssetSuccessData QueryAllMyAssets(LookupAssetRequest lookupRequest);
       Data DeleteAssetsById(DeleteAssetRequest deleteAssetRequest);
       void UpdateAlarmUrl(Uri alarmUrl);
       string PostNewAsset(PostAssetRequest postAssetRequest);
       bool[] CheckUserCapabilities(LookupCapabilitiesRequest lookupCapabilietiesRequest);
       Alarm CreateNewAlert(CreateAlarmRequest createAlarmRequest);
       LookupAlarmSuccessData QueryAllAlarms(LookupAlarmRequest createAlarmRequest);
       LookupAlarmUrlSuccessData LookupCurrentAlarmUrl(LookupAlarmUrlRequest lookupAlarmUrlRequest);
       bool DeleteAlarms(DeleteAlarmRequest deleteAlarmRequest);

    }
}