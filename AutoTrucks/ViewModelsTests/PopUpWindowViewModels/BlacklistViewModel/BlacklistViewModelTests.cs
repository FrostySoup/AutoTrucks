using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.PopUpWindowViewModels.BlacklistViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Service.SerializeServices;
using Service.AddNewWindowFactory;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Model.DataHelpers;

namespace ViewModels.PopUpWindowViewModels.BlacklistViewModel.Tests
{
    [TestClass()]
    public class BlacklistViewModelTests
    {
        Mock<ISerializeService> serializeService;
        Mock<IWindowFactory> windowFactory;

        BlacklistViewModel blacklistViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            serializeService = new Mock<ISerializeService>();
            windowFactory = new Mock<IWindowFactory>();
            blacklistViewModel = new BlacklistViewModel(windowFactory.Object, serializeService.Object);
        }

        [TestMethod()]
        public void RefreshBlacklistTest()
        {
            var initialList = blacklistViewModel.CompaniesCollection;
            serializeService.Setup(x => x.DeserializeCompanyName())
                .Returns(new List<string>() { "CompanyName1", "CompanyName2"});

            blacklistViewModel.RefreshBlacklist();
            var resultList = blacklistViewModel.CompaniesCollection;

            Assert.AreNotEqual(initialList, resultList);
            Assert.AreNotEqual(initialList.Count, resultList.Count);
        }

        [TestMethod()]
        public void DeleteSelectedCampaniesTest()
        {
            ICommand ic = blacklistViewModel.DeleteSelectedCompaniesCommand;
            ic.Execute(this);
            Assert.AreEqual(0, blacklistViewModel.CompaniesCollection.Count);
        }

    }
}