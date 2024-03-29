﻿using System.Net.Http;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Defines a PUT HTTP request.
    /// </summary>
    public class HttpPutRequest : HttpRequestWithContent
    {
        /// <summary>
        /// Default constructor of the HttpPutRequest.
        /// </summary>
        public HttpPutRequest() : base(HttpMethod.Put)
        {
        }
    }
}