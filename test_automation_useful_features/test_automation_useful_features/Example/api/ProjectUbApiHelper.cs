﻿using System.Collections.Generic;
using test_automation_useful_features.Helpers.Examples.API;

namespace test_automation_useful_features.Example.api
{
    public class ProjectUbApiHelper : BaseApiHelper
    {
        public ProjectUbApiHelper(ApiCallsClient apiCallsClient, Dictionary<string, string> xClient) : base(apiCallsClient, xClient) { }
        //public (IRestResponse, UploadFileToDocApiResponseModel) UploadFileToDocApi(string filePath, [Optional] Dictionary<string, object> additionalParameters)
        //{
        //    var endPoint = apiCallsClient.GetEndPoint(RfmcApiPaths.DocApiLoadsPath);
        //    var response = apiCallsClient.UploadFileWebRequest(Method.POST, endPoint, filePath, xClient, additionalParameters);
        //    JsonHelper.TryParseJson(response.Content, out UploadFileToDocApiResponseModel responseModel);
        //    return (response, responseModel);
        //}

        //public (IRestResponse, PutPriceGroupUuidToUpdateSVResponseApiModel) PutPriceGroupToUpdateSubmittedValues(string projectId, PutPriceGroupUuidToUpdateSVApiRequestModel requestModel)
        //{
        //    var apiPath = string.Format(RfmcApiPaths.SubmittedValuesTabPath, projectId);
        //    return RfmcSendPutRequest<PutPriceGroupUuidToUpdateSVApiRequestModel, PutPriceGroupUuidToUpdateSVResponseApiModel>(apiPath, requestModel);
        //}

        //public (IRestResponse, RevalidateOrRecalculateInitProjectResponseApiModel) RecalculateInitProject(string invoiceId)
        //{
        //    var apiPath = string.Format(RfmcApiPaths.RecalculateInitProjectPath, invoiceId);
        //    return RfmcSendPutRequest<object, RevalidateOrRecalculateInitProjectResponseApiModel>(apiPath);
        //}
    }
}