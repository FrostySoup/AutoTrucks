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

namespace Service.ConnexionService
{
    public partial class ConnectConnexionService : IConnectConnexionService
    {
        private string URL = "http://www.transcoreservices.com:8000/TfmiRequest";

        public bool CheckIfValidLoginToConnexion(string username, string password)
        {
            ISessionFacade session = LoginToConnexion(username, password);

            if (session != null)
                return true;
            return false;
        }

        public string PostNewAsset(ISessionFacade session, PostAssetOperation item)
        {
            PostAssetRequest postAssetRequest = new PostAssetRequest()
            {
                postAssetOperations = new PostAssetOperation[] { item }
            };
            return session.PostNewAsset(postAssetRequest);
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

        public CreateSearchSuccessData SearchConnexion(ISessionFacade session, CreateSearchOperation searchDataProvided)
        {
            CreateSearchRequest searchRequest = MapSearchOperationWithCreateSearchOperation(searchDataProvided);

            return session.Search(searchRequest);
        }

        public LookupAssetSuccessData QueryAllMyAssets(ISessionFacade session)
        {
            LookupAssetRequest lookupAssetRequest = new LookupAssetRequest()
            {
                lookupAssetOperation = new LookupAssetOperation()
                {
                    Item = new QueryAllMyAssets()
                }
            };
            return session.QueryAllMyAssets(lookupAssetRequest);
        }

        public LookupAssetSuccessData QueryAllMyGroupAssets(ISessionFacade sessionFacade)
        {
            LookupAssetRequest lookupAssetRequest = new LookupAssetRequest()
            {
                lookupAssetOperation = new LookupAssetOperation()
                {
                    Item = new QueryAllMyGroupsAssets()
                }
            };
            return sessionFacade.QueryAllMyAssets(lookupAssetRequest);
        }

        public Data DeleteAssetsById(ISessionFacade session, string[] Ids)
        {
            DeleteAssetRequest deleteAssetRequest = new DeleteAssetRequest()
            {
                deleteAssetOperation = new DeleteAssetOperation()
                {
                    Item = new DeleteAssetsByAssetIds()
                    {
                        assetIds = Ids
                    }
                }
            };
            return session.DeleteAssetsById(deleteAssetRequest);
        }  

        private CreateSearchRequest MapSearchOperationWithCreateSearchOperation(CreateSearchOperation searchDataProvided)
        {
            CreateSearchRequest searchRequest = new CreateSearchRequest();

            if (searchDataProvided == null || searchDataProvided.criteria == null)
                return null;

            searchRequest = new CreateSearchRequest()
            {
                createSearchOperation = searchDataProvided
            };

            return searchRequest;
        }

        public bool[] RetrieveUserCapabilities(ISessionFacade session, CapabilityType[] capabilities)
        {
            LookupCapabilitiesRequest lookupCapabilietiesRequest = new LookupCapabilitiesRequest()
            {
                lookupCapabilitiesOperation = new LookupCapabilitiesOperation()
                {
                    capability = capabilities
                }
            };
            return session.CheckUserCapabilities(lookupCapabilietiesRequest);
        }

        public Alarm CreateAlarm(ISessionFacade session, string assetID, AlarmSearchCriteria receivedCriteria)
        {
            CreateAlarmRequest createAlarmRequest = new CreateAlarmRequest()
            {
                createAlarmOperation = new CreateAlarmOperation()
                {
                    assetId = assetID,
                    criteria = receivedCriteria
                }
            };
            return session.CreateNewAlert(createAlarmRequest);
        }

        public LookupAlarmSuccessData QueryAllMyAlarms(ISessionFacade session)
        {
            LookupAlarmRequest createAlarmRequest = new LookupAlarmRequest()
            {
                lookupAlarmOperation = new LookupAlarmOperation()
                {
                    Item = new QueryAllMyAlarms()
                }
            };
            return session.QueryAllAlarms(createAlarmRequest);
        }

        public LookupAlarmSuccessData QueryAllMyByIdAlarms(ISessionFacade session, string[] alarmsIds)
        {
            LookupAlarmRequest createAlarmRequest = new LookupAlarmRequest()
            {
                lookupAlarmOperation = new LookupAlarmOperation()
                {
                    Item = new QueryAlarmsByAlarmIds()
                    {
                        alarmIds = alarmsIds
                    }
                }
            };
            return session.QueryAllAlarms(createAlarmRequest);
        }

        public LookupAlarmSuccessData QueryAllMyGroupAlarms(ISessionFacade session)
        {
            LookupAlarmRequest createAlarmRequest = new LookupAlarmRequest()
            {
                lookupAlarmOperation = new LookupAlarmOperation()
                {
                    Item = new QueryAllMyGroupsAlarms()
                }
            };
            return session.QueryAllAlarms(createAlarmRequest);
        }

        public LookupAlarmUrlSuccessData LookupAlarmUrl(ISessionFacade session)
        {
            LookupAlarmUrlRequest lookupAlarmUrlRequest = new LookupAlarmUrlRequest()
            {
                lookupAlarmUrlOperation = new LookupAlarmUrlOperation()
            };

            return session.LookupCurrentAlarmUrl(lookupAlarmUrlRequest);
        }

        public bool DeleteAlarms(List<string> ids, ISessionFacade session)
        {
            if (ids != null && ids.Count > 0)
            {
                DeleteAlarmRequest deleteAlarmRequest = new DeleteAlarmRequest()
                {
                    deleteAlarmOperation = new DeleteAlarmOperation()
                    {
                        Item = new DeleteAlarmsByAlarmIds()
                        {
                            alarmIds = ids.ToArray()
                        }
                    }
                };
                return session.DeleteAlarms(deleteAlarmRequest);
            }
            else
            {
                DeleteAlarmRequest deleteAlarmRequest = new DeleteAlarmRequest()
                {
                    deleteAlarmOperation = new DeleteAlarmOperation()
                    {
                        Item = new DeleteAllMyAlarms()
                    }
                };
                return session.DeleteAlarms(deleteAlarmRequest);
            }
        }      
    }
}
