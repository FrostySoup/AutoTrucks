using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.ReceiveData.AlarmMatch;
using Model.DataFromView;

namespace Service.ConnexionService.AlarmService
{
    public interface IHttpService : IDisposable
    {
        void Start(RemoteConnection port);
        new void Dispose();
        void Stop();
        event Action<HttpListenerContext> ProcessRequest;

        ObservableCollection<DisplayFoundAsset> GetAssets();
        void BindCommand(ICommand assetUpdatedCommand);
        void ClearFoundAssets();
        void RemoveAsset(DisplayFoundAsset asset);
    }
}
