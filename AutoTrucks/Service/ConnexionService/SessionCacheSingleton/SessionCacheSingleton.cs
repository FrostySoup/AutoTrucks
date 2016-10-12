using Model;
using Service.SerializeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService
{
    public class SessionCacheSingleton : ISessionCacheSingleton
    {
        public List<ISessionFacade> sessions { get; private set; }

        private ObservableCollection<DataSource> dataSources;

        private static SessionCacheSingleton instance;

        private ISerializeService serializeService;

        private IConnectConnexionService connectConnexionService;

        private SessionCacheSingleton()
        {
            sessions = new List<ISessionFacade>();
            //Needs fix later
            serializeService = new SerializeService();
            connectConnexionService = new ConnectConnexionService();
            dataSources = serializeService.ReturnDataSource();
            CreateSessionsForEachData();
        }

        private void CreateSessionsForEachData()
        {
            foreach(DataSource data in dataSources)
            {
                ISessionFacade sessionFacade = connectConnexionService.LoginToConnexion(data.UserName, data.Password);
                if (sessionFacade != null)
                {
                    sessions.Add(sessionFacade);
                }
            }
        }

        public void RenewSessionsForEachData()
        {
            dataSources = serializeService.ReturnDataSource();
            sessions = new List<ISessionFacade>();
            foreach (DataSource data in dataSources)
            {
                ISessionFacade sessionFacade = connectConnexionService.LoginToConnexion(data.UserName, data.Password);
                if (sessionFacade != null)
                {
                    sessions.Add(sessionFacade);
                }
            }
        }

        public static SessionCacheSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionCacheSingleton();
                }
                return instance;
            }
        }
    }
}
