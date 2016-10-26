using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Service.AddNewWindowFactory;
using Service.ColorListHolder;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Model.Enums;

namespace ViewModels.PopUpWindowViewModels.PostWindowViewModel.Tests
{
    [TestClass()]
    public class PostWindowViewModelTests
    {

        Mock<IWindowFactory> windowFactory;
        Mock<IColorListHolder> colorListHolder;
        PostWindowViewModel postWindowViewModel;
        List<string> receivedEvents;

        [TestInitialize]
        public void SetInitialValues()
        {
            windowFactory = new Mock<IWindowFactory>();
            colorListHolder = new Mock<IColorListHolder>();
            postWindowViewModel = new PostWindowViewModel(windowFactory.Object, colorListHolder.Object);
            receivedEvents = new List<string>();
            postWindowViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
        }

        [TestMethod()]
        public void CloseWindowDontSaveCommandTest()
        {
            ICommand ic = postWindowViewModel.CloseWindowCommand;
            ic.Execute(this);
            Assert.AreEqual(false, postWindowViewModel.saveChanges);
        }

        [TestMethod()]
        public void CloseWindowSaveChangesCommandTest()
        {
            ICommand ic = postWindowViewModel.CloseWindowSaveDataCommand;
            postWindowViewModel.postData.originState = StateProvince.AG;
            ic.Execute(this);
            Assert.AreEqual(true, postWindowViewModel.saveChanges);
        }

        [TestMethod()]
        public void CloseWindowSaveChangesCommandDontSaveTest()
        {
            ICommand ic = postWindowViewModel.CloseWindowSaveDataCommand;
            postWindowViewModel.postData.originState = StateProvince.Any;
            postWindowViewModel.postData.destinationState = StateProvince.Any;
            ic.Execute(this);
            Assert.AreEqual(false, postWindowViewModel.saveChanges);
        }

        [TestMethod()]
        public void OnBackgroundColorChangeTest()
        {
            postWindowViewModel.BackgroundColor = new Color();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("BackgroundColor", receivedEvents[0]);
            Assert.AreEqual(new Color(), postWindowViewModel.BackgroundColor);
        }

        [TestMethod()]
        public void OnForegroundColorChangeTest()
        {
            postWindowViewModel.ForegroundColor = new Color();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("ForegroundColor", receivedEvents[0]);
            Assert.AreEqual(new Color(), postWindowViewModel.ForegroundColor);
        }

        [TestMethod()]
        public void OnAvailFromChangeTest()
        {
            DateTime time = DateTime.Now;
            postWindowViewModel.AvailFrom = time;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("AvailFrom", receivedEvents[0]);
            Assert.AreEqual(time, postWindowViewModel.AvailFrom);
        }

        [TestMethod()]
        public void OnAvailToChangeTest()
        {
            DateTime time = DateTime.Now;
            postWindowViewModel.AvailTo = time;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("AvailTo", receivedEvents[0]);
            Assert.AreEqual(time, postWindowViewModel.AvailTo);
        }

        [TestMethod()]
        public void OnEquipmentTypeSelectedTest()
        {
            EquipmentType equipment = new EquipmentType();
            postWindowViewModel.EquipmentTypeSelected = equipment;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("EquipmentTypeSelected", receivedEvents[0]);
            Assert.AreEqual(equipment, postWindowViewModel.EquipmentTypeSelected);
        }

        [TestMethod()]
        public void OnDestinationProvinceChangeTest()
        {
            postWindowViewModel.DestinationStateProvinceSelected = new StateProvince();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("DestinationStateProvinceSelected", receivedEvents[0]);
            Assert.AreEqual(new StateProvince(), postWindowViewModel.DestinationStateProvinceSelected);
        }

        [TestMethod()]
        public void OnOriginProvinceChangeTest()
        {
            postWindowViewModel.OriginStateProvinceSelected = new StateProvince();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("OriginStateProvinceSelected", receivedEvents[0]);
            Assert.AreEqual(new StateProvince(), postWindowViewModel.OriginStateProvinceSelected);
        }

        [TestMethod()]
        public void OnDHOChangeTest()
        {
            int value = 100;
            postWindowViewModel.DHO = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("DHO", receivedEvents[0]);
            Assert.AreEqual(value, postWindowViewModel.DHO);
        }

        [TestMethod()]
        public void OnDHDChangeTest()
        {
            int value = 100;
            postWindowViewModel.DHD = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("DHD", receivedEvents[0]);
            Assert.AreEqual(value, postWindowViewModel.DHD);
        }

        [TestMethod()]
        public void OnWeigthNumberTest()
        {
            int value = 100;
            postWindowViewModel.Weight = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("Weight", receivedEvents[0]);
            Assert.AreEqual(value, postWindowViewModel.Weight);
        }

        [TestMethod()]
        public void OnLengthNumberTest()
        {
            int value = 100;
            postWindowViewModel.Length = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("Length", receivedEvents[0]);
            Assert.AreEqual(value, postWindowViewModel.Length);
        }

        [TestMethod()]
        public void OnFullChangeTest()
        {
            postWindowViewModel.FullOrPartial = FullOrPartial.Full;
            Assert.AreEqual(true, postWindowViewModel.postData.includeFulls);
            Assert.AreEqual(false, postWindowViewModel.postData.includeLtls);
        }

        [TestMethod()]
        public void OnPartialChangeTest()
        {
            postWindowViewModel.FullOrPartial = FullOrPartial.Partial;
            Assert.AreEqual(false, postWindowViewModel.postData.includeFulls);
            Assert.AreEqual(true, postWindowViewModel.postData.includeLtls);
        }

        [TestMethod()]
        public void OnFullAndPartialChangeTest()
        {
            postWindowViewModel.FullOrPartial = FullOrPartial.Both;
            Assert.AreEqual(true, postWindowViewModel.postData.includeFulls);
            Assert.AreEqual(true, postWindowViewModel.postData.includeLtls);
            Assert.AreEqual(FullOrPartial.Both, postWindowViewModel.FullOrPartial);
        }

        [TestMethod()]
        public void OnCommentOneTest()
        {
            string value = "my comment";
            postWindowViewModel.CommentOne = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("CommentOne", receivedEvents[0]);
            Assert.AreEqual(value, postWindowViewModel.CommentOne);
        }

        [TestMethod()]
        public void OnCommentTwoTest()
        {
            string value = "my comment";
            postWindowViewModel.CommentTwo = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("CommentTwo", receivedEvents[0]);
            Assert.AreEqual(value, postWindowViewModel.CommentTwo);
        }

        [TestMethod()]
        public void CurrentMaxValue()
        {
            int value = 100;
            postWindowViewModel.CurrentMaxValue = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("CurrentMaxValue", receivedEvents[0]);
            Assert.AreEqual(value, postWindowViewModel.CurrentMaxValue);
        }

        [TestMethod()]
        public void CurrentMinValue()
        {
            int value = 100;
            postWindowViewModel.CurrentMinValue = value;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("CurrentMinValue", receivedEvents[0]);
            Assert.AreEqual(value, postWindowViewModel.CurrentMinValue);
        }

        [TestMethod()]
        public void UselessTestToCoverSimpleGets()
        {
            var stateProvinceValues = postWindowViewModel.StateProvinceValues;
            var fullOrPartialValues = postWindowViewModel.FullOrPartialValues;
            var equipmentTypeValues = postWindowViewModel.EquipmentTypeValues;
        }

    }
}