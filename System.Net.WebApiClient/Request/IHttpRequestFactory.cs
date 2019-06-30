using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebApiClient.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Request
{
    public interface IHttpRequestFactory
    {
        Task<HttpRequestMessage> CreateHttpRequestMessageAsync(IHttpContentSerializer serializer, HttpRequestBase request, CancellationToken cancellationToken = default);
        Task<Uri> CreateRequestUriAsync(Uri baseUri, Uri requestUri, object queryParameters = null);
    }
}
