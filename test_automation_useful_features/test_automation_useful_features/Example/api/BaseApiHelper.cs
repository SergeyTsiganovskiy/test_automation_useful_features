using System.Collections.Generic;
using ExaLinkRebatesAndFees.Utility.CommonHelpers;
using RestSharp;

namespace test_automation_useful_features.Helpers.Examples.API
{
    public abstract class BaseApiHelper
    {
        protected ApiCallsClient apiCallsClient;
        protected readonly Dictionary<string, string> xClient;

        protected BaseApiHelper(ApiCallsClient apiCallsClient, Dictionary<string, string> xClient)
        {
            this.apiCallsClient = apiCallsClient;
            this.xClient = xClient;
        }

        protected (IRestResponse, T2) RfmcSendPutRequest<T1, T2>(string apiPath, T1 requestModel = null) where T1 : class
        {
            var endPoint = apiCallsClient.GetEndPoint(apiPath);
            var requestBody = requestModel == null ? null : JsonHelper.SerializeHandlingNullValues(requestModel);
            var response = apiCallsClient.SendPutRequest(endPoint, xClient, requestBody);
            JsonHelper.TryParseJson(response.Content, out T2 responseModel);
            return (response, responseModel);
        }

        protected (IRestResponse, T2) RfmcSendGetRequest<T2>(string apiPath)
        {
            var endPoint = apiCallsClient.GetEndPoint(apiPath);
            var response = apiCallsClient.SendGetRequest(endPoint, xClient);
            JsonHelper.TryParseJson(response.Content, out T2 responseModel);
            return (response, responseModel);
        }
    }
}