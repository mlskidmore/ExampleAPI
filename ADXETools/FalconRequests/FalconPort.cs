using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using ADXETools.Exceptions;
using System.Net;

namespace ADXETools.FalconRequests
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFalconPort
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspPage"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        Task<string> SubmitFalconRequest(string aspPage, string requestData);
    }

    /// <summary>
    /// 
    /// </summary>
    public class FalconPort : IFalconPort
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IEnvironmentConfiguration _environmentalConfiguration;

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="environmentalConfiguration"></param>
        public FalconPort(HttpClient httpClient, IEnvironmentConfiguration environmentalConfiguration)
        {
           _httpClient = httpClient;
           _httpClient.Timeout = TimeSpan.FromSeconds(300);
           _environmentalConfiguration = environmentalConfiguration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspPage"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public async Task<string> SubmitFalconRequest(string aspPage, string requestData)
        {
            try
            {
                var requestContent = new StringContent(requestData);

                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                Uri uri = new Uri(_environmentalConfiguration.FalconServiceUrl + "/" + aspPage);

                // Make a request and get a response
                return await SubmitFalconRequestNGetResponse(uri, requestContent);
            }
            catch (Exception ex)
            {
                ex.ToString(); // get rid of warning
                throw;
            }
        }
        #endregion Public Methods

        #region Private Methods
        private async Task<string> SubmitFalconRequestNGetResponse(Uri uri, HttpContent requestContent)
        {
            var requestResponse = await _httpClient.PostAsync(uri, requestContent);

            if (requestResponse.IsSuccessStatusCode)
            {
                var responseXml = await requestResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseXml))
                {
                    var xDoc = XDocument.Parse(responseXml);
                    CheckFalconResponse(xDoc);
                }
                else
                {
                    Console.WriteLine("Response from Falcon app is null or empty!");
                }

                return responseXml;
            }
            else
            {
                throw new HttpStatusException(requestResponse.StatusCode, requestResponse.ReasonPhrase, requestResponse.RequestMessage.ToString());
            }
        }

        private void CheckFalconResponse(XContainer xDoc)
        {
            XElement eleResponse = xDoc.Element("Response");
            if (eleResponse != null)
            {
                XAttribute attrError = eleResponse.Attribute("Error");
                XAttribute attrErrorDesc = eleResponse.Attribute("Desc");
                if (attrError != null && attrErrorDesc != null)
                {
                    var errorNum = attrError.Value;
                    var errorDesc = attrErrorDesc.Value;
                    if (Convert.ToInt32(errorNum) != 0)
                    {
                        throw new HttpStatusException($"Falcon app response error code:{ errorNum }, description: { errorDesc }");
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(errorNum) || !string.IsNullOrWhiteSpace(errorDesc))
                        {
                            throw new HttpStatusException($"Falcon app response error code:{ errorNum }, description: { errorDesc }");
                        }
                    }
                }
            }
        }
        #endregion Private Methods
    }
}