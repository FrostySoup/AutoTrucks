﻿using Service.AddNewWindowFactory;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;
using Service.DataConvertService;
using Service.ConnexionService;
using Model.DataFromView;
using System.Collections.ObjectModel;
using Model.SendData;
using Model.DataToView;
using Model.ReceiveData.CreateSearch;

namespace ViewModels.MainWindowViewModels
{
    public class SearchLoadsViewModel : SearchViewModelAbstract, IMainWindowDisplayViewModel
    {
        public ICommand OpenSearchWindowCommand { get; private set; }

        public ICommand SearchForSelectedTruckCommand { get; private set; }

        public SearchLoadsViewModel(IWindowFactory windowFactory, IDataConvertSingleton dataConvertSingleton, ISessionCacheSingleton sessionCacheSingleton,
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

        protected override void ConvertIntoDisplayableData(CreateSearchSuccessData searchSuccessData)
        {
            dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchCreated(searchSuccessData);
            assets = dataConvertSingleton.EquipmentCreateSearchSuccessDataToSearchCreated(searchSuccessData);
        }

        private void SearchForSelectedTruck()
        {
            PerformAssetSearch(AssetType.Equipment);
            OnPropertyChanged("Loads");
        }

        public ObservableCollection<SearchCreated> Loads
        {
            get
            {
                return assets;
            }
            set
            {
                assets = value;
                OnPropertyChanged("Loads");
            }
        }

        public ObservableCollection<SearchAssetsSearches> SearchesToDisplay
        {
            get
            {
                return searchesToDisplay;
            }
        }
    }
}