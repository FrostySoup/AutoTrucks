using Model.DataFromView;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
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

        protected IDataConvertPostAssetService dataConvertService;

        public ICommand OpenPostAssetWindowCommand { get; set; }
        public ICommand PostTruckCommand { get; set; }
        public ICommand RemoveAssetsCommand { get; set; }

        protected IDataExtractService dataExtractService;

        protected ObservableCollection<PostDataFromView> postAssets;

        protected AssetsAbstractViewModel(IWindowFactory windowFactory, IPostWindowViewModel postWindowViewModel, IConnectConnexionService connectConnexionService, 
            ISessionCacheSingleton sessionCacheSingleton, IDataConvertPostAssetService dataConvertService)
        {
            this.RemoveAssetsCommand = new DelegateCommand(o => this.RemoveSelectedAssets());
            postAssets = new ObservableCollection<PostDataFromView>();
            this.windowFactory = windowFactory;
            this.postWindowViewModel = postWindowViewModel;
            this.connectConnexionService = connectConnexionService;
            this.sessionCacheSingleton = sessionCacheSingleton;
            this.dataConvertService = dataConvertService;
        }

        protected void GetExistingAssets()
        {
            if (sessionCacheSingleton.sessions != null && sessionCacheSingleton.sessions.Count > 0)
                convertData(connectConnexionService.QueryAllMyAssets(sessionCacheSingleton.sessions[0]));
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
                if (sessionCacheSingleton.sessions != null && sessionCacheSingleton.sessions.Count > 0)
                {
                    connectConnexionService.DeleteAssetsById(sessionCacheSingleton.sessions[0], Ids.ToArray());
                    convertData(connectConnexionService.QueryAllMyAssets(sessionCacheSingleton.sessions[0]));
                    OnPropertyChanged("PostToDisplay");
                }
            }
        }

        protected void OpenPostAssetWindow()
        {
            windowFactory.CreateNewPostAssetWindow(postWindowViewModel);
            if (postWindowViewModel.saveChanges == true && postWindowViewModel.postData != null)
            {
                if (sessionCacheSingleton.sessions != null && sessionCacheSingleton.sessions.Count > 0)
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

        protected abstract void convertData(LookupAssetSuccessData lookupAssetSuccessData);

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
    }
}
