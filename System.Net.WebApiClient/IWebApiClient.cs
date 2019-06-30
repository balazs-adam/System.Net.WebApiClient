using System.Net.WebApiClient.Request;
using System.Net.WebApiClient.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient
{
    /// <summary>
    /// Defines a type safe client for all HTTP related communications.
    /// </summary>
    public interface IWebApClient
    {
        /// <summary>
        /// Initializes the current implementation.
        /// </summary>
        /// <param name="configuration">The current configuration to use.</param>
        /// <returns></returns>
        Task InitializeAsync(WebApiClientConfiguration configuration);

        /// <summary>
        /// Sends a raw HttpRequestMessage and returns the raw HttpResponseMessage recieved for it. 
        /// </summary>
        /// <param name="httpRequestMessage">The request message to be sent.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The recieved HttpResponseMessage.</returns>
        Task<HttpResponseMessage> SendRequestMessageAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send a HTTP request based on the HttpRequest and returns a parsed HttpResponse recieved for it. 
        /// </summary>
        /// <typeparam name="T">The type of the Http response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        Task<HttpResponse<T>> SendRequestAsync<T>(HttpRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send a void HTTP request based on the HttpRequest and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        Task<HttpResponse> SendRequestAsync(HttpRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a HTTP GET message and returns a parsed HttpResponse recieved for it.
        /// </summary>
        /// <typeparam name="T">The type of the Http response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        Task<HttpResponse<T>> SendGetRequestAsync<T>(HttpGetRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a HTTP GET message and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        Task<HttpResponse> SendGetRequestAsync(HttpGetRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a HTTP POST message and returns a parsed HttpResponse recieved for it.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        Task<HttpResponse<T>> SendPostRequestAsync<T>(HttpPostRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a HTTP POST message and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        Task<HttpResponse> SendPostRequestAsync(HttpPostRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a HTTP PUT message and returns a parsed HttpResponse recieved for it.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        Task<HttpResponse<T>> SendPutRequestAsync<T>(HttpPutRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a HTTP PUT message and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        Task<HttpResponse> SendPutRequestAsync(HttpPutRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a HTTP DELETE message and returns a parsed HttpResponse recieved for it.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response's content.</typeparam>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The parsed HttpResponse.</returns>
        Task<HttpResponse<T>> SendDeleteRequestAsync<T>(HttpDeleteRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a HTTP DELETE message and returns the non parsed HttpResponse recieved for it. 
        /// </summary>
        /// <param name="request">The HTTP request's parameters.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The non-parsed HttpResponse.</returns>
        Task<HttpResponse> SendDeleteRequestAsync(HttpDeleteRequest request, CancellationToken cancellationToken = default);
    }
}
