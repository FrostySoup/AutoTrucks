using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Service.ConnexionService;
using Service.SerializeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class SessionCacheSingletonTests
    {
        SessionCacheSingleton sessionCacheSingleton;

        Mock<ISerializeService> serializeService;
        Mock<IConnectConnexionService> connectConnexionService;
        Mock<ISessionFacade> sessionFacade;
        DataSource data;

        [TestInitialize]
        public void SetInitialValues()
        {
            serializeService = new Mock<ISerializeService>();
            connectConnexionService = new Mock<IConnectConnexionService>();
            sessionFacade = new Mock<ISessionFacade>();
            data = new DataSource()
            {
                UserName = "Hairy",
                Password = "Potter"
            };
            serializeService.Setup(x => x.ReturnDataSource()).Returns(new ObservableCollection<DataSource>() { data });

            connectConnexionService.Setup(x => x.LoginToConnexion(data.UserName, data.Password)).Returns(sessionFacade.Object);
            sessionCacheSingleton = new SessionCacheSingleton(serializeService.Object, connectConnexionService.Object);            
        }

        [TestMethod()]
        public void RenewSessionsForEachDataTest()
        {
            var currentSessions = sessionCacheSingleton.sessions;           
            sessionCacheSingleton.RenewSessionsForEachData();
            Assert.IsTrue(0 < sessionCacheSingleton.sessions.Count);
        }

        [TestMethod()]
        public void CheckIfSessionsCreatedSuccesfullyTest()
        {
            Assert.IsTrue(0 < sessionCacheSingleton.sessions.Count);
        }
    }
}