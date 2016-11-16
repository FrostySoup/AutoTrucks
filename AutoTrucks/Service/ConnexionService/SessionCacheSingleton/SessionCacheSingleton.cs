using Model;
using Service.SerializeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model.DataFromView;
using Service.FirewallController;

namespace Service.ConnexionService
{
    public class SessionCacheSingleton : ISessionCacheSingleton
    {
        public List<ISessionFacade> sessions { get; private set; }

        public RemoteConnection remoteURI { get; private set; }

        private ObservableCollection<DataSource> dataSources;

        private readonly ISerializeService serializeService;

        private readonly IConnectConnexionService connectConnexionService;

        private readonly IFirewallControl firewallControl;

        public SessionCacheSingleton(ISerializeService serializeService, IConnectConnexionService connectConnexionService, IFirewallControl firewallControl)
        {
            sessions = new List<ISessionFacade>();
            this.serializeService = serializeService;
            this.firewallControl = firewallControl;
            this.connectConnexionService = connectConnexionService;
            dataSources = serializeService.ReturnDataSource();
            SetAlarmUri();
            CreateSessionsForEachData();
        }

        private void SetAlarmUri()
        {
            remoteURI = serializeService.DeserializeRemote();
            if (sessions.Count > 0)
                sessions[0].UpdateAlarmUrl(new Uri(string.Format("http://{0}:{1}/AlarmMatch", remoteURI.RemoteIp, remoteURI.Port)));
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

        public void UpdateAlarmAdress()
        {            
            SetAlarmUri();
            firewallControl.AddNewPortToFirewall(remoteURI.Port);
        }
    }
}
