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

namespace AutoTrucks
{
    public class Unity
    {
        public IUnityContainer unity { get; set; }
        public Unity()
        {
            unity = new UnityContainer();
            unity.RegisterType<MainWindowViewModel>();
            unity.RegisterType<IWindowFactory, WindowFactory>();
            unity.RegisterType<ILoginViewModel, LoginViewModel>();
            unity.RegisterType<IMainWindowCanvasViewModel, PostLoadsViewModel>();
            unity.RegisterType<IMainWindowCanvasViewModel, PostTrucksViewModel>();
            unity.RegisterType<IMainWindowCanvasViewModel, SearchLoadsViewModel>();
            unity.RegisterType<IMainWindowCanvasViewModel, SearchTrucksViewModel>();
            unity.RegisterType<ITopButtonsViewModel, TopButtonsViewModel>();
        }

        internal object ResolveMainWindow()
        {
            return unity.Resolve<MainWindowViewModel>();
        }
    }
}
