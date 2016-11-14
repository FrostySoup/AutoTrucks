using Model.DataFromView;
using Service.AddNewWindowFactory;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.PopUpWindowViewModels.RemoteConnectionViewModels
{
    public class RemoteConnectionViewModel : NotifyPropertyChangedAbstract, IRemoteConnectionViewModel
    {
        public RemoteConnection remoteConnection { get; set; }

        public bool saveData { get; set; }

        public ICommand SaveDataCommand { get; private set; }

        private IWindowFactory _windowFactory;

        private readonly string windowName = "Remote connection";

        public RemoteConnectionViewModel(IWindowFactory windowFactory)
        {
            remoteConnection = new RemoteConnection();
            _windowFactory = windowFactory;
            this.SaveDataCommand = new DelegateCommand(o => this.SaveData());
        }

        private void SaveData()
        {
            saveData = true;
            _windowFactory.CloseWindowByName(windowName);
        }

        public string PublicIP
        {
            get { return remoteConnection.RemoteIp; }
            set {
                remoteConnection.RemoteIp = value;
                OnPropertyChanged("PublicIP");
            }
        }

        public string Port
        {
            get { return remoteConnection.Port; }
            set
            {
                remoteConnection.Port = value;
                OnPropertyChanged("Port");
            }
        }
    }
}
