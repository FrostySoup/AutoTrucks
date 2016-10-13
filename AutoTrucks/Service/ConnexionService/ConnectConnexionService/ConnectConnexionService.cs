using Model.ReceiveData.Login;
using Model.SendData;
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
            ISessionFacade session = LoginToConnexion(username, password);

            if (session != null)
                return true;
            return false;
        }

        public ISessionFacade LoginToConnexion(string user, string password)
        {
            if (user == null || password == null)
                return null;
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

        public CreateSearchSuccessData SearchConnexion(ISessionFacade session, SearchOperationParams searchDataProvided)
        {
            CreateSearchRequest searchRequest = MapSearchOperationWithCreateSearchOperation(searchDataProvided);

            return session.Search(searchRequest);
        }

        private CreateSearchRequest MapSearchOperationWithCreateSearchOperation(SearchOperationParams searchDataProvided)
        {
            CreateSearchRequest searchRequest = new CreateSearchRequest();

            if (searchDataProvided.criteria == null)
                return null;

            searchRequest = new CreateSearchRequest()
            {
                createSearchOperation = new CreateSearchOperation()
                {
                    criteria = new SearchCriteria()
                    {
                        ageLimitMinutes = searchDataProvided.criteria.ageLimitMinutes,
                        ageLimitMinutesSpecified = searchDataProvided.criteria.ageLimitMinutesSpecified,
                        assetType = searchDataProvided.criteria.assetType,
                        destination = searchDataProvided.criteria.destination,
                        equipmentClasses = searchDataProvided.criteria.equipmentClasses,
                        excludeOpenDestinationEquipment = searchDataProvided.criteria.excludeOpenDestinationEquipment,
                        availability = searchDataProvided.criteria.availability,
                        origin = searchDataProvided.criteria.origin,
                        includeFulls = searchDataProvided.criteria.includeFulls,
                        excludeOpenDestinationEquipmentSpecified = searchDataProvided.criteria.excludeOpenDestinationEquipment,
                        limits = searchDataProvided.criteria.limits
                    },
                    includeSearch = searchDataProvided.includeSearch,
                    includeSearchSpecified = searchDataProvided.includeSearchSpecified,
                    sortOrder = searchDataProvided.sortOrder,
                    sortOrderSpecified = searchDataProvided.sortOrderSpecified
                }
            };

            return searchRequest;
        }


    }
}
