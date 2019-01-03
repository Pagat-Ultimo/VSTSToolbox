using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.VSTS
{
    public interface IVSTSServiceConfig
    {
        /// <summary>
        /// The Base URL
        /// </summary>
        string BaseUrl
        {
            get;
            set;
        }

        string PersonalAccessToken { get; set; }

        /// <summary>
        /// The EndPoint
        /// </summary>
        string PullRequestsPath
        {
            get;
        }
    }
}
