using Model.DataFromView;
using Model.Enums;
using Model.ReceiveData.AlarmMatch;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.ConnexionService.AlarmService;
using Service.DataConvertService;
using Service.DataExtractService;
using Service.SerializeServices;
using Service.ViewModelsHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;

namespace ViewModels.MainWindowViewModels.MainWindowDisplayViewModels.PostAssetsViewModels
{
    public abstract class AssetsAbstractViewModel : NotifyPropertyChangedAbstract
    {
        private IWindowFactory windowFactory;

        private IPostWindowViewModel postWindowViewModel;

        private IConnectConnexionService connectConnexionService;

        private ISessionCacheSingleton sessionCacheSingleton;      

        private IAssetsViewModelHelper assetsViewModelHelper;        

        protected IDataExtractService dataExtractService;       

        protected IDataConvertPostAssetService dataConvertService;

        protected IHttpService httpService;

        protected bool isGroupSelected;

        protected ObservableCollection<PostDataFromView> postAssets;

        public ICommand OpenPostAssetWindowCommand { get; set; }
        public ICommand RemoveAssetsCommand { get; set; }
        public ICommand StartAlarmsCommand { get; set; }
        public ICommand StopAlarmCommand { get; set; }
        public ICommand AssetUpdatedCommand { get; set; }
        public ICommand ClearFoundAssetsCommand { get; set; }

        private ICommand m_blacklistCommand;       

        protected AssetsAbstractViewModel(IWindowFactory windowFactory, IPostWindowViewModel postWindowViewModel, IConnectConnexionService connectConnexionService,
            ISessionCacheSingleton sessionCacheSingleton, IDataConvertPostAssetService dataConvertService, IHttpService httpService, IAssetsViewModelHelper assetsViewModelHelper)
        {
            this.ClearFoundAssetsCommand = new DelegateCommand(o => this.ClearFoundAssets());
            this.RemoveAssetsCommand = new DelegateCommand(o => this.RemoveSelectedAssets());
            this.StartAlarmsCommand = new DelegateCommand(o => this.StartAlarms());
            this.StopAlarmCommand = new DelegateCommand(o => this.StopAlarms());
            this.AssetUpdatedCommand = new DelegateCommand(o => this.AssetUpdated());
            postAssets = new ObservableCollection<PostDataFromView>();
            this.windowFactory = windowFactory;
            this.postWindowViewModel = postWindowViewModel;
            this.connectConnexionService = connectConnexionService;
            this.sessionCacheSingleton = sessionCacheSingleton;
            this.dataConvertService = dataConvertService;
            this.httpService = httpService;
            this.httpService.BindCommand(AssetUpdatedCommand);
            this.assetsViewModelHelper = assetsViewModelHelper;
        }       

        private void StopAlarms()
        {
            if (CheckSession())
            {
                connectConnexionService.DeleteAlarms(null, sessionCacheSingleton.sessions.FirstOrDefault());
            }
            httpService.Dispose();
        }

        private void StartAlarms()
        {
            if (CheckSession())
            {              
                foreach (var asset in postAssets)
                {
                    if (asset.alarm == null)                    
                        asset.alarm = assetsViewModelHelper.AddAlarmForAsset(sessionCacheSingleton.sessions.FirstOrDefault(), asset, connectConnexionService);
                }
            }          
            OnPropertyChanged("PostToDisplay");
            var url = connectConnexionService.LookupAlarmUrl(sessionCacheSingleton.sessions[0]);
            httpService.Start(sessionCacheSingleton.remoteURI);
        }

        protected void GetExistingAssets()
        {
            if (CheckSession())
            {
                convertData(connectConnexionService.QueryAllMyAssets(sessionCacheSingleton.sessions[0]),
                    connectConnexionService.QueryAllMyAlarms(sessionCacheSingleton.sessions[0]));
                OnPropertyChanged("PostToDisplay");
            }
        }

        private bool CheckSession()
        {
            if (sessionCacheSingleton.sessions != null && sessionCacheSingleton.sessions.Count > 0)
                return true;
            else return false;
        }

        protected void GetExistingMyGroupAssets()
        {
            if (CheckSession())
            {
                convertData(connectConnexionService.QueryAllMyGroupAssets(sessionCacheSingleton.sessions[0]),
                    connectConnexionService.QueryAllMyGroupAlarms(sessionCacheSingleton.sessions[0]));
                OnPropertyChanged("PostToDisplay");
            }
        }

        private void RemoveSelectedAssets()
        {          
            if (CheckSession())
            {
                postAssets = assetsViewModelHelper.RemoveSelectedAssets(sessionCacheSingleton.sessions.FirstOrDefault(), postAssets, connectConnexionService);
                OnPropertyChanged("PostToDisplay");
            }
        }

        protected void OpenPostAssetWindow()
        {
            windowFactory.CreateNewPostAssetWindow(postWindowViewModel);
            if (postWindowViewModel.saveChanges == true && postWindowViewModel.postData != null)
            {
                if (CheckSession())
                {                    
                    postWindowViewModel.postData.ID = connectConnexionService.PostNewAsset(sessionCacheSingleton.sessions[0], convertAssetIntoBaseType(postWindowViewModel.postData));
                    if (!string.IsNullOrEmpty(postWindowViewModel.postData.ID))
                    {
                        postAssets.Add(postWindowViewModel.postData);
                        postWindowViewModel.saveChanges = false;
                        postWindowViewModel.postData = new PostDataFromView();
                    }
                }
            }
        }

        protected abstract void convertData(LookupAssetSuccessData lookupAssetSuccessData, LookupAlarmSuccessData lookupAlarmSuccessData);

        protected abstract PostAssetOperation convertAssetIntoBaseType(PostDataFromView postData);

        public ICommand AddToBlackistCommand
        {
            get
            {
                if (m_blacklistCommand == null)
                {
                    m_blacklistCommand = new DelegateCommand(param => AddToBlacklist((string)param), param => CanAddToBlacklist);
                }
                return m_blacklistCommand;
            }
        }

        private void AddToBlacklist(string companyName)
        {
            if (companyName != null)
            {
                assetsViewModelHelper.SerializeCompanyName(companyName);
            }
        }

        private bool CanAddToBlacklist
        {
            get { return true; }
        }

        private void ClearFoundAssets()
        {
            httpService.ClearFoundAssets();
            OnPropertyChanged("FoundAssets");
        }

        private void AssetUpdated()
        {
            OnPropertyChanged("FoundAssets");
        }

        public ObservableCollection<PostDataFromView> PostToDisplay
        {
            get
            {
                return postAssets;
            }
            set
            {
                postAssets = value;
                OnPropertyChanged("PostToDisplay");
            }
        }

        public bool IsGroupSelected
        {
            get
            {
                return isGroupSelected;
            }
            set
            {
                isGroupSelected = value;
                OnPropertyChanged("IsGroupSelected");
                if (isGroupSelected)
                    GetExistingMyGroupAssets();
                else GetExistingAssets();
            }
        }

        public ObservableCollection<DisplayFoundAsset> FoundAssets
        {
            get
            {
                return assetsViewModelHelper.GetAssetsFromHttpService(httpService, postAssets);
            }
        }

    }
}
