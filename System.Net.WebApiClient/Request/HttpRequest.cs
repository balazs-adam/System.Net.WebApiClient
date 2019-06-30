using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Defines a Http request.
    /// </summary>
    public class HttpRequest : HttpRequestWithContent
    {
        /// <summary>
        /// Default constructor of the HttpRequest class.
        /// </summary>
        /// <param name="method">The method of the http request message.</param>
        public HttpRequest(HttpMethod method) : base(method)
        {

        }
    }
}
