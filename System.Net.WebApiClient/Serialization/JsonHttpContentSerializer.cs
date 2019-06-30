using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Serialization
{
    /// <summary>
    /// Provides a method to serialize the JSON content of the HTTP requests and deserialize the JSON content of the HTTP responses. 
    /// </summary>
    public class JsonHttpContentSerializer : IHttpContentSerializer
    {
        /// <summary>
        /// The ContentType header value to be placed in the requests.
        /// </summary>
        protected const string ContentTypeHeader = "application/json";

        /// <summary>
        /// The AcceptHeader header value to be placed in the requests.
        /// </summary>
        protected const string AcceptHeaderValue = "application/json";

        /// <summary>
        /// The AcceptCharsetHeader header value to be placed in the requests.
        /// </summary>
        protected const string AcceptCharsetHeaderValue = "utf-8";

        JsonSerializer _jsonSerializer;

        /// <summary>
        /// Default constructor of the JsonHttpContentSerializer.
        /// </summary>
        /// <param name="jsonSerializer">The json.Net JsonSerializer to be used for the instance.</param>
        public JsonHttpContentSerializer(JsonSerializer jsonSerializer = null)
        {
            _jsonSerializer = jsonSerializer ?? JsonSerializer.CreateDefault();
        }

        /// <summary>
        /// Lists the values that must be placed in the Accept request header.
        /// </summary>
        /// <returns>The header values.</returns>
        public virtual Task<List<MediaTypeWithQualityHeaderValue>> CreateAcceptHeaderValuesAsync()
        {
            return Task.FromResult(new List<MediaTypeWithQualityHeaderValue>
            {
                new MediaTypeWithQualityHeaderValue(AcceptHeaderValue)
            });
        }

        /// <summary>
        /// Lists the values that must be placed in the AcceptCharset request header. 
        /// </summary>
        /// <returns>The header values.</returns>
        public virtual Task<List<StringWithQualityHeaderValue>> CreateAcceptCharsetHeaderValuesAsync()
        {
            return Task.FromResult(new List<StringWithQualityHeaderValue>
            {
                new StringWithQualityHeaderValue(AcceptCharsetHeaderValue)
            });
        }

        /// <summary>
        /// Creates the JSON HttpContent for the request. 
        /// </summary>
        /// <param name="requestContent">The optional content of the request to be serialized.</param>
        /// <param name="cancellationToken">A cancellationToken to cancel this operation.</param>
        /// <returns>The HttpContent of the request.</returns>
        public virtual Task<HttpContent> SerializeRequestContentAsync(object requestContent, CancellationToken cancellationToken = default)
        {
            if (requestContent == null)
                throw new ArgumentNullException();
            var stream = new MemoryStream();

            using (var streamWriter = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                _jsonSerializer.Serialize(jsonWriter, requestContent);
            }

            stream.Seek(0, SeekOrigin.Begin);

            HttpContent content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentTypeHeader);

            return Task.FromResult(content);
        }

        /// <summary>
        /// Parses the JSON content of the HTTP response into the passed type.
        /// </summary>
        /// <typeparam name="T">The type of the response's content.</typeparam>
        /// <param name="content">The HttpContent of the response.</param>
        /// <param name="cancellationToken">A cancellationToken to cancel this operation.</param>
        /// <returns>The parsed content of the response.</returns>
        public virtual async Task<T> DeserializeResponseContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)
        {
            if (content == null)
                return default;

            if (typeof(T) == typeof(string))
                return (T)(object)await content.ReadAsStringAsync();

            using (var contentStream = await content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(contentStream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var result = _jsonSerializer.Deserialize<T>(jsonReader);

                if (result != null)
                    return result;

                return default;
            }
        }
    }
}
