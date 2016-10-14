using Model.DataFromView;
using Model.ReceiveData.CreateSearch;
using Model.SendData;
using System.Collections.ObjectModel;

namespace Service.DataConvertService
{
    public interface IDataConvertSingleton
    {
        //DataConvertSingleton Instance { get; }

        SearchOperationParams ToSearchOperationParams(SearchDataFromView searchData, AssetType assetType);

        ObservableCollection<SearchCreated> TrucksCreateSearchSuccessDataToSearchCreated(CreateSearchSuccessData searchSuccessData);

        ObservableCollection<SearchCreated> EquipmentCreateSearchSuccessDataToSearchCreated(CreateSearchSuccessData searchSuccessData);
    }
}