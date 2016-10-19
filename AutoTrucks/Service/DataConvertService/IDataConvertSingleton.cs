using Model.DataFromView;
using Model.DataHelpers;
using Model.ReceiveData.CreateSearch;
using Model.SendData;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service.DataConvertService
{
    public interface IDataConvertSingleton
    {
        //DataConvertSingleton Instance { get; }

        SearchOperationParams ToSearchOperationParams(SearchDataFromView searchData, AssetType assetType);

        ObservableCollection<SearchCreated> ShipmentCreateSearchSuccessDataToSearchCreated(CreateSearchSuccessData searchSuccessData, DataColors dataColors);

        ObservableCollection<SearchCreated> EquipmentCreateSearchSuccessDataToSearchCreated(CreateSearchSuccessData searchSuccessData, DataColors dataColors);
    }
}