using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Http.Headers;
using System.Net.WebApiClient.Response;
using System.Net.WebApiClient.Request;
using System.IO;
using System.Net.WebApiClient.Extensions;

namespace System.Net.WebApiClient
{
    /// <summary>
    /// Default implementation of a type safe client for all HTTP related communications.
    /// </summary>
    public class WebApiClient : IWebApClient
    {
        /// <summary>
        /// The current WebApiClientConfiguration for this client.
        /// </summary>
        protected WebApiClientConfiguration _configuration;

        /// <summary>
        /// The default constructor of the WebApiClient.
        /// </summary>
        public WebApiClient()
        {
            _configuration = WebApiClientConfiguration.Default;
        }

        /// <summary>
        /// Initializes the current implementation.
        /// </summary>
        /// <param name="configuration">The current configuration to use.</param>
        /// <returns></returns>
        public virtual Task InitializeAsync(WebApiClientConfiguration configuration)
        {
            if (configuration.Serializer == null)
                throw new ArgumentNullException("Serializer must not be null!");

            if (configuration.ErrorHandler == null)
                throw new ArgumentNullException("ErrorHandler must not be null!");

            if (configuration.RequestFactory == null)
                throw new ArgumentNullException("RequestFactory must not be null!");

            if (configuration.ResponseFactory == null)
                throw new ArgumentNullException("ResponseFactory must not be null!");

            _configuration = configuration;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates a new HttpClient instance to be used for the underlying communication.
        /// </summary>
        /// <returns>The HttpClient instance.</returns>
        protected virtual Task<HttpClient> CreateUnderlyingHttpClientAsync()
        {
            if (_configuration.RequestFactory.UseGzip)
                return Task.FromResult(new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                }));

            return Task.FromResult(new HttpClient());
        }

        /// <summary>
        /// Sends a raw HttpRequestMessage and returns the raw HttpResponseMessage recieved for it. 
        /// </summary>
        /// <param name="httpRequestMessage">The request message to be sent.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The recieved HttpResponseMessage.</returns>
        public virtual async Task<HttpResponseMessage> SendRequestMessageAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default)
        {
            using (var client = await CreateUnderlyingHttpClientAsync())
            {
                return await client.SendAsync(httpRequestMessage, cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Send a HTTP request based on the HttpRequest and returns a parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        protected virtual async Task<HttpResponse> SendRequestCoreAsync(HttpRequestBase request, CancellationToken cancellationToken = default)
        {
            using (var client = await CreateUnderlyingHttpClientAsync())
            using (var httpRequest = await _configuration.RequestFactory.CreateHttpRequestMessageAsync(_configuration.Serializer, request))
            {
                using (var response = await SendRequestMessageAsync(httpRequest, cancellationToken))
                {
                    await HandleErrorsCoreAsync(response, cancellationToken);
                    return await _configuration.ResponseFactory.CreateResponseAsync(_configuration.Serializer, response);
                }
            }
        }

        /// <summary>
        /// Send a HTTP request based on the HttpRequest and returns a parsed HttpResponse recieved for it. 
        /// </summary>
        /// <typeparam name="T">The type of the Http response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        protected virtual async Task<HttpResponse<T>> SendRequestCoreAsync<T>(HttpRequestBase request, CancellationToken cancellationToken = default)
        {
            using (var client = await CreateUnderlyingHttpClientAsync())
            using (var httpRequest = await _configuration.RequestFactory
                .CreateHttpRequestMessageAsync(_configuration.Serializer, request))
            {
                using (var response = await SendRequestMessageAsync(httpRequest, cancellationToken))
                {
                    await HandleErrorsCoreAsync(response, cancellationToken);

                    return await _configuration.ResponseFactory.CreateResponseAsync<T>(_configuration.Serializer, response);
                }
            }
        }

        /// <summary>
        /// Handles any errors recieved for a HTTP request.
        /// </summary>
        /// <param name="responseMessage">The recieved HttpResponseMessage object.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns></returns>
        protected virtual Task HandleErrorsCoreAsync(HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
        {
            if (responseMessage.IsSuccessStatusCode)
                return Task.CompletedTask;

            return _configuration.ErrorHandler.HandleErrorsAsync(responseMessage, cancellationToken);
        }

        /// <summary>
        /// Send a HTTP request based on the HttpRequest and returns a parsed HttpResponse recieved for it. 
        /// </summary>
        /// <typeparam name="T">The type of the Http response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        public virtual Task<HttpResponse<T>> SendRequestAsync<T>(HttpRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);

        /// <summary>
        /// Send a void HTTP request based on the HttpRequest and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        public virtual Task<HttpResponse> SendRequestAsync(HttpRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);

        /// <summary>
        /// Sends a HTTP GET message and returns a parsed HttpResponse recieved for it.
        /// </summary>
        /// <typeparam name="T">The type of the Http response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        public Task<HttpResponse<T>> SendGetRequestAsync<T>(HttpGetRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);

        /// <summary>
        /// Sends a HTTP GET message and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        public Task<HttpResponse> SendGetRequestAsync(HttpGetRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);

        /// <summary>
        /// Sends a HTTP POST message and returns a parsed HttpResponse recieved for it.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        public Task<HttpResponse<T>> SendPostRequestAsync<T>(HttpPostRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);

        /// <summary>
        /// Sends a HTTP POST message and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        public Task<HttpResponse> SendPostRequestAsync(HttpPostRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);

        /// <summary>
        /// Sends a HTTP PUT message and returns a parsed HttpResponse recieved for it.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        public Task<HttpResponse<T>> SendPutRequestAsync<T>(HttpPutRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);

        /// <summary>
        /// Sends a HTTP PUT message and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        public Task<HttpResponse> SendPutRequestAsync(HttpPutRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);

        /// <summary>
        /// Sends a HTTP DELETE message and returns a parsed HttpResponse recieved for it.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        public Task<HttpResponse<T>> SendDeleteRequestAsync<T>(HttpDeleteRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);

        /// <summary>
        /// Sends a HTTP DELETE message and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        public Task<HttpResponse> SendDeleteRequestAsync(HttpDeleteRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);
    }
}
