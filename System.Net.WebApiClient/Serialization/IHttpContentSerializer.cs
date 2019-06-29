using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Serialization
{
    /// <summary>
    /// Provides a method to serialize the content of the HTTP requests and deserialize the content of the HTTP responses. 
    /// </summary>
    public interface IHttpContentSerializer
    {
        /// <summary>
        /// Lists the values that must be placed in the Accept request header.
        /// </summary>
        /// <returns>The header values.</returns>
        Task<List<MediaTypeWithQualityHeaderValue>> CreateAcceptHeaderValuesAsync();

        /// <summary>
        /// Lists the values that must be placed in the AcceptCharset request header. 
        /// </summary>
        /// <returns>The header values.</returns>
        Task<List<StringWithQualityHeaderValue>> CreateAcceptCharsetHeaderValuesAsync();

        /// <summary>
        /// Creates the HttpContent for the request. 
        /// </summary>
        /// <param name="requestContent">The optional content of the request to be serialized.</param>
        /// <param name="cancellationToken">A cancellationToken to cancel this operation.</param>
        /// <returns>The HttpContent of the request.</returns>
        Task<HttpContent> SerializeRequestContentAsync(object requestContent, CancellationToken cancellationToken = default);

        /// <summary>
        /// Parses the content of the HTTP response into the passed type.
        /// </summary>
        /// <typeparam name="T">The type of the response's content.</typeparam>
        /// <param name="responseContent">The HttpContent of the response.</param>
        /// <param name="cancellationToken">A cancellationToken to cancel this operation.</param>
        /// <returns>The parsed content of the response.</returns>
        Task<T> DeserializeResponseContentAsync<T>(HttpContent responseContent, CancellationToken cancellationToken = default);
    }
}
