﻿using Model;
using Model.DataFromView;
using Model.DataToView;
using Model.ReceiveData.CreateSearch;
using Model.SearchCRUD;
using Model.SendData;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.DataConvertService;
using Service.FillDataFactory;
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
        private ObservableCollection<SearchCreated> trucks;

        private SearchOperationParams newSearch;

        private ObservableCollection<SearchOperationParams> searches;

        private ObservableCollection<SearchTrucksSearches> searchesToDisplay;

        public ICommand OpenSearchWindowCommand { get; private set; }

        public ICommand SearchForSelectedTruckCommand { get; private set; }

        public SearchTrucksViewModel(IWindowFactory windowFactory)
        {
            searches = new ObservableCollection<SearchOperationParams>();

            searchesToDisplay = new ObservableCollection<SearchTrucksSearches>();

            newSearch = SetValuesForSearch();

            this.windowFactory = windowFactory;

            this.OpenSearchWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());

            this.SearchForSelectedTruckCommand = new DelegateCommand(o => this.SearchForSelectedTruck());
        }

        protected override void AddNewSearch(SearchDataFromView searchData)
        {
            //searches.Add(DataConvertSingleton.Instance
            //.ToSearchOperationParams(searchData, AssetType.Shipment));

            searchesToDisplay.Add(new SearchTrucksSearches(searchData));
            OnPropertyChanged("SearchesToDisplay");

        }

        private SearchOperationParams SetValuesForSearch()
        {
            var origin = new SearchArea { stateProvinces = new[] { StateProvince.CA } };

            var destination = new SearchArea { stateProvinces = new[] { StateProvince.IL } };

            var searchCriteria = new CreateSearchCriteria
            {
                ageLimitMinutes = 90,
                ageLimitMinutesSpecified = true,
                assetType = AssetType.Shipment,
                destination = new GeoCriteria { Item = destination },
                equipmentClasses = new[] { EquipmentClass.Flatbeds, EquipmentClass.Reefers },
                includeFulls = true,
                includeLtls = true,
                origin = new GeoCriteria { Item = origin }
            };

            return new SearchOperationParams
            {
                criteria = searchCriteria,
                includeSearch = true,
                includeSearchSpecified = true,
                sortOrder = SortOrder.Age,
                sortOrderSpecified = true
            };
        }

        private void SearchForSelectedTruck()
        {
            CreateSearchSuccessData searchSuccessData;
            
            if (SessionCacheSingleton.Instance.sessions.Count > 0 && searchesToDisplay.Count > 0)
            {
                searches.Add(DataConvertSingleton.Instance
                    .ToSearchOperationParams(searchesToDisplay[0].SearchData, AssetType.Shipment));
                Trucks = new ObservableCollection<SearchCreated>();
                searchSuccessData = ConnectConnexionServiceSingleton
                    .Instance.SearchConnexion(SessionCacheSingleton.Instance.sessions[0], searches[0]);

                MapSearchResultsToSearchCreated(searchSuccessData);
              
                OnPropertyChanged("Trucks");
            }
            else
            {
                //Temporary solution
                SessionCacheSingleton.Instance.RenewSessionsForEachData();
            }                  
        }

        private void MapSearchResultsToSearchCreated(CreateSearchSuccessData searchSuccessData)
        {
            if (searchSuccessData != null)
            {
                foreach (MatchingAsset match in searchSuccessData.matches)
                {
                    Shipment truck = (Shipment)match.asset.Item;
                    Trucks.Add(new SearchCreated()
                    {
                        Destination = truck.destination,
                        Truck = truck.equipmentType,
                        Origin = truck.origin,
                        Avail = match.asset.availability,
                        FP = match.asset.ltl,
                        DHD = match.destinationDeadhead,
                        DHO = match.originDeadhead,
                        Age = match.asset.status.created.date,
                        InitialO = match.callback.postersStateProvince.ToString()
                    });
                }
            }
        }

        public ObservableCollection<SearchCreated> Trucks
        {
            get
            {
                return trucks;
            }
            set
            {
                trucks = value;
                OnPropertyChanged("Trucks");
            }
        }

        public ObservableCollection<SearchTrucksSearches> SearchesToDisplay
        {
            get { 
                return searchesToDisplay;
            }
        }
    }
}
