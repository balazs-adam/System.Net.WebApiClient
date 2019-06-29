using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebApiClient.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Response
{
    public class HttpResponseFactory : IHttpResponseFactory
    {
        public virtual Task<HttpResponse> CreateResponseAsync(IHttpContentSerializer serializer, HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new HttpResponse(responseMessage.StatusCode));
        }

        public virtual async Task<HttpResponse<T>> CreateResponseAsync<T>(IHttpContentSerializer serializer, HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
        {
            return new HttpResponse<T>
            (
                responseMessage.StatusCode,
                await serializer.DeserializeResponseContentAsync<T>(responseMessage.Content, cancellationToken)
            );
        }
    }
}
