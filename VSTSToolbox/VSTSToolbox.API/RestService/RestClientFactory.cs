using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.RestService
{
    public class RestClientFactory : IRestClientFactory
    {
        public IRestClient CreateRestClient()
        {
            return new RestClient();
        }
    }
}
