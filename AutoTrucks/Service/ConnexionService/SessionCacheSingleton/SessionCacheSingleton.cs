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

        private ISerializeService serializeService;

        private IConnectConnexionService connectConnexionService;

        public SessionCacheSingleton(ISerializeService serializeService, IConnectConnexionService connectConnexionService)
        {
            sessions = new List<ISessionFacade>();
            this.serializeService = serializeService;
            this.connectConnexionService = connectConnexionService;
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

        //public static SessionCacheSingleton Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            //SessionCacheSingleton(serializeService, connectConnexionService);
        //        }
        //        return instance;
        //    }
        //}
    }
}
