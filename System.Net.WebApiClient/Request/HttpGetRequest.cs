using System.Net.Http;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Defines a GET HTTP request.
    /// </summary>
    public class HttpGetRequest : HttpRequestBase
    {
        public HttpGetRequest() : base(HttpMethod.Get)
        {
        }
    }
}