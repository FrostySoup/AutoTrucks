using Model.DataFromView;
using Model.DataHelpers;
using Model.ReceiveData.CreateSearch;
using Model.SendData;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service.DataConvertService
{
    public interface IDataConvertService
    {
        //DataConvertSingleton Instance { get; }

        SearchOperationParams ToSearchOperationParams(SearchDataFromView searchData, AssetType assetType);

        ObservableCollection<SearchAssetsReceived> ShipmentCreateSearchSuccessDataToSearchAssetsReceived(CreateSearchSuccessData searchSuccessData, DataColors dataColors);

        ObservableCollection<SearchAssetsReceived> EquipmentCreateSearchSuccessDataToSearchAssetsReceived(CreateSearchSuccessData searchSuccessData, DataColors dataColors);
    }
}