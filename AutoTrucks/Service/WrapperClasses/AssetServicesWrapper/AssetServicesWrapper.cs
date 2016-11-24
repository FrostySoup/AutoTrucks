using Service.AddNewWindowFactory;
using Service.ConnexionService;
using Service.ConnexionService.AlarmService;
using Service.DataConvertService;
using Service.ViewModelsHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.WrapperClasses.AssetServicesWrapper
{
    public class AssetServicesWrapper : IAssetServicesWrapper
    {
        IWindowFactory windowFactory;
        IConnectConnexionService connectConnexionService;
        ISessionCacheSingleton sessionCacheSingleton;
        IDataConvertPostAssetService dataConvertService;
        IHttpService httpService;
        IAssetsViewModelHelper assetsViewModelHelper;
    }
}
