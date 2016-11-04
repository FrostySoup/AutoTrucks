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

namespace Service.ConnexionService
{
    public class SessionCacheSingleton : ISessionCacheSingleton
    {
        public Uri defaultURL { get; private set; }
        public List<ISessionFacade> sessions { get; private set; }

        private ObservableCollection<DataSource> dataSources;

        private ISerializeService serializeService;

        private IConnectConnexionService connectConnexionService;

        private Uri BuildAlarmUrl()
        {
            string host = "";
            string path = "/AlarmMatch";
            int port = 1010;
            var uriBuilder = new UriBuilder
            {
                Scheme = "http",
                Host = host,
                Path = path,
                Port = port
            };
            Uri uri;
            if (!TryUri(uriBuilder, out uri))
            {
                IPAddress ipAddress;
                if (host != null && IPAddress.TryParse(host, out ipAddress)) { }
                else
                {
                    string hostName = Dns.GetHostName();
                    IPAddress[] addresses = Dns.GetHostAddresses(hostName);
                    ipAddress = addresses.First(x => x.AddressFamily == AddressFamily.InterNetwork);
                }
                uriBuilder.Host = "88.119.98.103";
                uri = uriBuilder.Uri;
            }
            return uri;
        }

        private static bool TryUri(UriBuilder builder, out Uri uri)
        {
            bool wellFormed = false;
            try
            {
                uri = builder.Uri;
                wellFormed = true;
            }
            catch (UriFormatException)
            {
                uri = null;
            }
            return wellFormed;
        }

        public SessionCacheSingleton(ISerializeService serializeService, IConnectConnexionService connectConnexionService)
        {
            defaultURL = BuildAlarmUrl();
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
                    sessionFacade.UpdateAlarmUrl(defaultURL);
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
    }
}
