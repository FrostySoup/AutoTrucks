using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Moq;
using ViewModels.MainWindowViewModels;
using System.Windows.Input;
using System.ComponentModel;

namespace ViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class MainWindowViewModelTests
    {
        Mock<ITopButtonsViewModel> topButtonsViewModel;
        Mock<IMainWindowDisplayViewModel> postLoadsViewModel;
        Mock<IMainWindowDisplayViewModel> postTrucksViewModel;
        Mock<IMainWindowDisplayViewModel> searchLoadsViewModel;
        Mock<IMainWindowDisplayViewModel> searchTrucksViewModel;
        MainWindowViewModel mainWindowViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            topButtonsViewModel = new Mock<ITopButtonsViewModel>();
            postLoadsViewModel = new Mock<IMainWindowDisplayViewModel>();
            postTrucksViewModel = new Mock<IMainWindowDisplayViewModel>();
            searchLoadsViewModel = new Mock<IMainWindowDisplayViewModel>();
            searchTrucksViewModel = new Mock<IMainWindowDisplayViewModel>();

            mainWindowViewModel = new MainWindowViewModel(topButtonsViewModel.Object, postLoadsViewModel.Object, postTrucksViewModel.Object,
                searchLoadsViewModel.Object, searchTrucksViewModel.Object);
        }

        [TestMethod()]
        public void MainWindowViewModelChangePostTrucksViewModelCommandTest()
        {
            ICommand ic = mainWindowViewModel.ChangePostTrucksViewModelCommand;
            ic.Execute(this);
        }

        [TestMethod()]
        public void MainWindowViewModelChangePostLoadsViewModelCommandTest()
        {
            ICommand ic = mainWindowViewModel.ChangePostLoadsViewModelCommand;
            ic.Execute(this);
        }

        [TestMethod()]
        public void OnSearchTruckViewModelChangeTest()
        {
            List<string> receivedEvents = new List<string>();
            mainWindowViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
            mainWindowViewModel.SearchTrucksViewModel = searchTrucksViewModel.Object;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("SearchTrucksViewModel", receivedEvents[0]);
            Assert.AreEqual(searchTrucksViewModel.Object, mainWindowViewModel.SearchTrucksViewModel);
        }

        [TestMethod()]
        public void OnPostTrucksViewModelChangeTest()
        {
            List<string> receivedEvents = new List<string>();
            mainWindowViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
            mainWindowViewModel.PostTrucksViewModel = postTrucksViewModel.Object;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("PostTrucksViewModel", receivedEvents[0]);
            Assert.AreEqual(postTrucksViewModel.Object, mainWindowViewModel.PostTrucksViewModel);
        }

        [TestMethod()]
        public void OnPostLoadsViewModelChangeTest()
        {
            List<string> receivedEvents = new List<string>();
            mainWindowViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
            mainWindowViewModel.PostLoadsViewModel = postLoadsViewModel.Object;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("PostLoadsViewModel", receivedEvents[0]);
            Assert.AreEqual(postLoadsViewModel.Object, mainWindowViewModel.PostLoadsViewModel);
        }

        [TestMethod()]
        public void OnSearchLoadsViewModelChangeTest()
        {
            List<string> receivedEvents = new List<string>();
            mainWindowViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
            mainWindowViewModel.SearchLoadsViewModel = searchLoadsViewModel.Object;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("SearchLoadsViewModel", receivedEvents[0]);
            Assert.AreEqual(searchLoadsViewModel.Object, mainWindowViewModel.SearchLoadsViewModel);
        }

        [TestMethod()]
        public void OnTopButtonsViewModelChangeTest()
        {
            List<string> receivedEvents = new List<string>();
            mainWindowViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
            mainWindowViewModel.TopButtonsViewModel = topButtonsViewModel.Object;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("TopButtonsViewModel", receivedEvents[0]);
            Assert.AreEqual(topButtonsViewModel.Object, mainWindowViewModel.TopButtonsViewModel);
        }
    }
}