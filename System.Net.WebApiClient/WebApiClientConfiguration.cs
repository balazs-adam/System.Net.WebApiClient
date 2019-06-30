using System.Net.WebApiClient.ErrorHandling;
using System.Net.WebApiClient.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebApiClient.Request;
using System.Net.WebApiClient.Response;
using Newtonsoft.Json;

namespace System.Net.WebApiClient
{
    /// <summary>
    /// Handles all the configuration options of the WebApiClient instance.
    /// </summary>
    public class WebApiClientConfiguration
    {
        /// <summary>
        /// The IHttpContentSerializer to be used for all Http requests and responses.
        /// </summary>
        public IHttpContentSerializer Serializer { get; set; }

        /// <summary>
        /// The IWebApiClientErrorHandler to be used for the error handling of the Http responses.
        /// </summary>
        public IWebApiClientErrorHandler ErrorHandler { get; set; }

        /// <summary>
        /// The IHttpRequestFactory to be used for creating the HttpRequestMessage instance for each request.
        /// </summary>
        public IHttpRequestFactory RequestFactory { get; set; }

        /// <summary>
        /// The IHttpResponseFactory to be used for creating the response object from each HttpResponseMessage.
        /// </summary>
        public IHttpResponseFactory ResponseFactory { get; set; }

        /// <summary>
        /// Creates a default WebApiClientConfiguration.
        /// </summary>
        public static WebApiClientConfiguration Default
        {
            get
            {
                return new WebApiClientConfiguration
                {
                    Serializer = new JsonHttpContentSerializer(),
                    ErrorHandler = new WebApiClientErrorHandler(),
                    RequestFactory = new HttpRequestFactory(),
                    ResponseFactory = new HttpResponseFactory()
                };
            }
        }
    }
}
