using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataFromView;
using Model.ReceiveData.AlarmMatch;
using Service.ConnexionService;
using Service.ConnexionService.AlarmService;

namespace Service.ViewModelsHelpers
{
    public interface IAssetsViewModelHelper
    {
        Alarm AddAlarmForAsset(ISessionFacade sessionFacade, PostDataFromView asset, IConnectConnexionService connectConnexionService);
        ObservableCollection<PostDataFromView> RemoveSelectedAssets(ISessionFacade sessionFacade, ObservableCollection<PostDataFromView> postAssets, IConnectConnexionService connectConnexionService);
        ObservableCollection<DisplayFoundAsset> GetAssetsFromHttpService(IHttpService httpService, ObservableCollection<PostDataFromView> postAssets);
        void SerializeCompanyName(string companyName);
    }
}
