using Model;
using Model.DataFromView;
using Model.DataToView;
using Model.ReceiveData.CreateSearch;
using Model.SearchCRUD;
using Model.SendData;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.DataConvertService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;

namespace ViewModels.MainWindowViewModels
{
    public class SearchTrucksViewModel : SearchViewModelAbstract, IMainWindowDisplayViewModel
    {                   

        public ICommand OpenSearchWindowCommand { get; private set; }

        public ICommand SearchForSelectedTruckCommand { get; private set; }

        public SearchTrucksViewModel(IWindowFactory windowFactory, IDataConvertSingleton dataConvertSingleton, ISessionCacheSingleton sessionCacheSingleton,
            ISearchWindowViewModel searchWindowViewModel, IConnectConnexionService connectConnexionService)
            : base(dataConvertSingleton, sessionCacheSingleton, searchWindowViewModel, connectConnexionService)
        {
            searches = new ObservableCollection<SearchOperationParams>();

            searchesToDisplay = new ObservableCollection<SearchAssetsSearches>();

            this.windowFactory = windowFactory;

            this.OpenSearchWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());

            this.SearchForSelectedTruckCommand = new DelegateCommand(o => this.SearchForSelectedTruck());
        }

        protected override void AddNewSearch(SearchDataFromView searchData)
        {       
            searchesToDisplay.Add(new SearchAssetsSearches(searchData));
            OnPropertyChanged("SearchesToDisplay");
        }     

        private void SearchForSelectedTruck()
        {
            PerformAssetSearch(AssetType.Shipment);
            OnPropertyChanged("Trucks");
        }

        public ObservableCollection<SearchCreated> Trucks
        {
            get
            {
                return assets;
            }
            set
            {
                assets = value;
                OnPropertyChanged("Trucks");
            }
        }

        public ObservableCollection<SearchAssetsSearches> SearchesToDisplay
        {
            get { 
                return searchesToDisplay;
            }
        }
    }
}
