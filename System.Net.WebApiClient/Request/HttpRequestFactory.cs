using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebApiClient.Extensions;
using System.Net.WebApiClient.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Default imlemenetation of the IHttpRequestFactory.
    /// Creates a HttpRequestMessage by the current configuration to be sent.
    /// </summary>
    public class HttpRequestFactory : IHttpRequestFactory
    {
        /// <summary>
        /// The GzipEncoding header value.
        /// </summary>
        protected const string GzipEncoding = "gzip";

        /// <summary>
        /// The Deflate header value.
        /// </summary>
        protected const string DeflateEncoding = "deflate";

        /// <summary>
        /// Specifies if the http communication must be gzipped.
        /// </summary>
        public bool UseGzip { get; }

        /// <summary>
        /// Specifies a base url for all requests.
        /// </summary>
        public Uri BaseUri { get; }

        /// <summary>
        /// Deafault constructor of the HttpRequestFactory.
        /// </summary>
        /// <param name="useGzip">Specifies if the http communication must be gzipped.</param>
        /// <param name="baseUri">Specifies a base url for all requests.</param>
        public HttpRequestFactory(bool useGzip = true, Uri baseUri = default)
        {
            if (baseUri != default)
            {
                if (!baseUri.IsAbsoluteUri)
                    throw new ArgumentException("The BaseUri must be absolute!");
            }

            UseGzip = useGzip;
            BaseUri = baseUri;
        }

        /// <summary>
        /// Creates a new HttpRequestMessage instance for the outgoint request.
        /// </summary>
        /// <param name="serializer">The seralizer to be used for the requests content.</param>
        /// <param name="request">The requests parameteres.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The HttpRequest instance to be sent.</returns>
        public virtual async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(IHttpContentSerializer serializer, HttpRequestBase request, CancellationToken cancellationToken = default)
        {
            var requestMessage = new HttpRequestMessage(request.Method, await CreateRequestUriAsync(BaseUri, request.RequestUri, request.QueryParameters));

            if (request is HttpRequestWithContent requestWithContent && requestWithContent.Content != null)
            {
                requestMessage.Content = await serializer.SerializeRequestContentAsync(requestWithContent.Content, cancellationToken);
            }

            if (UseGzip)
            {
                requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(GzipEncoding));
                requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(DeflateEncoding));
            }

            foreach (var header in await serializer.CreateAcceptCharsetHeaderValuesAsync())
                requestMessage.Headers.AcceptCharset.Add(header);

            foreach (var header in await serializer.CreateAcceptHeaderValuesAsync())
                requestMessage.Headers.Accept.Add(header);

            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    var headerValue = header.Value.StringValue;

                    if (!string.IsNullOrEmpty(headerValue))
                        requestMessage.Headers.Add(header.Key, headerValue);
                }
            }

            return requestMessage;
        }

        /// <summary>
        /// Creates an aboulute Uri for the outgoing request, including an optional baseUri and a query string.
        /// </summary>
        /// <param name="baseUri">The optional baseUri.</param>
        /// <param name="requestUri">The absolute or relative uri of the request.</param>
        /// <param name="queryParameters">The optional query parameters.</param>
        /// <returns>The Uri for the HttpRequestMessage.</returns>
        public virtual Task<Uri> CreateRequestUriAsync(Uri baseUri, Uri requestUri, object queryParameters = null)
        {
            //Ha még nem abszolút és van baseUri akkor kombináljuk a base-t a kérés url-jével. 
            if (!requestUri.IsAbsoluteUri && baseUri != default)
                requestUri = new Uri(Path.Combine(baseUri.ToString(), requestUri.ToString().Trim('/').Trim('\\')));

            if (queryParameters != null)
            {
                var urlParams = queryParameters.ToQueryString();
                requestUri = new Uri($"{requestUri}?{urlParams}");
            }

            return Task.FromResult(requestUri);
        }
    }
}
