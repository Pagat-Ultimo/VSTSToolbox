using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VSTSToolbox.API.RestService
{
    public class RestResponse
    {
        #region Constructors

        public RestResponse(HttpStatusCode status, bool success, string body)
        {
            StatusCode = status;
            IsSuccessStatusCode = success;
            Body = body;
        }

        #endregion

        #region Properties

        public string Body
        {
            get;
            private set;
        }

        public Exception Exception
        {
            get;
            set;
        }

        public bool IsSuccessStatusCode
        {
            get;
            private set;
        }

        public HttpStatusCode StatusCode
        {
            get;
            private set;
        }

        #endregion

        #region Public static methods

        public static RestResponse FromException(Exception ex)
        {
            var response = new RestResponse(HttpStatusCode.ServiceUnavailable, false, "")
            {
                Exception = ex
            };

            return response;
        }

        #endregion
    }
}
