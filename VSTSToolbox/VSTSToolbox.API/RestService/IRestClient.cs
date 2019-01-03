using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VSTSToolbox.API.RestService
{
    public interface IRestClient
    {
        #region Public abstract methods

        /// <summary>
        /// Makes a call with the http GET method type to a specific ressource
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<RestResponse> GetAsync(string url, Dictionary<string, string> headers = null, CancellationToken? ct = null);
        /// <summary>
        /// Makes a call with the http POST method type to a specific ressource
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postContent"></param>
        /// <param name="headers"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<RestResponse> PostAsync(string url, HttpContent postContent, Dictionary<string, string> headers = null, CancellationToken? ct = null);

        /// <summary>
        /// Initializes the client
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="headers"></param>
        void SetUp(string baseUrl, Dictionary<string, string> headers = null);


        void SetAuthHeader(AuthenticationHeaderValue auth);

        #endregion
    }
}
