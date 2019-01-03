using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.RestService
{
    /// <summary>
    /// Factory used to support IoC for IRestClient with simple IoC while having a different instance of IRestClient for every service using it.
    /// </summary>
    public interface IRestClientFactory
    {
        /// <summary>
        /// Creates a new Instance of an IRestClientImplementation
        /// </summary>
        /// <returns></returns>
        IRestClient CreateRestClient();
    }
}
