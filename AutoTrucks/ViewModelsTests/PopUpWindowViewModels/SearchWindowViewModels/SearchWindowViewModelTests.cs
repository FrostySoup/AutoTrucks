using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.PopUpWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Service.AddNewWindowFactory;
using Moq;
using System.Windows.Input;
using Model.DataFromView;
using System.ComponentModel;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Model.Enums;

namespace ViewModels.PopUpWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class SearchWindowViewModelTests
    {
        Mock<IWindowFactory> windowFactory;
        SearchWindowViewModel searchWindowViewModel;
        List<string> receivedEvents;

        [TestInitialize]
        public void SetInitialValues()
        {
            windowFactory = new Mock<IWindowFactory>();
            searchWindowViewModel = new SearchWindowViewModel(windowFactory.Object);
            receivedEvents = new List<string>();
            searchWindowViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
        }

        [TestMethod()]
        public void CloseWindowDontSaveCommandTest()
        {            
            ICommand ic = searchWindowViewModel.CloseWindowCommand;
            ic.Execute(this);
            Assert.AreEqual(false, searchWindowViewModel.saveData);
        }

        [TestMethod()]
        public void CloseWindowSaveCommandTest()
        {
            ICommand ic = searchWindowViewModel.CloseWindowSaveDataCommand;
            searchWindowViewModel.DHO = 100;
            ic.Execute(this);
            Assert.AreEqual(100, searchWindowViewModel.searchData.dho);
        }

        [TestMethod()]
        public void OnBackgroundColorChangeTest()
        {
            searchWindowViewModel.BackgroundColor = new Color();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("BackgroundColor", receivedEvents[0]);
            Assert.AreEqual(new Color(), searchWindowViewModel.BackgroundColor);
        }

        [TestMethod()]
        public void OnForegroundColorChangeTest()
        {
            searchWindowViewModel.ForegroundColor = new Color();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("ForegroundColor", receivedEvents[0]);
            Assert.AreEqual(new Color(), searchWindowViewModel.ForegroundColor);
        }

        [TestMethod()]
        public void OnAvailFromChangeTest()
        {
            DateTime time = DateTime.Now;
            searchWindowViewModel.AvailFrom = time;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("AvailFrom", receivedEvents[0]);
            Assert.AreEqual(time, searchWindowViewModel.AvailFrom);
        }

        [TestMethod()]
        public void OnAvailToChangeTest()
        {
            DateTime time = DateTime.Now;
            searchWindowViewModel.AvailTo = time;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("AvailTo", receivedEvents[0]);
            Assert.AreEqual(time, searchWindowViewModel.AvailTo);
        }

        [TestMethod()]
        public void OnEquipmentTypeSelectedTest()
        {
            ObservableCollection<EquipmentType> equipment = new ObservableCollection<EquipmentType>();
            searchWindowViewModel.EquipmentTypeSelected = equipment;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("EquipmentTypeSelected", receivedEvents[0]);
            Assert.AreEqual(equipment, searchWindowViewModel.EquipmentTypeSelected);
        }

        [TestMethod()]
        public void OnDestinationProvinceChangeTest()
        {
            searchWindowViewModel.DestinationProvinceSelect = new StateProvince();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("DestinationProvince", receivedEvents[0]);
            Assert.AreEqual(new StateProvince(), searchWindowViewModel.DestinationProvinceSelect);
        }

        [TestMethod()]
        public void OnOriginProvinceChangeTest()
        {
            searchWindowViewModel.OriginProvinceSelect = new StateProvince();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("OriginProvince", receivedEvents[0]);
            Assert.AreEqual(new StateProvince(), searchWindowViewModel.OriginProvinceSelect);
        }

        [TestMethod()]
        public void OnDHOChangeTest()
        {
            int value = 100;
            searchWindowViewModel.DHO = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("DHO", receivedEvents[0]);
            Assert.AreEqual(value, searchWindowViewModel.DHO);
        }

        [TestMethod()]
        public void OnDHDChangeTest()
        {
            int value = 100;
            searchWindowViewModel.DHD = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("DHD", receivedEvents[0]);
            Assert.AreEqual(value, searchWindowViewModel.DHD);
        }

        [TestMethod()]
        public void OnCityOriginChangeTest()
        {
            string value = "peep";
            searchWindowViewModel.CityOrigin = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("CityOrigin", receivedEvents[0]);
            Assert.AreEqual(value, searchWindowViewModel.CityOrigin);
        }

        [TestMethod()]
        public void OnCityDestinationChangeTest()
        {
            string value = "peep";
            searchWindowViewModel.CityDestination = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("CityDestination", receivedEvents[0]);
            Assert.AreEqual(value, searchWindowViewModel.CityDestination);
        }

        [TestMethod()]
        public void OnFullChangeTest()
        {
            searchWindowViewModel.SelectedFullOrPartial = FullOrPartial.Full;
            Assert.AreEqual(true, searchWindowViewModel.searchData.includeFulls);
            Assert.AreEqual(false, searchWindowViewModel.searchData.includeLtls);
        }

        [TestMethod()]
        public void OnPartialChangeTest()
        {
            searchWindowViewModel.SelectedFullOrPartial = FullOrPartial.Partial;
            Assert.AreEqual(false, searchWindowViewModel.searchData.includeFulls);
            Assert.AreEqual(true, searchWindowViewModel.searchData.includeLtls);
        }

        [TestMethod()]
        public void OnFullAndPartialChangeTest()
        {
            searchWindowViewModel.SelectedFullOrPartial = FullOrPartial.Both;
            Assert.AreEqual(true, searchWindowViewModel.searchData.includeFulls);
            Assert.AreEqual(true, searchWindowViewModel.searchData.includeLtls);
        }

        [TestMethod()]
        public void OnWeightChangeTest()
        {
            searchWindowViewModel.SelectedWeigth = Weigth.Weigth45.ToString();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("Weigth", receivedEvents[0]);
            Assert.AreEqual(Weigth.Weigth45.ToString(), searchWindowViewModel.SelectedWeigth);
        }

        [TestMethod()]
        public void OnLengthChangeTest()
        {
            searchWindowViewModel.SelectedLength = Length.Length45.ToString();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("LengthEnum", receivedEvents[0]);
            Assert.AreEqual(Length.Length45.ToString(), searchWindowViewModel.SelectedLength);
        }

        [TestMethod()]
        public void OnSearchBackChangeTest()
        {
            int value = 100;
            searchWindowViewModel.SearchBack = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("SearchBack", receivedEvents[0]);
            Assert.AreEqual(value, searchWindowViewModel.SearchBack);
        }

        [TestMethod()]
        public void GetEquipmentClassValuesTest()
        {
            var enums = searchWindowViewModel.EquipmentClassValues;
            Assert.IsNotNull(enums);
        }

        [TestMethod()]
        public void GetEquipmentTypeValuesTest()
        {
            var enums = searchWindowViewModel.EquipmentTypeValues;
            Assert.IsNotNull(enums);
        }

        [TestMethod()]
        public void GetProvinceValuesTest()
        {
            var enums = searchWindowViewModel.ProvinceValues;
            Assert.IsNotNull(enums);
        }

        [TestMethod()]
        public void GetFullOrPartialValuesTest()
        {
            var enums = searchWindowViewModel.FullOrPartialValues;
            Assert.IsNotNull(enums);
        }

        [TestMethod()]
        public void GetWeigthValuesTest()
        {
            var enums = searchWindowViewModel.WeigthValues;
            Assert.IsNotNull(enums);
        }

        [TestMethod()]
        public void GetLengthValuesTest()
        {
            var enums = searchWindowViewModel.LengthValues;
            Assert.IsNotNull(enums);
        }

        [TestMethod()]
        public void GetSelectedFullOrPartialTest()
        {
            var value = FullOrPartial.Both;
            searchWindowViewModel.SelectedFullOrPartial = value;
            Assert.AreEqual(value, searchWindowViewModel.SelectedFullOrPartial);
        }

        [TestMethod()]
        public void EquipmentClassSelectedTest()
        {
            var value = new ObservableCollection<EquipmentClass>() { EquipmentClass.DryBulk };
            searchWindowViewModel.EquipmentClassSelected = value;
            Assert.AreEqual(value, searchWindowViewModel.EquipmentClassSelected);
        }

        [TestMethod()]
        public void SaveDataFalseWhenBothProvinceAnyTest()
        {
            ICommand ic = searchWindowViewModel.CloseWindowSaveDataCommand;
            searchWindowViewModel.OriginProvinceSelect = StateProvince.Any;
            searchWindowViewModel.DestinationProvinceSelect = StateProvince.Any;
            ic.Execute(this);
            Assert.AreEqual(false, searchWindowViewModel.saveData);
        }

        [TestMethod()]
        public void SaveDataTrueWhenOriginProvinceAnyTest()
        {
            ICommand ic = searchWindowViewModel.CloseWindowSaveDataCommand;
            searchWindowViewModel.OriginProvinceSelect = StateProvince.Any;
            searchWindowViewModel.DestinationProvinceSelect = StateProvince.AR;
            ic.Execute(this);
            Assert.AreEqual(true, searchWindowViewModel.saveData);
        }

        [TestMethod()]
        public void SaveDataTrueWhenDestinationProvinceAnyTest()
        {
            ICommand ic = searchWindowViewModel.CloseWindowSaveDataCommand;
            searchWindowViewModel.OriginProvinceSelect = StateProvince.AR;
            searchWindowViewModel.DestinationProvinceSelect = StateProvince.Any;
            ic.Execute(this);
            Assert.AreEqual(true, searchWindowViewModel.saveData);
        }
    }
}