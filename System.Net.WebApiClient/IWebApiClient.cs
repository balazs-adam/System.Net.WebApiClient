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
        Task<HttpResponseMessage> SendRequestMessageAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default);
        Task<HttpResponse<T>> SendRequestAsync<T>(HttpRequest request, CancellationToken cancellationToken = default);
        Task<HttpResponse> SendRequestAsync(HttpRequest request, CancellationToken cancellationToken = default);
        Task<HttpResponse<T>> SendGetRequestAsync<T>(HttpGetRequest request, CancellationToken cancellationToken = default);
        Task<HttpResponse> SendGetRequestAsync(HttpGetRequest request, CancellationToken cancellationToken = default);
        Task<HttpResponse<T>> SendPostRequestAsync<T>(HttpPostRequest request, CancellationToken cancellationToken = default);
        Task<HttpResponse> SendPostRequestAsync(HttpPostRequest request, CancellationToken cancellationToken = default);
        Task<HttpResponse<T>> SendPutRequestAsync<T>(HttpPutRequest request, CancellationToken cancellationToken = default);
        Task<HttpResponse> SendPutRequestAsync(HttpPutRequest request, CancellationToken cancellationToken = default);
        Task<HttpResponse<T>> SendDeleteRequestAsync<T>(HttpDeleteRequest request, CancellationToken cancellationToken = default);
    }
}
