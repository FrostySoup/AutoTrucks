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
        /*
        public void DeleteAllAssets()
        {
            var deleteAssetOperation = new DeleteAssetOperation { Item = new DeleteAllMyAssets() };
            var deleteAssetRequest = new DeleteAssetRequest { deleteAssetOperation = deleteAssetOperation };

           
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

            Console.WriteLine("[deleting any previous assets]");
        }

        public void LookupDobCarriersByCarrierId(string carrierId, int indent = 0)
        {
            var operation = new LookupDobCarriersOperation { carrierId = carrierId };
            var request = new LookupDobCarriersRequest { lookupDobCarriersOperations = operation };
            LookupDobCarriers(request, MethodBase.GetCurrentMethod().Name, indent);
        }

        public void LookupDobEvents(DateTime since)
        {
            var operation = new LookupDobEventsOperation { sinceDate = since };
            var request = new LookupDobEventsRequest { lookupDobEventsOperations = operation };

            LookupDobEvents(request);
        }

        public void LookupSignedCarriers()
        {
            var operation = new LookupDobSignedCarriersOperation();
            var request = new LookupDobSignedCarriersRequest { lookupDobSignedCarriersOperations = operation };

            LookupSignedCarriers(request);
        }

        public void LookupCarrierByDotNumber(int dotNumber)
        {
            LookupCarrierRequest lookupCarrierRequest = BuildLookupCarrierRequest(dotNumber, ItemChoiceType2.dotNumber);
            LookupCarrier(lookupCarrierRequest, MethodBase.GetCurrentMethod().Name);
        }

        public void LookupCarrierByMcNumber(int mcNumber)
        {
            LookupCarrierRequest lookupCarrierRequest = BuildLookupCarrierRequest(mcNumber, ItemChoiceType2.mcNumber);
            LookupCarrier(lookupCarrierRequest, MethodBase.GetCurrentMethod().Name);
        }

        public void LookupCarrierByUserId(int userId)
        {
            LookupCarrierRequest lookupCarrierRequest = BuildLookupCarrierRequest(userId, ItemChoiceType2.userId);
            LookupCarrier(lookupCarrierRequest, MethodBase.GetCurrentMethod().Name);
        }*/

        /// <summary>
        /// 	Calls <see cref="TfmiFreightMatchingPortTypeClient.PostAsset" /> method and writes result to console.
        /// </summary>
        /// <param name="postAssetRequest"> </param>
        /*
        public void Post(PostAssetRequest postAssetRequest)
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

            Console.WriteLine("===============Post Results===============");

            if (postAssetResponse != null)
            {
                foreach (PostAssetResult postAssetResult in postAssetResponse.postAssetResults)
                {
                    var postAssetSuccessData = postAssetResult.Item as PostAssetSuccessData;
                    if (postAssetSuccessData == null)
                    {
                        var serviceError = postAssetResult.Item as ServiceError;
                    }
                    else
                    {

                    }
                }
            }
        }*/

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
        /*
        public void UpdateAlarm(string alarmUrl)
        {
            var updateAlarmUrlRequest = new UpdateAlarmUrlRequest
            { updateAlarmUrlOperation = new UpdateAlarmUrlOperation { alarmUrl = alarmUrl } };

            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            UpdateAlarmUrlResponse updateAlarmUrlResponse;
            _client.UpdateAlarmUrl(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                updateAlarmUrlRequest,
                out warningHeader,
                out updateAlarmUrlResponse);

            if (updateAlarmUrlResponse != null)
            {
                Data item = updateAlarmUrlResponse.updateAlarmUrlResult.Item;
                var data = item as UpdateAlarmUrlSuccessData;
                if (data == null)
                {
                    var serviceError = item as ServiceError;
                }
                else
                {

                }
            }
        }*/
        /*
        private void LookupCarrier(LookupCarrierRequest lookupCarrierRequest, string description)
        {

            CorrelationHeader correlationHeader = _correlationHeader;

            SessionHeader sessionHeader = _sessionHeader;
            WarningHeader warningHeader;
            LookupCarrierResponse lookupCarrierResponse;
            _client.LookupCarrier(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                lookupCarrierRequest,
                out warningHeader,
                out lookupCarrierResponse);

            Console.WriteLine(description + ":");

            if (lookupCarrierResponse != null)
            {
                LookupCarrierResult lookupCarrierResult = lookupCarrierResponse.lookupCarrierResult[0];
                var data = lookupCarrierResult.Item as LookupCarrierSuccessData;
                if (data == null)
                {
                    var serviceError = lookupCarrierResult.Item as ServiceError;

                }
                else
                {

                }
            }
        }*/
        /*
        private void LookupDobCarriers(LookupDobCarriersRequest lookupDobCarriersRequest, string description, int indent)
        {

            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            LookupDobCarriersResponse lookupDobCarriersResponse;
            _client.LookupDobCarriers(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                lookupDobCarriersRequest,
                out warningHeader,
                out lookupDobCarriersResponse);

            if (indent == 0)
            {
                Console.WriteLine(description + ":");
            }
            if (lookupDobCarriersResponse != null)
            {
                LookupDobCarriersResult result = lookupDobCarriersResponse.lookupDobCarriersResults;
                var data = result.Item as LookupDobCarriersSuccessData;
                if (data == null)
                {
                    var serviceError = result.Item as ServiceError;
                }
                else
                {

                }
            }
        }
        */
        /*
        private void LookupDobEvents(LookupDobEventsRequest request)
        {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            LookupDobEventsResponse lookupDobEventsResponse;
            _client.lookupDobEvents(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                request,
                out warningHeader,
                out lookupDobEventsResponse);

            if (lookupDobEventsResponse != null)
            {
                LookupDobEventsResult result = lookupDobEventsResponse.lookupDobEventsResults;
                var data = result.Item as LookupDobEventsSuccessData;
                if (data == null)
                {
                    var serviceError = result.Item as ServiceError;


                }
                else
                {

                }
            }
        }*/
        /*
        private void LookupSignedCarriers(LookupDobSignedCarriersRequest request)
        {
            CorrelationHeader correlationHeader = _correlationHeader;
            SessionHeader sessionHeader = _sessionHeader;

            WarningHeader warningHeader;
            LookupDobSignedCarriersResponse signedCarriersResponse;
            _client.LookupDobSignedCarriers(_applicationHeader,
                ref correlationHeader,
                ref sessionHeader,
                request,
                out warningHeader,
                out signedCarriersResponse);

            if (signedCarriersResponse != null)
            {
                LookupDobSignedCarriersResult result = signedCarriersResponse.lookupDobSignedCarriersResults;
                var data = result.Item as LookupDobSignedCarriersSuccessData;
                if (data == null)
                {
                    var serviceError = result.Item as ServiceError;
                

                }
                else
                {

                }
            }

        }

        private static LookupCarrierRequest BuildLookupCarrierRequest(object item, ItemChoiceType2 itemElementName)
        {
            var lookupCarrierOperation = new LookupCarrierOperation
            {
                Item = item,
                ItemElementName = itemElementName,
                includeDotAuthority = true,
                includeDotAuthoritySpecified = true,
                includeDotInsurance = true,
                includeDotInsuranceSpecified = true,
                includeDotProfile = true,
                includeDotProfileSpecified = true,
                includeFmcsaCrashes = true,
                includeFmcsaCrashesSpecified = true,
                includeFmcsaInspections = true,
                includeFmcsaInspectionsSpecified = true,
                includeFmcsaSafeStat = true,
                includeFmcsaSafeStatSpecified = true,
                includeFmcsaSafetyRating = true,
                includeFmcsaSafetyRatingSpecified = true,
                includeCsa2010Basic = true,
                includeCsa2010BasicSpecified = true,
                includeCsa2010SafetyFitness = true,
                includeCsa2010SafetyFitnessSpecified = true,
                includeExtendedProfile = true,
                includeExtendedProfileSpecified = true
            };
            var lookupCarrierRequest = new LookupCarrierRequest { lookupCarrierOperation = new[] { lookupCarrierOperation } };
            return lookupCarrierRequest;
        }*/

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
