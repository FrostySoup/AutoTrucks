using Model.DataFromView;
using Model.DataToView;
using Model.Enums;
using Model.SearchCRUD;
using Model.SendData;
using Service.AddNewWindowFactory;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ViewModels.PopUpWindowViewModels
{
    public class SearchWindowViewModel : ISearchWindowViewModel
    {
        //private SearchOperationParams searchData;

        public SearchDataFromView searchData { get; private set; }

        private IWindowFactory windowFactory;     

        public bool saveData { get; private set; }

        public ICommand CloseWindowSaveDataCommand { get; private set; }

        public ICommand CloseWindowCommand { get; private set; }

        public SearchWindowViewModel(IWindowFactory windowFactory)
        {
            this.windowFactory = windowFactory;
            searchData = new SearchDataFromView();
            this.CloseWindowSaveDataCommand = new DelegateCommand(o => this.CloseSaveDataWindow());
            this.CloseWindowCommand = new DelegateCommand(o => this.CloseWindow());
        }

        private void CloseSaveDataWindow()
        {

            saveData = true;
            windowFactory.CloseSearchWindow();
        }

        private void CloseWindow()
        {
            saveData = false;
            searchData = new SearchDataFromView();
            windowFactory.CloseSearchWindow();
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
                origin = new GeoCriteria { Item = origin },
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

        #region On property changed Members

        public Color? BackgroundColor
        {
            get { return searchData.backgroundColor; }
            set
            {
                searchData.backgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        public Color? ForegroundColor
        {
            get { return searchData.foregroundColor; }
            set
            {
                searchData.foregroundColor = value;
                OnPropertyChanged("ForegroundColor");
            }
        }

        public DateTime AvailFrom
        {
            get { return searchData.availFrom; }
            set
            {
                searchData.availFrom = value;
                OnPropertyChanged("AvailFrom");
            }
        }

        public DateTime AvailTo
        {
            get { return searchData.availTo; }
            set
            {
                searchData.availTo = value;
                OnPropertyChanged("AvailTo");
            }
        }

        public ObservableCollection<EquipmentType> EquipmentTypeSelected
        {
            get { return searchData.equipmentType; }
            set
            {
                searchData.equipmentType = value;
                OnPropertyChanged("EquipmentTypeSelected");
            }
        }

        public IEnumerable<EquipmentType> EquipmentTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(EquipmentType))
                    .Cast<EquipmentType>();
            }
        }

        public StateProvince DestinationProvinceSelect
        {
            get { return searchData.destinationProvince; }
            set
            {
                searchData.destinationProvince = value;
                OnPropertyChanged("DestinationProvince");
            }
        }

        public StateProvince OriginProvinceSelect
        {
            get { return searchData.originProvince; }
            set
            {
                searchData.originProvince = value;
                OnPropertyChanged("OriginProvince");
            }
        }

        public IEnumerable<StateProvince> ProvinceValues
        {
            get
            {
                return Enum.GetValues(typeof(StateProvince))
                    .Cast<StateProvince>();
            }
        }

        public int DHO
        {
            get { return searchData.dho; }
            set
            {
                searchData.dho = value;
                this.OnPropertyChanged("DHO");
            }
        }

        public FullOrPartial SelectedFullOrPartial
        {
            get { return searchData.fullOrPartial; }
            set
            {
                if (value == FullOrPartial.Both)
                {
                    searchData.includeFulls = true;
                    searchData.includeLtls = true;
                }else if (value == FullOrPartial.Full)
                {
                    searchData.includeFulls = true;
                    searchData.includeLtls = false;
                }
                else
                {
                    searchData.includeFulls = false;
                    searchData.includeLtls = true;
                }
                searchData.fullOrPartial = value;
                OnPropertyChanged("FullOrPartial");
            }
        }

        public IEnumerable<FullOrPartial> FullOrPartialValues
        {
            get
            {
                return Enum.GetValues(typeof(FullOrPartial))
                    .Cast<FullOrPartial>();
            }
        }

        public string SelectedWeigth
        {
            get { return searchData.weight; }
            set
            {
                searchData.weight = value;
                OnPropertyChanged("Weigth");
            }
        }

        public IEnumerable<string> WeigthValues
        {
            get
            {
                IEnumerable<string> weightValues = EnumExtensionForDescription<Weigth>
                    .GetAllDescriptions(Enum.GetValues(typeof(Weigth))
                    .Cast<Weigth>());
                return weightValues;
            }
        }

        public string SelectedLength
        {
            get { return searchData.length; }
            set
            {
                searchData.length = value;
                OnPropertyChanged("LengthEnum");
            }
        }

        public IEnumerable<string> LengthValues
        {
            get
            {
                IEnumerable<string> lenghtValues = EnumExtensionForDescription<Length>
                    .GetAllDescriptions(Enum.GetValues(typeof(Length))
                    .Cast<Length>());
                return lenghtValues;
            }
        }

        public int DHD
        {
            get { return searchData.dhd; }
            set
            {
                searchData.dhd = value;
                this.OnPropertyChanged("DHD");
            }
        }

        public string Length
        {
            get { return searchData.length; }
            set
            {
                searchData.length = value;
                this.OnPropertyChanged("Length");
            }
        }

        public string Weight
        {
            get { return searchData.weight; }
            set
            {
                searchData.weight = value;
                this.OnPropertyChanged("Weight");
            }
        }

        public int SearchBack
        {
            get { return searchData.searchBack; }
            set
            {
                searchData.searchBack = value;
                this.OnPropertyChanged("SearchBack");
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
