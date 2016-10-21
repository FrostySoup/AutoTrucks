using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.SendData;
using Moq;
using Service.ConnexionService;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class ConnectConnexionServiceTests
    {

        ConnectConnexionService connectConnexionService;
        Mock<ISessionFacade> session;

        [TestInitialize]
        public void SetInitialValues()
        {
            session = new Mock<ISessionFacade>();
            connectConnexionService = new ConnectConnexionService();
        }

        [TestMethod()]
        public void CheckIfValidLoginLoginFailTest()
        {
            string username = "Petter";
            string password = "Password";
            bool result = connectConnexionService.CheckIfValidLoginToConnexion(username, password);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckIfValidLoginSuccessTest()
        {
            string username = "TES7";
            string password = "teservices";
            bool result = connectConnexionService.CheckIfValidLoginToConnexion(username, password);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckIfValidLoginNullValuesTest()
        {
            string username = null;
            string password = null;
            bool result = connectConnexionService.CheckIfValidLoginToConnexion(username, password);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void TryToLoginInvalidDataTest()
        {
            string username = "zz";
            string password = "oz@";
            bool result = connectConnexionService.CheckIfValidLoginToConnexion(username, password);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void SearchConnexionWithCriteriaNullDataTest()
        {
            CreateSearchOperation searchOperationParams = new CreateSearchOperation();
            CreateSearchSuccessData results = connectConnexionService.SearchConnexion(session.Object, searchOperationParams);

            Assert.IsNull(results);
        }

        [TestMethod()]
        public void SearchConnexionWithNullDataTest()
        {
            CreateSearchOperation searchOperationParams = null;

            CreateSearchSuccessData results = connectConnexionService.SearchConnexion(session.Object, searchOperationParams);

            Assert.IsNull(results);
        }

        [TestMethod()]
        public void SearchConnexionWithSomeDataTest()
        {
            CreateSearchOperation searchOperationParams = new CreateSearchOperation();
            searchOperationParams.criteria = new SearchCriteria();
            session.Setup(x => x.Search(It.IsAny<CreateSearchRequest>())).Returns(new CreateSearchSuccessData());
            CreateSearchSuccessData results = connectConnexionService.SearchConnexion(session.Object, searchOperationParams);
            Assert.IsNotNull(results);
        }
    }
}