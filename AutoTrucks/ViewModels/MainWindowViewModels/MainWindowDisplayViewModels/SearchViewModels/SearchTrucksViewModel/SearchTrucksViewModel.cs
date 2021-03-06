﻿using Model;
using Model.DataFromView;
using Model.DataHelpers;
using Model.DataToView;
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

        public SearchTrucksViewModel(IWindowFactory windowFactory, IDataConvertService dataConvertSingleton, ISessionCacheSingleton sessionCacheSingleton,
            ISearchWindowViewModel searchWindowViewModel, IConnectConnexionService connectConnexionService)
            : base(dataConvertSingleton, sessionCacheSingleton, searchWindowViewModel, connectConnexionService)
        {
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

        protected override void ConvertIntoDisplayableData(CreateSearchSuccessData searchSuccessData, DataColors dataColors)
        {
            var foundList = dataConvertService.EquipmentCreateSearchSuccessDataToSearchAssetsReceived(searchSuccessData, dataColors);

            foreach(var item in foundList)
            {
                assets.Add(item);
            }
        }

        private void SearchForSelectedTruck()
        {
            PerformAssetSearch(AssetType.Equipment);
            OnPropertyChanged("Assets");
        }      

        public ObservableCollection<SearchAssetsSearches> SearchesToDisplay
        {
            get { 
                return searchesToDisplay;
            }
        }
    }
}
