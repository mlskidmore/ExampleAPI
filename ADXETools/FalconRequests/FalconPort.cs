using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

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
                throw new Exception(ex.Message, ex);
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
                    Console.WriteLine("Response from Falcon app is null or empty string!");
                }

                return responseXml;
            }
            else
            {
                string err = string.Format("Status code:{0}, request message: {1}", requestResponse.StatusCode.ToString(), requestResponse.RequestMessage.ToString());
                throw new Exception(err);
            }
        }

        private void CheckFalconResponse(XContainer xDoc)
        {
            XElement eleResponse = xDoc.Element("Response");
            if (eleResponse != null)
            {
                XAttribute attrError = eleResponse.Attribute("Error");
                XAttribute attrErrorDesc = eleResponse.Attribute("Desc");
                if ((attrError != null) && (attrErrorDesc != null))
                {
                    var errorNum = attrError.Value.ToString();
                    var errorDesc = attrErrorDesc.Value.ToString();
                    string err = String.Empty;
                    if (Convert.ToInt32(errorNum) != 0)
                    {
                        err = string.Format("Falcon app response error number:{0}, description: {1}", errorNum, errorDesc);
                        throw new Exception(err);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(errorDesc))
                        {
                            err = string.Format("Falcon app response error number:{0}, description: {1}", errorNum, errorDesc);
                            throw new Exception(err);
                        }
                    }
                }
            }
        }
        #endregion Private Methods
    }
}