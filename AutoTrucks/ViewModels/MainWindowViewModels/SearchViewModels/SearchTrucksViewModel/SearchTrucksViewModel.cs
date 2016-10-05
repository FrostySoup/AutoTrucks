﻿using Model;
using Model.ReceiveData.CreateSearch;
using Model.SearchCRUD;
using Model.SendData;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.FillDataFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;

namespace ViewModels.MainWindowViewModels
{
    public class SearchTrucksViewModel : SearchViewModelAbstract, ISearchTrucksViewModel
    {
        private ObservableCollection<SearchCreated> trucks;

        private string name;

        private int number;

        private SearchOperationParams newSearch;

        public ICommand OpenSearchWindowCommand { get; private set; }

        public ICommand CreateSearchCommand { get; private set; }

        public ICommand SearchForSelectedTruckCommand { get; private set; }

        public SearchTrucksViewModel(IWindowFactory windowFactory)
        {

            newSearch = SetValuesForSearch();

            this.windowFactory = windowFactory;

            this.OpenSearchWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());

            this.CreateSearchCommand = new DelegateCommand(o => this.CreateSearch());

            this.SearchForSelectedTruckCommand = new DelegateCommand(o => this.SearchForSelectedTruck());

        }

        private SearchOperationParams SetValuesForSearch()
        {
            var origin = new SearchArea { stateProvinces = new[] { StateProvince.CA, StateProvince.IL } };

            var destination = new SearchArea { zones = new[] { Zone.MidAtlantic } };

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
                sortOrder = SortOrder.Closest,
                sortOrderSpecified = true
            };
        }

        private void CreateSearch()
        {
            throw new NotImplementedException();
        }

        private void SearchForSelectedTruck()
        {
            CreateSearchSuccessData searchSuccessData;
            if (SessionCacheSingleton.Instance.sessions.Count > 0)
            {
                Trucks = new ObservableCollection<SearchCreated>();
                searchSuccessData = ConnectConnexionServiceSingleton.Instance.SearchConnexion(SessionCacheSingleton.Instance.sessions[0], newSearch);
                foreach (MatchingAsset match in searchSuccessData.matches)
                {
                    Shipment truck = (Shipment)match.asset.Item;
                    Trucks.Add(new SearchCreated()
                    {
                        Destination = truck.destination,
                        Truck = truck.equipmentType,
                        Origin = truck.origin
                    });
                }
                OnPropertyChanged("Trucks");
            }
            else
            {
                //Temporary solution
                SessionCacheSingleton.Instance.RenewSessionsForEachData();
            }                  
        }

        public string Name
        {
            get
            {
                return name;
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
    }
}
