using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebApiClient.Request;
using System.Net.WebApiClient.Serialization;
using System.Net.WebApiClient.Test.Model;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Net.WebApiClient.Test.Request
{
    public class HttpRequestFactoryTests : HttpRequestFactoryTestBase
    {
        protected RequestConfiguration RequestConfiguration =>
            WebApiClientConfiguration.Default.RequestConfiguration;
        protected IHttpContentSerializer HttpContentSerializer =>
            WebApiClientConfiguration.Default.Serializer;

        [Fact]
        public async Task CreateHttpRequestMessageAsync_Get()
        {
            var factory = CreateHttpRequestFactory();

            var request = new HttpRequest(Http.HttpMethod.Get)
            {
                RequestUri = new Uri("https://index.hu/api/test"),
                QueryParameters = new { Param1 = 1, Param2 = "teszt" }
            };

            var requestMessage = await factory
                .CreateHttpRequestMessageAsync(RequestConfiguration, HttpContentSerializer, request);

            Assert.NotNull(requestMessage);
            Assert.Equal(request.Method, requestMessage.Method);
            Assert.Equal(new Uri("https://index.hu/api/test?Param1=1&Param2=teszt"), requestMessage.RequestUri);

            Assert.Single(requestMessage.Headers.Accept);
            Assert.Single(requestMessage.Headers.AcceptCharset);
            Assert.Empty(requestMessage.Headers.AcceptEncoding);

            Assert.Null(requestMessage.Content);
        }

        [Fact]
        public async Task CreateHttpRequestMessageAsync_Post()
        {
            var factory = CreateHttpRequestFactory();

            var request = new HttpRequest(Http.HttpMethod.Post)
            {
                RequestUri = new Uri("https://index.hu/api/test"),
                Content = new Person
                {
                    Age = 18,
                    DateOfBirth = DateTime.Now,
                    Name = "John Smith",
                }
            };

            var requestMessage = await factory
                .CreateHttpRequestMessageAsync(RequestConfiguration, HttpContentSerializer, request);

            Assert.NotNull(requestMessage);
            Assert.Equal(request.Method, requestMessage.Method);
            Assert.Equal(new Uri("https://index.hu/api/test"), requestMessage.RequestUri);

            Assert.Single(requestMessage.Headers.Accept);
            Assert.Single(requestMessage.Headers.AcceptCharset);
            Assert.Empty(requestMessage.Headers.AcceptEncoding);

            Assert.NotNull(requestMessage.Content);
        }

        [Fact]
        public async Task CreateHttpRequestMessageAsync_Gzip()
        {
            var factory = CreateHttpRequestFactory();

            var request = new HttpRequest(Http.HttpMethod.Get)
            {
                RequestUri = new Uri("https://index.hu/api/test")
            };

            var requestConfiguration = RequestConfiguration;
            requestConfiguration.UseGzip = true;

            var requestMessage = await factory
                .CreateHttpRequestMessageAsync(requestConfiguration, HttpContentSerializer, request);

            Assert.NotNull(requestMessage);
            Assert.Equal(request.Method, requestMessage.Method);
            Assert.Equal(new Uri("https://index.hu/api/test"), requestMessage.RequestUri);

            Assert.Single(requestMessage.Headers.Accept);
            Assert.Single(requestMessage.Headers.AcceptCharset);

            Assert.Contains("gzip", requestMessage.Headers.AcceptEncoding.Select(h => h.Value).ToList());
            Assert.Contains("deflate", requestMessage.Headers.AcceptEncoding.Select(h => h.Value).ToList());

            Assert.Null(requestMessage.Content);
        }

        [Fact]
        public async Task CreateHttpRequestMessageAsync_CustomHeader()
        {
            var factory = CreateHttpRequestFactory();

            var apiKeyHeaderKey = "apikey";
            var countHeaderKey = "count";

            var apiKeyHeaderValue = "asdfg";
            var countHeaderValue = 3;

            var request = new HttpRequest(Http.HttpMethod.Get)
            {
                RequestUri = new Uri("https://index.hu/api/test"),
                Headers = new Dictionary<string, HttpHeaderValue>
                {
                    {apiKeyHeaderKey, new HttpHeaderValue<string>(apiKeyHeaderValue) },
                    {countHeaderKey, new HttpHeaderValue<int>(countHeaderValue) },
                }
            };

            var requestMessage = await factory
                .CreateHttpRequestMessageAsync(RequestConfiguration, HttpContentSerializer, request);

            Assert.NotNull(requestMessage);
            Assert.Equal(request.Method, requestMessage.Method);
            Assert.Equal(new Uri("https://index.hu/api/test"), requestMessage.RequestUri);

            Assert.Single(requestMessage.Headers.Accept);
            Assert.Single(requestMessage.Headers.AcceptCharset);

            Assert.Contains(apiKeyHeaderKey, requestMessage.Headers.Select(h => h.Key).ToList());
            Assert.Contains(countHeaderKey, requestMessage.Headers.Select(h => h.Key).ToList());

            var apiKeyHeader = requestMessage.Headers.Single(h => h.Key == apiKeyHeaderKey);
            var countHeader = requestMessage.Headers.Single(h => h.Key == countHeaderKey);

            Assert.Equal(apiKeyHeaderValue, apiKeyHeader.Value.ToList()[0]);
            Assert.Equal(countHeaderValue.ToString(), countHeader.Value.ToList()[0]);

            Assert.Null(requestMessage.Content);
        }
    }
}
