using Model.ReceiveData.Login;
using Model.SendData;
using Service.FillDataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Model.SearchCRUD;

namespace Service.ConnexionService
{
    public class ConnectConnexionService : IConnectConnexionService
    {
        private string URL = "http://www.transcoreservices.com:8000/TfmiRequest";

        public bool CheckIfValidLoginToConnexion(string username, string password)
        {
            SessionFacade session = LoginToConnexion(username, password);

            if (session != null)
                return true;
            return false;
        }

        public SessionFacade LoginToConnexion(string user, string password)
        {
            var remoteAddress = new EndpointAddress(URL);
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None) { MaxReceivedMessageSize = 2 << 20 };
            var client = new TfmiFreightMatchingPortTypeClient(binding, remoteAddress);

            // build request
            var loginRequest = new LoginRequest
            {
                loginOperation = new LoginOperation { loginId = user, password = password, thirdPartyId = "SampleClient.NET" }
            };

            // build various headers required by the service method
            var applicationHeader = new ApplicationHeader
            { application = "Connexion C# .NET Test", applicationVersion = "1.0" };
            var correlationHeader = new CorrelationHeader();
            var sessionHeader = new SessionHeader
            { sessionToken = new SessionToken { primary = new byte[] { }, secondary = new byte[] { } } };

            // invoke the service
            WarningHeader warningHeader;
            LoginResponse loginResponse;
            client.Login(applicationHeader,
                         ref correlationHeader,
                         ref sessionHeader,
                         loginRequest,
                         out warningHeader,
                         out loginResponse);

            // return a SessionFacade, which wraps the login results along with the client object
            var data = loginResponse.loginResult.Item as LoginSuccessData;
            if (data == null)
            {
                var serviceError = loginResponse.loginResult.Item as ServiceError;
                return null;
            }
            return new SessionFacade(applicationHeader, correlationHeader, data, client);
        }

        public void SearchConnexion(SessionFacade session, CreateSearch searchDataProvided)
        {
            CreateSearchRequest searchRequest = new CreateSearchRequest()
            {
               createSearchOperation = new CreateSearchOperation()
               {
                   criteria = new SearchCriteria()
                   {
                       ageLimitMinutes = searchDataProvided.ageLimitMinutes,
                       assetType = (AssetType)searchDataProvided.criteria,
                       destination = new GeoCriteria()
                       {
                           Item = searchDataProvided.destination
                       },
                       origin = new GeoCriteria()
                       {
                           Item = searchDataProvided.origin
                       }
                   }
               } 
            };
            session.Search(searchRequest);
        }

        /*public async Task<string> LoginToConnexion()
        {
            string Token = "";

            HttpClient client = new HttpClient();

            // build client to TFMI service 
            var remoteAddress = new Uri(URL);

            // build request
            Login user = AddUserFactory.GetUser();

            ReceivedLogin loginTokens;

            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(URL, user);
            if (response.IsSuccessStatusCode)
            {
                loginTokens = await response.Content.ReadAsAsync<ReceivedLogin>();
            }

            return Token;
        }*/


    }
}
