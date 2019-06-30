using System.Net.Http;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Defines a DELETE HTTP request.
    /// </summary>
    public class HttpDeleteRequest : HttpRequestBase
    {
        /// <summary>
        /// Default constructor of the HttpDeleteRequest.
        /// </summary>
        public HttpDeleteRequest() : base(HttpMethod.Delete)
        {
        }
    }
}