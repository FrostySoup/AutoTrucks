using Microsoft.Practices.Unity;
using Service.AddNewWindowFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.MainWindowViewModels;
using ViewModels.MainWindowViewModels.MainWindowViewModelsInterfaces;

namespace UnitySingleton
{
    public class UnitySingleton
    {
        private IUnityContainer unity { get; set; }

        public object ResolveMainWindow()
        {
            return unity.Resolve<MainWindowViewModel>("Load");
        }

        private static UnitySingleton instance;

        private UnitySingleton()
        {
            unity = new UnityContainer();
            unity.RegisterType<MainWindowViewModel>();
            unity.RegisterType<IWindowFactory, WindowFactory>();
            unity.RegisterType<ILoginViewModel, LoginViewModel>();
            unity.RegisterType<IPostLoadsViewModel, PostLoadsViewModel>();
            unity.RegisterType<IPostTrucksViewModel, PostTrucksViewModel>();
            unity.RegisterType<ISearchLoadsViewModel, SearchLoadsViewModel>();
            unity.RegisterType<ISearchTrucksViewModel, SearchTrucksViewModel>();
            unity.RegisterType<ITopButtonsViewModel, TopButtonsViewModel>();
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
