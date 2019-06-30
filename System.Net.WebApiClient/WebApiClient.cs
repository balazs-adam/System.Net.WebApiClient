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
    public class WebApiClient : IWebApClient
    {

        private WebApiClientConfiguration _configuration;

        public WebApiClient()
        {
            _configuration = WebApiClientConfiguration.Default;
        }

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

        protected virtual Task<HttpClient> CreateUnderlyingHttpClientAsync()
        {
            return Task.FromResult(new HttpClient());
        }

        public virtual async Task<HttpResponseMessage> SendRequestMessageAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default)
        {
            using (var client = await CreateUnderlyingHttpClientAsync())
            {
                return await client.SendAsync(httpRequest, cancellationToken: cancellationToken);
            }
        }

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

        protected virtual Task HandleErrorsCoreAsync(HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
        {
            if (responseMessage.IsSuccessStatusCode)
                return Task.CompletedTask;

            return _configuration.ErrorHandler.HandleErrorsAsync(responseMessage, cancellationToken);
        }

        public Task<HttpResponse<T>> SendGetRequestAsync<T>(HttpGetRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);
        public Task<HttpResponse<T>> SendPostRequestAsync<T>(HttpPostRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);
        public Task<HttpResponse<T>> SendPutRequestAsync<T>(HttpPutRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);
        public Task<HttpResponse<T>> SendDeleteRequestAsync<T>(HttpDeleteRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);
        public virtual Task<HttpResponse<T>> SendRequestAsync<T>(HttpRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync<T>(request, cancellationToken);
        public Task<HttpResponse> SendGetRequestAsync(HttpGetRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);
        public Task<HttpResponse> SendPostRequestAsync(HttpPostRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);
        public Task<HttpResponse> SendPutRequestAsync(HttpPutRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);
        public Task<HttpResponse> SendDeleteRequestAsync(HttpDeleteRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);
        public virtual Task<HttpResponse> SendRequestAsync(HttpRequest request, CancellationToken cancellationToken = default)
            => SendRequestCoreAsync(request, cancellationToken);
    }
}
