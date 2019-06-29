using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace System.Net.WebApiClient.Response
{
    /// <summary>
    /// Defines a base class for a HTTP response message with no content. 
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// The HTTP status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }

    /// <summary>
    /// Defines a class for a HTTP response message with content.
    /// </summary>
    /// <typeparam name="T">A type of the HTTP response's content.</typeparam>
    public class HttpResponse<T> : HttpResponse
    {
        /// <summary>
        /// The parsed content of the HTTP response.
        /// </summary>
        public T Content { get; }

        public HttpResponse(HttpStatusCode statusCode, T content) : base(statusCode)
        {
            Content = content;
        }
    }
}
