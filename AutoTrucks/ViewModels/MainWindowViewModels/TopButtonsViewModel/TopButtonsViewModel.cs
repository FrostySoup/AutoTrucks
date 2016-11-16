using Model.ReceiveData.Login;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.SerializeServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;
using ViewModels.PopUpWindowViewModels.BlacklistViewModel;
using ViewModels.PopUpWindowViewModels.RemoteConnectionViewModels;

namespace ViewModels.MainWindowViewModels
{
    public class TopButtonsViewModel : NotifyPropertyChangedAbstract, ITopButtonsViewModel
    {

        private readonly IWindowFactory windowFactory;

        private IDataSourceViewModel dataSourceViewModel;

        private ISerializeService serializeService;

        private IRemoteConnectionViewModel remoteConnectionViewModel;

        private IBlacklistViewModel blacklistViewModel;

        private ISessionCacheSingleton sessionCacheSingleton;

        public ICommand OpenWindowCommand { get; private set; }

        public ICommand ChangePostTrucksViewModelCommand { get; private set; }
        public ICommand ChangePostLoadsViewModelCommand { get; private set; }
        public ICommand OpenBlacklistCommand { get; private set; }
        public ICommand OpemRemoteConnectionCommand { get; private set; }

        public TopButtonsViewModel(IWindowFactory windowFactory, IDataSourceViewModel dataSourceViewModel,
            IRemoteConnectionViewModel remoteConnectionViewModel, ISerializeService serializeService, ISessionCacheSingleton sessionCacheSingleton,
            IBlacklistViewModel blacklistViewModel)
        {
            this.blacklistViewModel = blacklistViewModel;
            this.sessionCacheSingleton = sessionCacheSingleton;
            this.serializeService = serializeService;
            this.windowFactory = windowFactory;
            this.remoteConnectionViewModel = remoteConnectionViewModel;
            this.dataSourceViewModel = dataSourceViewModel;
            this.OpenWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());
            this.OpemRemoteConnectionCommand = new DelegateCommand(o => this.OpemRemoteConnection());
            this.OpenBlacklistCommand = new DelegateCommand(o => this.OpenBlacklist());
        }

        private void OpemRemoteConnection()
        {
            windowFactory.CreateNewRemoteConnectionWindow(remoteConnectionViewModel);
            if (remoteConnectionViewModel.saveData)
            {
                remoteConnectionViewModel.saveData = false;
                serializeService.SerializeRemoteConnection(remoteConnectionViewModel.remoteConnection);
                sessionCacheSingleton.UpdateAlarmAdress();
            }
        }

        private void OpenBlacklist()
        {
            blacklistViewModel.RefreshBlacklist();
            windowFactory.CreateNewBlacklistWindow(blacklistViewModel);
        }

        private void OpenWindowConnections()
        {
            windowFactory.CreateNewDataSourceWindow(dataSourceViewModel);      
        }

        public void AddCommand(ICommand changePostTrucksViewModelCommand, ICommand changePostLoadsViewModelCommand)
        {
            ChangePostTrucksViewModelCommand = changePostTrucksViewModelCommand;
            ChangePostLoadsViewModelCommand = changePostLoadsViewModelCommand;
        }
    }
}
