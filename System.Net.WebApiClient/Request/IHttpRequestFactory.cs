using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebApiClient.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Creates a HttpRequestMessage by the current configuration to be sent.
    /// </summary>
    public interface IHttpRequestFactory
    {
        /// <summary>
        /// Creates a new HttpRequestMessage instance for the outgoint request.
        /// </summary>
        /// <param name="serializer">The seralizer to be used for the requests content.</param>
        /// <param name="request">The requests parameteres.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The HttpRequest instance to be sent.</returns>
        Task<HttpRequestMessage> CreateHttpRequestMessageAsync(IHttpContentSerializer serializer, HttpRequestBase request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates an aboulute Uri for the outgoing request, including an optional baseUri and a query string.
        /// </summary>
        /// <param name="baseUri">The optional baseUri.</param>
        /// <param name="requestUri">The absolute or relative uri of the request.</param>
        /// <param name="queryParameters">The optional query parameters.</param>
        /// <returns>The Uri for the HttpRequestMessage.</returns>
        Task<Uri> CreateRequestUriAsync(Uri baseUri, Uri requestUri, object queryParameters = null);
    }
}
