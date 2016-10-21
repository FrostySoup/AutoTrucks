using Model.DataFromView;
using Model.DataHelpers;
using Model.DataToView;
using Model.ReceiveData.CreateSearch;
using Model.SendData;
using Service.AddNewWindowFactory;
using Service.ConnexionService;
using Service.DataConvertService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;

namespace ViewModels.MainWindowViewModels
{
    public abstract class SearchViewModelAbstract : INotifyPropertyChanged
    {
        protected ISearchWindowViewModel searchWindowViewModel;

        protected IWindowFactory windowFactory;

        protected ObservableCollection<SearchAssetsReceived> assets;

        protected ObservableCollection<SearchAssetsSearches> searchesToDisplay;

        protected CreateSearchOperation search;

        protected IDataConvertService dataConvertService;

        protected ISessionCacheSingleton sessionCacheSingleton;

        protected IConnectConnexionService connectConnexionService;

        private bool isActive;

        private readonly string totalAssetsFoundString = "Search results Total : ";

        public SearchViewModelAbstract(IDataConvertService dataConvertService, ISessionCacheSingleton sessionCacheSingleton,
            ISearchWindowViewModel searchWindowViewModel, IConnectConnexionService connectConnexionService)
        {
            this.dataConvertService = dataConvertService;
            this.sessionCacheSingleton = sessionCacheSingleton;
            this.searchWindowViewModel = searchWindowViewModel;
            this.connectConnexionService = connectConnexionService;
        }


        protected void OpenWindowConnections()
        {
            windowFactory.CreateNewSearchWindow(searchWindowViewModel);
            if (searchWindowViewModel.saveData == true && searchWindowViewModel.searchData != null)
            {
                AddNewSearch(searchWindowViewModel.searchData);
                searchWindowViewModel.saveData = false;
                searchWindowViewModel.searchData = new SearchDataFromView();
            }
        }

        protected void PerformAssetSearch(AssetType assetType)
        {
            if (searchesToDisplay.Count < 1)
                return;

            if (sessionCacheSingleton.sessions.Count > 0)
            {
                isActive = true;
                OnPropertyChanged("ActivateLoad");
                CreateSearchSuccessData searchSuccessData = new CreateSearchSuccessData();
                assets = new ObservableCollection<SearchAssetsReceived>();
                foreach (SearchAssetsSearches asset in searchesToDisplay)
                {
                    if (asset.Marked)
                    {
                        search = dataConvertService
                              .ToSearchOperationParams(asset.SearchData, assetType);
                        searchSuccessData = connectConnexionService
                            .SearchConnexion(sessionCacheSingleton.sessions[0], search);
                        ConvertIntoDisplayableData(searchSuccessData, new DataColors() { BackgroundColor = asset.BackgroundColor, ForegroundColor = asset.ForegroundColor });
                    }
                }
                OnPropertyChanged("SearchResults");
                isActive = false;
                OnPropertyChanged("ActivateLoad");
            }
            else
            {
                sessionCacheSingleton.RenewSessionsForEachData();
            }
        }

        protected abstract void AddNewSearch(SearchDataFromView searchData);

        protected abstract void ConvertIntoDisplayableData(CreateSearchSuccessData searchSuccessData, DataColors dataColors);

        public string SearchResults
        {
            get
            {
                if (assets != null)
                    return totalAssetsFoundString + assets.Count.ToString();
                return totalAssetsFoundString + "0";
            }
        }

        public bool ActivateLoad
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged("ActivateLoad");
            }
        }

        public ObservableCollection<SearchAssetsReceived> Assets
        {
            get
            {
                return assets;
            }
            set
            {
                assets = value;
                OnPropertyChanged("Assets");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
