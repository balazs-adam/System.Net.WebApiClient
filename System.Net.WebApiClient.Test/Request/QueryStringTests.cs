using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Net.WebApiClient.Test.Request
{
    public class QueryStringTests : HttpRequestFactoryTestBase
    {
        [Fact]
        public async Task CreateRequestUri_BaseUri_RelativeRequestUri()
        {
            var factory = CreateHttpRequestFactory();

            var uri = await factory.CreateRequestUriAsync(new Uri("https://index.hu"), new Uri("/api/test", UriKind.Relative));

            Assert.Equal(new Uri("https://index.hu/api/test"), uri);
        }

        [Fact]
        public async Task CreateRequestUri_BaseUriAbsoluteRequestUri()
        {
            var factory = CreateHttpRequestFactory();

            var uri = await factory.CreateRequestUriAsync(new Uri("https://index.hu"), new Uri("https://origo.hu/api/test"));

            Assert.Equal(new Uri("https://origo.hu/api/test"), uri);
        }

        [Fact]
        public async Task CreateRequestUri_QueryString_Int()
        {
            var factory = CreateHttpRequestFactory();

            var uri = await factory.CreateRequestUriAsync(new Uri("https://index.hu"), new Uri("/api/test", UriKind.Relative), new
            {
                Int = 10,
            });

            Assert.Equal(new Uri($"https://index.hu/api/test?Int=10"), uri);
        }

        [Fact]
        public async Task CreateRequestUri_QueryString_String()
        {
            var factory = CreateHttpRequestFactory();

            var uri = await factory.CreateRequestUriAsync(new Uri("https://index.hu"), new Uri("/api/test", UriKind.Relative), new
            {
                String = "test"
            });

            Assert.Equal(new Uri($"https://index.hu/api/test?String=test"), uri);
        }

        [Fact]
        public async Task CreateRequestUri_QueryString_DateTime()
        {
            var factory = CreateHttpRequestFactory();

            var uri = await factory.CreateRequestUriAsync(new Uri("https://index.hu"), new Uri("/api/test", UriKind.Relative), new
            {
                DateTime = new DateTime(2018, 10, 5, 8, 0, 0),
            });

            Assert.Equal(new Uri($"https://index.hu/api/test?DateTime=2018-10-05T08%3A00%3A00"), uri);
        }

        [Fact]
        public async Task CreateRequestUri_QueryString_Time()
        {
            var factory = CreateHttpRequestFactory();

            var uri = await factory.CreateRequestUriAsync(new Uri("https://index.hu"), new Uri("/api/test", UriKind.Relative), new
            {
                Time = new TimeSpan(10, 0, 0)
            });

            Assert.Equal(new Uri($"https://index.hu/api/test?Time=10%3A00%3A00"), uri);
        }

        [Fact]
        public async Task CreateRequestUri_QueryString_Bool()
        {
            var factory = CreateHttpRequestFactory();

            var uri = await factory.CreateRequestUriAsync(new Uri("https://index.hu"), new Uri("/api/test", UriKind.Relative), new
            {
                Bool = true,
            });

            Assert.Equal(new Uri($"https://index.hu/api/test?Bool=True"), uri);
        }

        [Fact]
        public async Task CreateRequestUri_QueryString_All()
        {
            var factory = CreateHttpRequestFactory();

            var uri = await factory.CreateRequestUriAsync(new Uri("https://index.hu"), new Uri("/api/test", UriKind.Relative), new
            {
                Int = 10,
                String = "test",
                DateTime = new DateTime(2018, 10, 5, 8, 0, 0),
                Time = new TimeSpan(10, 0, 0),
                Bool = true,
            });

            Assert.Equal(new Uri($"https://index.hu/api/test?Int=10&String=test&DateTime=2018-10-05T08%3A00%3A00&Time=10%3A00%3A00&Bool=True"), uri);
        }

    }
}
