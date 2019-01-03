using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace VSTSToolbox.API.RestService
{
    public class RestClient : IRestClient
    {
        #region Instance fields

        private readonly HttpClient client;

        #endregion

        #region Constructors

        public RestClient()
        {
            this.client = new HttpClient();
        }

        #endregion

        #region IRestClient Members

        public void SetUp(string baseUrl, Dictionary<string, string> headers = null)
        {
            if (!string.IsNullOrEmpty(baseUrl))
                this.client.BaseAddress = new Uri(baseUrl);

            this.client.DefaultRequestHeaders.Clear();
            if (headers != null)
            {
                foreach (string headerName in headers.Keys)
                {
                    this.client.DefaultRequestHeaders.Add(headerName, headers[headerName]);
                }
            }
        }

        public void SetAuthHeader(AuthenticationHeaderValue auth)
        {
            this.client.DefaultRequestHeaders.Authorization = auth;
        }

        public async Task<RestResponse> GetAsync(string url, Dictionary<string, string> headers = null,
            CancellationToken? ct = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(url, UriKind.Relative),
                Method = HttpMethod.Get
            };

            if (headers != null)
            {
                foreach (string headerName in headers.Keys)
                {
                    requestMessage.Headers.Add(headerName, headers[headerName]);
                }
            }

            HttpResponseMessage response;
            try
            {
                response = await this.client.SendAsync(requestMessage, ct ?? CancellationToken.None);
            }
            catch (Exception ex)
            {
                return RestResponse.FromException(ex);
            }

            return await ParseResponse(response);
        }

        public async Task<RestResponse> PostAsync(string url, HttpContent postContent,
            Dictionary<string, string> headers = null, CancellationToken? ct = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(url, UriKind.Relative),
                Method = HttpMethod.Post,
                Content = postContent
            };

            if (headers != null)
            {
                foreach (string headerName in headers.Keys)
                {
                    requestMessage.Headers.Add(headerName, headers[headerName]);
                }
            }

            HttpResponseMessage response;
            try
            {
                response = await this.client.SendAsync(requestMessage, ct ?? CancellationToken.None);
            }
            catch (Exception ex)
            {
                return RestResponse.FromException(ex);
            }

            return await ParseResponse(response);
        }

        #endregion

        #region Private methods

        private async Task<RestResponse> ParseResponse(HttpResponseMessage response)
        {
            if (response == null)
                return new RestResponse(HttpStatusCode.NotFound, false, "no response");

            string responseBody = await response.Content.ReadAsStringAsync();
            return new RestResponse(response.StatusCode, response.IsSuccessStatusCode, responseBody);
        }

        #endregion
    }
}
