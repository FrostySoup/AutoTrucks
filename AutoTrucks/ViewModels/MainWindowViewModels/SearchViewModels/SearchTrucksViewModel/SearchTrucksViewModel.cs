using Model;
using Model.SearchCRUD;
using Service.AddNewWindowFactory;
using Service.Commands;
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
        private ObservableCollection<Truck> trucks;

        private string name;

        private int number;

        private CreateSearch newSearch;

        public ICommand OpenSearchWindowCommand { get; private set; }

        public ICommand CreateSearchCommand { get; private set; }


        public SearchTrucksViewModel(IWindowFactory windowFactory)
        {
            //Remove later-----------------------------
            FillDataFactory fillDataFactory = new FillDataFactory();

            trucks = fillDataFactory.GenerateTrucks();

            newSearch = new CreateSearch()
            {
                criteria = Model.Enum.AssetType.Shipment,
                ageLimitMinutes = 90,
                destination = Model.Enum.StateProvince.IL,
                equipmentClasses = new[] { Model.Enum.EquipmentType.Flatbed, Model.Enum.EquipmentType.Reefer },
                includeFulls = true,
                includeLtls = true
            };

            //------------------------------------------------------

            this.windowFactory = windowFactory;

            this.OpenSearchWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());

            this.CreateSearchCommand = new DelegateCommand(o => this.CreateSearch());

        }

        private void CreateSearch()
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public ObservableCollection<Truck> Trucks
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
