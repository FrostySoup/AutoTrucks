﻿using Microsoft.Practices.Unity;
using Service.AddNewWindowFactory;
using Service.ColorListHolder;
using Service.ConnexionService;
using Service.ConnexionService.AlarmService;
using Service.DataConvertService;
using Service.DataConvertService.BaseAssetHelp;
using Service.DataConvertService.LocationHelp;
using Service.DataExtractService;
using Service.FirewallController;
using Service.SerializeServices;
using Service.ViewModelsHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.MainWindowViewModels;
using ViewModels.PopUpWindowViewModels;
using ViewModels.PopUpWindowViewModels.BlacklistViewModel;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;
using ViewModels.PopUpWindowViewModels.RemoteConnectionViewModels;

namespace UnitySingleton
{
    public class UnitySingleton
    {
        private IUnityContainer unity { get; set; }

        public object ResolveMainWindow()
        {
            return unity.Resolve<MainWindowViewModel>();
        }

        private static UnitySingleton instance;

        private UnitySingleton()
        {
            unity = new UnityContainer();
            //MainWindowViewModels----------------------------------------------------------------------------------------
            unity.RegisterType<IMainWindowDisplayViewModel, PostLoadsViewModel>("PostLoadsViewModel");
            unity.RegisterType<IMainWindowDisplayViewModel, PostTrucksViewModel>("PostTrucksViewModel");
            unity.RegisterType<IMainWindowDisplayViewModel, SearchLoadsViewModel>("SearchLoadsViewModel");
            unity.RegisterType<IMainWindowDisplayViewModel, SearchTrucksViewModel>("SearchTrucksViewModel");
            unity.RegisterType<ITopButtonsViewModel, TopButtonsViewModel>();
            unity.RegisterType<IRemoteConnectionViewModel, RemoteConnectionViewModel>();
            //------------------------------------------------------------------------------------------------------------
            //PopUpWindowViewModels---------------------------------------------------------------------------------------
            unity.RegisterType<ISearchWindowViewModel, SearchWindowViewModel>();
            unity.RegisterType<IPostWindowViewModel, PostWindowViewModel>();
            unity.RegisterType<IBlacklistViewModel, BlacklistViewModel>();
            unity.RegisterType<IDataSourceViewModel, DataSourceViewModel>();
            unity.RegisterType<ILoginViewModel, LoginViewModel>();
            //------------------------------------------------------------------------------------------------------------
            //ServicesViewModels------------------------------------------------------------------------------------------
            unity.RegisterType<IDataExtractService, DataExtractService>();
            unity.RegisterType<IConnectConnexionService, ConnectConnexionService>();
            unity.RegisterType<IDataConvertPostAssetService, DataConvertPostAssetService>();
            unity.RegisterType<IWindowFactory, WindowFactory>();
            unity.RegisterType<ISerializeService, SerializeService>();
            unity.RegisterType<IDataConvertService, DataConvertService>();
            unity.RegisterType<IColorListHolder, ColorListHolder>(new ContainerControlledLifetimeManager());
            unity.RegisterType<ISessionCacheSingleton, SessionCacheSingleton>(new ContainerControlledLifetimeManager());
            unity.RegisterType<IAssetDisplayHelper, AssetDisplayHelper>();
            unity.RegisterType<ILocationHelper, LocationHelper>();
            unity.RegisterType<IFirewallControl, FirewallControl>();
            unity.RegisterType<IHttpService, HttpService>();
            unity.RegisterType<IAssetsViewModelHelper, AssetsViewModelHelper>();
            //------------------------------------------------------------------------------------------------------------

            unity.RegisterType<MainWindowViewModel>(new InjectionConstructor(
                new ResolvedParameter<ITopButtonsViewModel>(),
                new ResolvedParameter<IMainWindowDisplayViewModel>("PostLoadsViewModel"),
                new ResolvedParameter<IMainWindowDisplayViewModel>("SearchLoadsViewModel"),
                new ResolvedParameter<IMainWindowDisplayViewModel>("SearchTrucksViewModel"),
                new ResolvedParameter<IMainWindowDisplayViewModel>("PostTrucksViewModel")));
        }

        public static UnitySingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UnitySingleton();
                }
                return instance;
            }
        }
    }
}
