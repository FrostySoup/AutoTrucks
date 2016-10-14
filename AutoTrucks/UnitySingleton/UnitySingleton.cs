using Microsoft.Practices.Unity;
using Service.AddNewWindowFactory;
using Service.ConnexionService;
using Service.DataConvertService;
using Service.SerializeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.MainWindowViewModels;
using ViewModels.PopUpWindowViewModels;

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
            unity.RegisterType<IWindowFactory, WindowFactory>();
            unity.RegisterType<ISerializeService, SerializeService>();
            unity.RegisterInstance<IDataConvertSingleton>(DataConvertSingleton.Instance);
            unity.RegisterType<ISessionCacheSingleton, SessionCacheSingleton>(new ContainerControlledLifetimeManager());
            unity.RegisterType<ILoginViewModel, LoginViewModel>();
            unity.RegisterType<IConnectConnexionService, ConnectConnexionService>();
            unity.RegisterType<IDataSourceViewModel, DataSourceViewModel>();
            unity.RegisterType<ISearchWindowViewModel, SearchWindowViewModel>();
            unity.RegisterType<IMainWindowDisplayViewModel, PostLoadsViewModel>("PostLoadsViewModel");
            unity.RegisterType<IMainWindowDisplayViewModel, PostTrucksViewModel>("PostTrucksViewModel");
            unity.RegisterType<IMainWindowDisplayViewModel, SearchLoadsViewModel>("SearchLoadsViewModel");
            unity.RegisterType<IMainWindowDisplayViewModel, SearchTrucksViewModel>("SearchTrucksViewModel");
            unity.RegisterType<ITopButtonsViewModel, TopButtonsViewModel>();

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
