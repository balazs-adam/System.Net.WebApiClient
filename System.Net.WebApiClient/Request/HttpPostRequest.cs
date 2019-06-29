using System.Net.Http;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Defines a POST HTTP request.
    /// </summary>
    public class HttpPostRequest : HttpRequestWithContent
    {
        public HttpPostRequest() : base(HttpMethod.Post)
        {
        }
    }
}