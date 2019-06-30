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
    public class WebApiClientConfiguration
    {
        public IHttpContentSerializer Serializer { get; set; }
        public IWebApiClientErrorHandler ErrorHandler { get; set; }
        public IHttpRequestFactory RequestFactory { get; set; }
        public IHttpResponseFactory ResponseFactory { get; set; }

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
