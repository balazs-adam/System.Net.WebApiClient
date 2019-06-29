using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebApiClient.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Response
{
    public interface IHttpResponseFactory
    {
        Task<HttpResponse> CreateResponseAsync(IHttpContentSerializer serializer, HttpResponseMessage responseMessage, CancellationToken cancellationToken = default);
        Task<HttpResponse<T>> CreateResponseAsync<T>(IHttpContentSerializer serializer, HttpResponseMessage responseMessage, CancellationToken cancellationToken = default);
    }
}
