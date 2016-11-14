using Model.DataFromView;
using Model.ReceiveData.AlarmMatch;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.ConnexionService.AlarmService;
using Service.DataConvertService;
using Service.DataExtractService;
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

        protected bool isGroupSelected;

        protected IDataExtractService dataExtractService;

        protected ObservableCollection<PostDataFromView> postAssets;

        protected IDataConvertPostAssetService dataConvertService;

        protected IHttpService httpService;

        public ICommand OpenPostAssetWindowCommand { get; set; }
        public ICommand RemoveAssetsCommand { get; set; }
        public ICommand StartAlarmsCommand { get; set; }
        public ICommand StopAlarmCommand { get; set; }
        public ICommand AssetUpdatedCommand { get; set; }
        public ICommand ClearFoundAssetsCommand { get; set; }

        protected AssetsAbstractViewModel(IWindowFactory windowFactory, IPostWindowViewModel postWindowViewModel, IConnectConnexionService connectConnexionService,
            ISessionCacheSingleton sessionCacheSingleton, IDataConvertPostAssetService dataConvertService, IHttpService httpService)
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

        private void StopAlarms()
        {
            if (CheckSession())
            {
                connectConnexionService.DeleteAlarms(null, sessionCacheSingleton.sessions.FirstOrDefault());
            }
            httpService.Stop();
        }

        private void StartAlarms()
        {
            if (CheckSession())
            {              
                foreach (var asset in postAssets)
                {
                    //Transfer this code somewhere else later
                    if (asset.alarm == null) {
                        var alarmSearchCriteria = new AlarmSearchCriteria();
                        if (asset.DHD >= 0)
                            alarmSearchCriteria.destinationRadius = new Mileage()
                            {
                                miles = asset.DHD,
                                method = MileageType.Road
                            };
                        if (asset.DHO >= 0)
                            alarmSearchCriteria.originRadius = new Mileage()
                            {
                                miles = asset.DHO,
                                method = MileageType.Road
                            };
                        else
                            alarmSearchCriteria.originRadius = new Mileage()
                            {
                                miles = 50,
                                method = MileageType.Road
                            };
                        alarmSearchCriteria.maxMatches = 30;
                        alarmSearchCriteria.maxMatchesSpecified = true;
                        //----------------------------------------------------
                        asset.alarm = connectConnexionService.CreateAlarm(sessionCacheSingleton.sessions[0], asset.ID, alarmSearchCriteria);
                    }
                }
            }          
            OnPropertyChanged("PostToDisplay");
            //connectConnexionService.QueryAllMyByIdAlarms(sessionCacheSingleton.sessions[0], new string[] { "DA0Gmkda", "DA0Gmkdb" });
            httpService.Start(sessionCacheSingleton.defaultURL.AbsoluteUri);
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
            List<string> Ids = new List<string>();
            foreach (var item in postAssets)
            {
                if (item != null && item.Marked && item.ID != null)
                {
                    Ids.Add(item.ID);
                }
            }
            if (Ids.Count > 0)
            {
                if (CheckSession())
                {
                    connectConnexionService.DeleteAlarms(Ids, sessionCacheSingleton.sessions.FirstOrDefault());
                    connectConnexionService.DeleteAssetsById(sessionCacheSingleton.sessions.FirstOrDefault(), Ids.ToArray());
                    postAssets = new ObservableCollection<PostDataFromView>(postAssets
                        .Where(x => !Ids.Any(y => y.Equals(x.ID))));                   
                    OnPropertyChanged("PostToDisplay");
                }
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
                var assets = httpService.GetAssets();
                if (assets.LastOrDefault() != null)
                {
                    string assetId = assets.LastOrDefault().AssetId;
                    foreach (var post in postAssets)
                    {
                        if (assetId.Equals(post.ID))
                        {
                            assets.LastOrDefault().BackgroundColor = post.BackgroundColor;
                            break;
                        }
                    }
                }
                return assets;
            }
        }

    }
}
