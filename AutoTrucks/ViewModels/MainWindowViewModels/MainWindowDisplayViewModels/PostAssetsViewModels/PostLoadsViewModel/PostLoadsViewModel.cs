﻿using Model.DataFromView;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.ConnexionService.AlarmService;
using Service.DataConvertService;
using Service.DataExtractService;
using Service.SerializeServices;
using Service.ViewModelsHelpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ViewModels.MainWindowViewModels.MainWindowDisplayViewModels.PostAssetsViewModels;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;

namespace ViewModels.MainWindowViewModels
{
    public class PostLoadsViewModel : AssetsAbstractViewModel, IMainWindowDisplayViewModel
    {
        public PostLoadsViewModel(IWindowFactory windowFactory, IPostWindowViewModel postWindowViewModel, IConnectConnexionService connectConnexionService, 
            ISessionCacheSingleton sessionCacheSingleton, IDataExtractService dataExtractService, IDataConvertPostAssetService dataConvertService,
            IHttpService httpService, IAssetsViewModelHelper assetsViewModelHelper)
            : base(windowFactory, postWindowViewModel, connectConnexionService, sessionCacheSingleton, dataConvertService, httpService, assetsViewModelHelper)
        {
            this.dataExtractService = dataExtractService;
            this.OpenPostAssetWindowCommand = new DelegateCommand(o => this.OpenPostAssetWindow());
            GetExistingAssets();
        }

        protected override void convertData(LookupAssetSuccessData lookupAssetSuccessData, LookupAlarmSuccessData lookupAlarmSuccessData)
        {
            postAssets = dataExtractService.ExtractShipmentFromData(lookupAssetSuccessData, lookupAlarmSuccessData);
        }

        protected override PostAssetOperation convertAssetIntoBaseType(PostDataFromView postData)
        {
            return dataConvertService.PostDataFromViewShipmentToBaseAsset(postData);
        }

    }
}
