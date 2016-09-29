﻿using Model;
using Service.FillDataFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModels.MainWindowViewModels
{
    public class SearchTrucksViewModel : ISearchTrucksViewModel
    {
        private ObservableCollection<Truck> trucks;

        private string name;

        private int number;

        public SearchTrucksViewModel()
        {
            //Remove later-----------------------------
            FillDataFactory fillDataFactory = new FillDataFactory();

            trucks = fillDataFactory.GenerateTrucks();

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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}