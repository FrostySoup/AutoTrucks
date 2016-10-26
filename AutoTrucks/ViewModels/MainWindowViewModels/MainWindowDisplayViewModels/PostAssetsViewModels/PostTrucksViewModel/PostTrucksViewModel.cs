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

namespace ViewModels.MainWindowViewModels
{
    public class PostTrucksViewModel : AssetsAbstractViewModel, IMainWindowDisplayViewModel
    {
        public PostTrucksViewModel(IWindowFactory windowFactory, IPostWindowViewModel postWindowViewModel, IConnectConnexionService connectConnexionService,
            ISessionCacheSingleton sessionCacheSingleton, IDataExtractService dataExtractService, IDataConvertPostAssetService dataConvertService) 
            : base(windowFactory, postWindowViewModel, connectConnexionService, sessionCacheSingleton, dataConvertService)
        {
            this.dataExtractService = dataExtractService;
            this.OpenPostAssetWindowCommand = new DelegateCommand(o => this.OpenPostAssetWindow());
            this.PostTruckCommand = new DelegateCommand(o => this.PostTruck());
            GetExistingAssets();
        }

        private void PostTruck()
        {
            throw new NotImplementedException();
        }

        protected override void convertData(LookupAssetSuccessData lookupAssetSuccessData)
        {
            postAssets = dataExtractService.ExtractEquipmentFromData(lookupAssetSuccessData);
        }

        protected override PostAssetOperation convertAssetIntoBaseType(PostDataFromView postData)
        {
            return dataConvertService.PostDataFromViewEquipmentToBaseAsset(postData);
        }
    }
}
