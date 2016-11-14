using Model.DataFromView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PopUpWindowViewModels.RemoteConnectionViewModels
{
    public interface IRemoteConnectionViewModel : INotifyPropertyChanged
    {
        RemoteConnection remoteConnection { get; set; }
        bool saveData { get; set; }
    }
}
