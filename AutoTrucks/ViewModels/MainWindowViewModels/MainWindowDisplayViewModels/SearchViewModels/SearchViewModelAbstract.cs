using Model.DataFromView;
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

        protected ObservableCollection<SearchOperationParams> searches;

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
                AddNewSearch(searchWindowViewModel.searchData);
        }

        protected void PerformAssetSearch(AssetType assetType)
        {
            CreateSearchSuccessData searchSuccessData;

            if (searchesToDisplay.Count > 0 && sessionCacheSingleton.sessions.Count > 0)
            {
                searches.Add(DataConvertSingleton.Instance
                    .ToSearchOperationParams(searchesToDisplay[0].SearchData, assetType));

                searchSuccessData = connectConnexionService
                    .SearchConnexion(sessionCacheSingleton.sessions[0], searches[0]);
                ConvertIntoDisplayableData(searchSuccessData);
            }
            else
            {
                //Temporary solution
                sessionCacheSingleton.RenewSessionsForEachData();
            }
        }

        protected abstract void AddNewSearch(SearchDataFromView searchData);

        protected abstract void ConvertIntoDisplayableData(CreateSearchSuccessData searchSuccessData);

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
