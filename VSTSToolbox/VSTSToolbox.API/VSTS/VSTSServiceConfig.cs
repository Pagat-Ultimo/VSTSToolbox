using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.VSTS
{
    public class VSTSServiceConfig : IVSTSServiceConfig
    {
        public string BaseUrl { get; set; } = "";
        public string PersonalAccessToken { get; set; } = "";
        public string PullRequestsPath { get; } = "pullrequests";
    }
}
