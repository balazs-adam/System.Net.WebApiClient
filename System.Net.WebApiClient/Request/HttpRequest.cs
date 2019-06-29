using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace System.Net.WebApiClient.Request
{
    public class HttpRequest : HttpRequestWithContent
    {
        public HttpRequest(HttpMethod method) : base(method)
        {

        }
    }
}
