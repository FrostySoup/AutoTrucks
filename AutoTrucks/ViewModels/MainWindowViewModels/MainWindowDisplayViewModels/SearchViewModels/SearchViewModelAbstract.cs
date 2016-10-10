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

        protected void OpenWindowConnections()
        {
            //initiating VIEWMODEL
            searchWindowViewModel = new SearchWindowViewModel(windowFactory);
            windowFactory.CreateNewSearchWindow(searchWindowViewModel);
            if (searchWindowViewModel.saveData == true)
                AddNewSearch(searchWindowViewModel.searchData);
        }

        protected void PerformAssetSearch(AssetType assetType)
        {
            CreateSearchSuccessData searchSuccessData;

            if (SessionCacheSingleton.Instance.sessions.Count > 0 && searchesToDisplay.Count > 0)
            {
                searches.Add(DataConvertSingleton.Instance
                    .ToSearchOperationParams(searchesToDisplay[0].SearchData, assetType));

                searchSuccessData = ConnectConnexionServiceSingleton
                    .Instance.SearchConnexion(SessionCacheSingleton.Instance.sessions[0], searches[0]);

                assets = DataConvertSingleton.Instance.CreateSearchSuccessDataToSearchCreated(searchSuccessData);               
            }
            else
            {
                //Temporary solution
                SessionCacheSingleton.Instance.RenewSessionsForEachData();
            }
        }

        protected virtual void AddNewSearch(SearchDataFromView searchData){}

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
