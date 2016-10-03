using Model;
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

        public ICommand OpenSearchWindowCommand { get; private set; }

        public SearchTrucksViewModel(IWindowFactory windowFactory)
        {
            //Remove later-----------------------------
            FillDataFactory fillDataFactory = new FillDataFactory();

            trucks = fillDataFactory.GenerateTrucks();

            this.windowFactory = windowFactory;

            this.OpenSearchWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());

            //-----------------------------------------
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
