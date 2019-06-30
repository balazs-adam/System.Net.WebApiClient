using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebApiClient.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Response
{
    /// <summary>
    /// Creates and parses the HttpResponse object for the recieved HttpResponseMessage.
    /// </summary>
    public interface IHttpResponseFactory
    {
        /// <summary>
        /// Creates a HttpResponse object from the incoming HttpResponseMessage without parsing it's content.
        /// </summary>
        /// <param name="serializer">The IHttpContentSerializer to be used.</param>
        /// <param name="responseMessage">The recieved HttpResponseMessage to be parsed.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The HttpResponse instance.</returns>
        Task<HttpResponse> CreateResponseAsync(IHttpContentSerializer serializer, HttpResponseMessage responseMessage, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a HttpResponse object from the incoming HttpResponseMessage and parses it's content for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the parsed content.</typeparam>
        /// <param name="serializer">The IHttpContentSerializer to be used.</param>
        /// <param name="responseMessage">The recieved HttpResponseMessage to be parsed.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns>The HttpResponse instance.</returns>
        Task<HttpResponse<T>> CreateResponseAsync<T>(IHttpContentSerializer serializer, HttpResponseMessage responseMessage, CancellationToken cancellationToken = default);
    }
}
