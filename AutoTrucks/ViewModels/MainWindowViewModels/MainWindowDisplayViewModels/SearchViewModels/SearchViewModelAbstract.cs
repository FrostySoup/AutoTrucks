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

        protected ObservableCollection<SearchCreated> assets;

        protected ObservableCollection<SearchAssetsSearches> searchesToDisplay;

        protected SearchOperationParams search;

        protected IDataConvertSingleton dataConvertSingleton;

        protected ISessionCacheSingleton sessionCacheSingleton;

        protected IConnectConnexionService connectConnexionService;

        public SearchViewModelAbstract(IDataConvertSingleton dataConvertSingleton, ISessionCacheSingleton sessionCacheSingleton,
            ISearchWindowViewModel searchWindowViewModel, IConnectConnexionService connectConnexionService)
        {
            this.dataConvertSingleton = dataConvertSingleton;
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
                CreateSearchSuccessData searchSuccessData = new CreateSearchSuccessData();
                assets = new ObservableCollection<SearchCreated>();
                foreach (SearchAssetsSearches asset in searchesToDisplay)
                {
                    if (asset.Marked)
                    {
                        search = DataConvertSingleton.Instance
                              .ToSearchOperationParams(asset.SearchData, assetType);
                        searchSuccessData = connectConnexionService
                            .SearchConnexion(sessionCacheSingleton.sessions[0], search);

                        ConvertIntoDisplayableData(searchSuccessData, new DataColors() { BackgroundColor = asset.BackgroundColor, ForegroundColor = asset.ForegroundColor });
                    }
                }                             
            }
            else
            {
                //Temporar solution
                sessionCacheSingleton.RenewSessionsForEachData();
            }
        }

        protected abstract void AddNewSearch(SearchDataFromView searchData);

        protected abstract void ConvertIntoDisplayableData(CreateSearchSuccessData searchSuccessData, DataColors dataColors);

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
