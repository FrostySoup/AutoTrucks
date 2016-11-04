
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService
{
    /// <summary>
	/// 	Represents the state and behavior of a user's session with the TFMI service, in a simplified interface.
	/// </summary>
	/// <remarks>
	/// 	The object state includes both session state objects ( <see cref="ApplicationHeader" /> , <see cref="CorrelationHeader" /> , and <see
	///  	cref="SessionHeader" /> ) and a client proxy (a <see cref="TfmiFreightMatchingPortTypeClient" /> instance) to the TFMI service. The behavior is simply to wrap the various methods of the TFMI service, simplifying the interface so that the repetitive bookkeeping objects (the session state objects and the servic eclient itself) can be hidden implementation details. For more details on the Facade design pattern, see <seealso
	///  	cref="http://en.wikipedia.org/wiki/Facade_pattern" /> .
	/// </remarks>
	public class SessionFacade : ISessionFacade
    {
        private readonly ApplicationHeader _applicationHeader;
        private readonly CorrelationHeader _correlationHeader;
        private readonly SessionHeader _sessionHeader;
        private readonly TfmiFreightMatchingPortTypeClient _client;

        public SessionFacade(ApplicationHeader applicationHeader,
            CorrelationHeader correlationHeader,
            LoginSuccessData loginSuccessData,
            TfmiFreightMatchingPortTypeClient client)
        {
            _applicationHeader = applicationHeader;
            _correlationHeader = correlationHeader;
            _sessionHeader = BuildSessionHeader(loginSuccessData);
            _client = client;
        }

        public Uri EndpointUrl
        {
            get { return _client.Endpoint.Address.Uri; }
        }

        public string PostNewAsset(PostAssetRequest postAssetRequest)
        {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            PostAssetResponse postAssetResponse;
            _client.PostAsset(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                postAssetRequest,
                out warningHeader,
                out postAssetResponse);

            if (postAssetResponse != null)
            {
                var data = postAssetResponse.postAssetResults;
                if (data == null)
                {
                    var serviceError = postAssetResponse.postAssetResults[0].Item as ServiceError;
                }
                else
                {
                    var item = data[0].Item as PostAssetSuccessData;
                    if (item == null)
                        return null;
                    return item.assetId;
                }
            }
            return null;
        }

        public Data DeleteAssetsById(DeleteAssetRequest deleteAssetRequest)
        {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            DeleteAssetResponse deleteAssetResponse;
            _client.DeleteAsset(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                deleteAssetRequest,
                out warningHeader,
                out deleteAssetResponse);


            if (deleteAssetResponse != null)
            {
                var data = deleteAssetResponse.deleteAssetResult.Item;
                if (data == null)
                {
                    var serviceError = deleteAssetResponse.deleteAssetResult.Item as ServiceError;
                }
                else
                {
                    return data;
                }
            }
            return null;
        }



        /// <summary>
        /// 	Calls <see cref="TfmiFreightMatchingPortTypeClient.CreateSearch" /> method and writes result to console.
        /// </summary>
        /// <param name="searchRequest"> </param>
        public CreateSearchSuccessData Search(CreateSearchRequest searchRequest)
        {
            if (searchRequest == null || searchRequest.createSearchOperation == null || searchRequest.createSearchOperation.criteria == null)
                return null;
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            CreateSearchResponse createSearchResponse;
            _client.CreateSearch(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                searchRequest,
                out warningHeader,
                out createSearchResponse);


            if (createSearchResponse != null)
            {
                var data = createSearchResponse.createSearchResult.Item as CreateSearchSuccessData;
                if (data == null)
                {
                    var serviceError = createSearchResponse.createSearchResult.Item as ServiceError;
                }
                else
                {
                    return data;
                }
            }
            return null;
        }

        public LookupAssetSuccessData QueryAllMyAssets(LookupAssetRequest lookupRequest)
        {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            LookupAssetResponse createLookupResponse;
            _client.LookupAsset(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                lookupRequest,
                out warningHeader,
                out createLookupResponse);


            if (createLookupResponse != null)
            {
                var data = createLookupResponse.lookupAssetResult.Item as LookupAssetSuccessData;
                if (data == null)
                {
                    var serviceError = createLookupResponse.lookupAssetResult.Item as ServiceError;
                }
                else
                {
                    return data;
                }
            }
            return null;
        }

       public bool[] CheckUserCapabilities(LookupCapabilitiesRequest lookupCapabilietiesRequest)
       {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            LookupCapabilitiesResponse lookupCapabilitiesResponse;
            _client.LookupCapabilities(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                lookupCapabilietiesRequest,
                out warningHeader,
                out lookupCapabilitiesResponse);


            if (lookupCapabilitiesResponse != null)
            {
                var data = lookupCapabilitiesResponse.lookupCapabilitiesResult.Item as LookupCapabilitiesSuccessData;
                if (data == null)
                {
                    var serviceError = lookupCapabilitiesResponse.lookupCapabilitiesResult.Item as ServiceError;
                }
                else
                {
                    return data.capabilityPresent;
                }
            }
            return null;
        }

        public Alarm CreateNewAlert(CreateAlarmRequest createAlarmRequest)
        {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            CreateAlarmResponse createAlarmResponse;
            _client.CreateAlarm(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                createAlarmRequest,
                out warningHeader,
                out createAlarmResponse);


            if (createAlarmResponse != null)
            {
                var data = createAlarmResponse.createAlarmResult.Item as CreateAlarmSuccessData;
                if (data == null)
                {
                    var serviceError = createAlarmResponse.createAlarmResult.Item as ServiceError;
                }
                else
                {
                    return data.alarm;
                }
            }
            return null;
        }

        public LookupAlarmSuccessData QueryAllAlarms(LookupAlarmRequest createAlarmRequest)
        {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            LookupAlarmResponse lookupAlarmResponse;
            _client.LookupAlarm(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                createAlarmRequest,
                out warningHeader,
                out lookupAlarmResponse);


            if (lookupAlarmResponse != null)
            {
                var data = lookupAlarmResponse.lookupAlarmResult.Item as LookupAlarmSuccessData;
                if (data == null)
                {
                    var serviceError = lookupAlarmResponse.lookupAlarmResult.Item as ServiceError;
                }
                else
                {
                    return data;
                }
            }
            return null;
        }

        public void UpdateAlarmUrl(Uri alarmUrl)
        {
            
            UpdateAlarmUrlRequest request = new UpdateAlarmUrlRequest()
            {
                updateAlarmUrlOperation = new UpdateAlarmUrlOperation()
                {
                    alarmUrl = alarmUrl.ToString()+"/"
                }
            };

            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            UpdateAlarmUrlResponse updateAlarmUrlResponse;
            _client.UpdateAlarmUrl(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                request,
                out warningHeader,
                out updateAlarmUrlResponse);


            if (updateAlarmUrlResponse != null)
            {
                var data = updateAlarmUrlResponse.updateAlarmUrlResult.Item as UpdateAlarmUrlSuccessData;
                if (data == null)
                {
                    var serviceError = updateAlarmUrlResponse.updateAlarmUrlResult.Item as ServiceError;
                }
            }
        }

        public LookupAlarmUrlSuccessData LookupCurrentAlarmUrl(LookupAlarmUrlRequest lookupAlarmUrlRequest)
        {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            LookupAlarmUrlResponse lookupAlarmResponse;
            _client.LookupAlarmUrl(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                lookupAlarmUrlRequest,
                out warningHeader,
                out lookupAlarmResponse);


            if (lookupAlarmResponse != null)
            {
                var data = lookupAlarmResponse.lookupAlarmUrlResult.Item as LookupAlarmUrlSuccessData;
                if (data == null)
                {
                    var serviceError = lookupAlarmResponse.lookupAlarmUrlResult.Item as ServiceError;
                }
                else return data;
            }
            return null;
        }

        /// <summary>
        /// 	Session header needs to be formed from the service's response, not from the ref input parameter of type <see
        ///  	cref="SessionHeader" /> .
        /// </summary>
        /// <param name="data"> </param>
        /// <returns> A <see cref="SessionHeader" /> built from properties of the input. </returns>
        private static SessionHeader BuildSessionHeader(LoginSuccessData data)
        {
            return new SessionHeader
            {
                sessionToken =
                           new SessionToken
                           {
                               expiration = data.expiration,
                               primary = data.token.primary,
                               secondary = data.token.secondary,
                               expirationSpecified = true
                           }
            };
        }
        
    }
}
