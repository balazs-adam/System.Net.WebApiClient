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
    public class HttpRequestFactory : IHttpRequestFactory
    {
        protected const string GzipEncoding = "gzip";
        protected const string DeflateEncoding = "deflate";

        private bool _useGzip;
        private Uri _baseUri;

        public HttpRequestFactory(bool useGzip = false, Uri baseUri = default)
        {
            if (baseUri != default)
            {
                if (!baseUri.IsAbsoluteUri)
                    throw new ArgumentException("The BaseUri must be absolute!");
            }

            _useGzip = useGzip;
            _baseUri = baseUri;
        }

        public virtual async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(IHttpContentSerializer serializer, HttpRequestBase request, CancellationToken cancellationToken = default)
        {
            var requestMessage = new HttpRequestMessage(request.Method, await CreateRequestUriAsync(_baseUri, request.RequestUri, request.QueryParameters));

            if (request is HttpRequestWithContent requestWithContent && requestWithContent.Content != null)
            {
                requestMessage.Content = await serializer.SerializeRequestContentAsync(requestWithContent.Content, cancellationToken);
            }

            if (_useGzip)
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
