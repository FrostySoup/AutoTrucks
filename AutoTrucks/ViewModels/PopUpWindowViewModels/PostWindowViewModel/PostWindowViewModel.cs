using Model.DataFromView;
using Model.Enums;
using Service.AddNewWindowFactory;
using Service.ColorListHolder;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace ViewModels.PopUpWindowViewModels.PostWindowViewModel
{
    public class PostWindowViewModel : NotifyPropertyChangedAbstract, IPostWindowViewModel
    {

        public ICommand CloseWindowSaveDataCommand { get; private set; }

        public ICommand CloseWindowCommand { get; private set; }

        public PostDataFromView postData { get; set; }

        private IColorListHolder colorListHolder;

        private readonly string windowName = "Post Window";

        IWindowFactory windowFactory;

        public bool saveChanges { get; set; }

        public PostWindowViewModel(IWindowFactory windowFactory, IColorListHolder colorListHolder)
        {
            this.windowFactory = windowFactory;
            this.colorListHolder = colorListHolder;
            this.CloseWindowCommand = new DelegateCommand(o => this.CloseWindow());
            this.CloseWindowSaveDataCommand = new DelegateCommand(o => this.CloseWindowSaveData());
            postData = new PostDataFromView();
        }

        private void CloseWindowSaveData()
        {
            if (postData.originState != StateProvince.Any || postData.destinationState != StateProvince.Any)
                saveChanges = true;
            windowFactory.CloseWindowByName(windowName);
        }

        private void CloseWindow()
        {
            saveChanges = false;
            windowFactory.CloseWindowByName(windowName);
        }

        #region On property changed Members

        public int CurrentMinValue
        {
            get { return postData.TripMinValue; }
            set
            {
                postData.TripMinValue = value;
                OnPropertyChanged("CurrentMinValue");
            }
        }

        public ObservableCollection<ColorItem> AvailableColors
        {
            get {
                return colorListHolder.GetColors();
            }
        }     

        public EquipmentType EquipmentTypeSelected
        {
            get { return postData.equipmentType; }
            set
            {
                postData.equipmentType = value;
                OnPropertyChanged("EquipmentTypeSelected");
            }
        }

        public IEnumerable<EquipmentType> EquipmentTypeValues
        {
            get {
                return Enum.GetValues(typeof(EquipmentType))
                   .Cast<EquipmentType>();
            }
        }

        public int CurrentMaxValue
        {
            get { return postData.TripMaxValue; }
            set
            {
                postData.TripMaxValue = value;
                OnPropertyChanged("CurrentMaxValue");
            }
        }

        public string CityOrigin
        {
            get { return postData.cityOrigin; }
            set
            {
                postData.cityOrigin = value;
                OnPropertyChanged("CityOrigin");
            }
        }

        public string CityDestination
        {
            get { return postData.cityDestination; }
            set
            {
                postData.cityDestination = value;
                OnPropertyChanged("CityDestination");
            }
        }

        public DateTime AvailFrom
        {
            get { return postData.availFrom; }
            set
            {
                postData.availFrom = value;
                OnPropertyChanged("AvailFrom");
            }
        }

        public DateTime AvailTo
        {
            get { return postData.availTo; }
            set
            {
                postData.availTo = value;
                OnPropertyChanged("AvailTo");
            }
        }

        public Color BackgroundColor
        {
            get { return postData.backgroundColor; }
            set
            {
                postData.backgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        public Color ForegroundColor
        {
            get { return postData.foregroundColor; }
            set
            {
                postData.foregroundColor = value;
                OnPropertyChanged("ForegroundColor");
            }
        }

        public string CommentOne
        {
            get { return postData.commentOne; }
            set
            {
                postData.commentOne = value;
                OnPropertyChanged("CommentOne");
            }
        }

        public string CommentTwo
        {
            get { return postData.commentTwo; }
            set
            {
                postData.commentTwo = value;
                OnPropertyChanged("CommentTwo");
            }
        }

        public StateProvince DestinationStateProvinceSelected
        {
            get { return postData.destinationState; }
            set
            {
                postData.destinationState = value;
                OnPropertyChanged("DestinationStateProvinceSelected");
            }
        }

        public IEnumerable<StateProvince> StateProvinceValues
        {
            get
            {
                return Enum.GetValues(typeof(StateProvince))
                   .Cast<StateProvince>();
            }
        }

        public StateProvince OriginStateProvinceSelected
        {
            get { return postData.originState; }
            set
            {
                postData.originState = value;
                OnPropertyChanged("OriginStateProvinceSelected");
            }
        }

        public int Length
        {
            get { return postData.length; }
            set
            {
                postData.length = value;
                OnPropertyChanged("Length");
            }
        }

        public int Weight
        {
            get { return postData.weight; }
            set
            {
                postData.weight = value;
                OnPropertyChanged("Weight");
            }
        }

        public int DHD
        {
            get { return postData.DHD; }
            set
            {
                postData.DHD = value;
                OnPropertyChanged("DHD");
            }
        }

        public int DHO
        {
            get { return postData.DHO; }
            set
            {
                postData.DHO = value;
                OnPropertyChanged("DHO");
            }
        }

        public FullOrPartial FullOrPartial
        {
            get { return postData.fullOrPartial; }
            set
            {
                if (value == FullOrPartial.Both)
                {
                    postData.includeFulls = true;
                    postData.includeLtls = true;
                }
                else if (value == FullOrPartial.Full)
                {
                    postData.includeFulls = true;
                    postData.includeLtls = false;
                }
                else
                {
                    postData.includeFulls = false;
                    postData.includeLtls = true;
                }
                postData.fullOrPartial = value;
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

        #endregion
    }
}
