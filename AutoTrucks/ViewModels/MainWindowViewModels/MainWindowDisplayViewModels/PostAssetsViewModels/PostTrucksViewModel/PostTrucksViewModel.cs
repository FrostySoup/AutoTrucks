using Service.Commands;
using System.Windows.Input;
using ViewModels.MainWindowViewModels.MainWindowDisplayViewModels.PostAssetsViewModels;
using System;
using Service.AddNewWindowFactory;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;
using Model.DataFromView;
using System.Collections.ObjectModel;
using Service.ConnexionService;
using Service.DataExtractService;
using Service.DataConvertService;
using Service.ConnexionService.AlarmService;
using Service.SerializeServices;

namespace ViewModels.MainWindowViewModels
{
    public class PostTrucksViewModel : AssetsAbstractViewModel, IMainWindowDisplayViewModel
    {
        public PostTrucksViewModel(IWindowFactory windowFactory, IPostWindowViewModel postWindowViewModel, IConnectConnexionService connectConnexionService,
            ISessionCacheSingleton sessionCacheSingleton, IDataExtractService dataExtractService, IDataConvertPostAssetService dataConvertService, IHttpService httpService,
            ISerializeService serializeService) 
            : base(windowFactory, postWindowViewModel, connectConnexionService, sessionCacheSingleton, dataConvertService, httpService, serializeService)
        {
            this.dataExtractService = dataExtractService;
            this.OpenPostAssetWindowCommand = new DelegateCommand(o => this.OpenPostAssetWindow());
            GetExistingAssets();
        }

        protected override void convertData(LookupAssetSuccessData lookupAssetSuccessData, LookupAlarmSuccessData lookupAlarmSuccessData)
        {
            postAssets = dataExtractService.ExtractEquipmentFromData(lookupAssetSuccessData, lookupAlarmSuccessData);
        }

        protected override PostAssetOperation convertAssetIntoBaseType(PostDataFromView postData)
        {
            return dataConvertService.PostDataFromViewEquipmentToBaseAsset(postData);
        }
    }
}
