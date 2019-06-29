using System.Net.Http;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Defines a DELETE HTTP request.
    /// </summary>
    public class HttpDeleteRequest : HttpRequestBase
    {
        public HttpDeleteRequest() : base(HttpMethod.Delete)
        {
        }
    }
}