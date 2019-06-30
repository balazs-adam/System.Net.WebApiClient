using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Defines a base class for every HTTP requests.
    /// </summary>
    public abstract class HttpRequestBase
    {
        /// <summary>
        /// The absoute or relative (to the BaseUrl defined in the HttpClientConfiguration) url of the request.
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Optional query parameters for the request.
        /// </summary>
        public object QueryParameters { get; set; }

        /// <summary>
        /// The HTTP headers to attach to the request.
        /// </summary>
        public Dictionary<string, HttpHeaderValue> Headers { get; set; } = new Dictionary<string, HttpHeaderValue>();

        /// <summary>
        /// The HTTP method of the request.
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// Default constructor of the HttpRequestBase.
        /// </summary>
        /// <param name="method">The method of the Http request.</param>
        public HttpRequestBase(HttpMethod method)
        {
            Method = method;
        }
    }
}
