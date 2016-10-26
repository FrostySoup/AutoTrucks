using System;

namespace Service.ConnexionService
{
    public interface ISessionFacade
    {
       Uri EndpointUrl { get; }

       //void DeleteAllAssets();

      // void LookupDobCarriersByCarrierId(string carrierId, int indent = 0);

      // void LookupDobEvents(DateTime since);

      // void LookupSignedCarriers();

      // void LookupCarrierByDotNumber(int dotNumber);

      // void LookupCarrierByMcNumber(int mcNumber);

      // void LookupCarrierByUserId(int userId);
      // void Post(PostAssetRequest postAssetRequest);

       CreateSearchSuccessData Search(CreateSearchRequest searchRequest);

        LookupAssetSuccessData QueryAllMyAssets(LookupAssetRequest lookupRequest);
        Data DeleteAssetsById(DeleteAssetRequest deleteAssetRequest);
        string PostNewAsset(PostAssetRequest postAssetRequest);

        // void UpdateAlarm(string alarmUrl);



    }
}