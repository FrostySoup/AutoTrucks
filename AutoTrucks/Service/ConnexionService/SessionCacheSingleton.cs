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
    public class SessionCacheSingleton
    {
        public List<SessionFacade> sessions;

        private ObservableCollection<DataSource> dataSources;

        private static SessionCacheSingleton instance;
        private SessionCacheSingleton()
        {
            sessions = new List<SessionFacade>();
            dataSources = SerializeServiceSingleton.Instance.ReturnDataSource();
            CreateSessionsForEachData();
        }

        private void CreateSessionsForEachData()
        {
            foreach(DataSource data in dataSources)
            {
                SessionFacade sessionFacade = ConnectConnexionServiceSingleton.Instance.LoginToConnexion(data.UserName, data.Password);
                if (sessionFacade != null)
                {
                    sessions.Add(sessionFacade);
                }
            }
        }

        public void RenewSessionsForEachData()
        {
            dataSources = SerializeServiceSingleton.Instance.ReturnDataSource();
            sessions = new List<SessionFacade>();
            foreach (DataSource data in dataSources)
            {
                SessionFacade sessionFacade = ConnectConnexionServiceSingleton.Instance.LoginToConnexion(data.UserName, data.Password);
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
