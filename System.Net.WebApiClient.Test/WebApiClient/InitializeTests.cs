using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Net.WebApiClient.Test.WebApiClient
{
    public class InitializeTests : WebApiClientTestBase
    {
        [Fact]
        public async Task InitializeAsync_NullSerializer()
        {
            var client = CreateWebApiClient();

            var defaultConfig = WebApiClientConfiguration.Default;
            defaultConfig.Serializer = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.InitializeAsync(defaultConfig));
        }

        [Fact]
        public async Task InitializeAsync_NullErrorHandler()
        {
            var client = CreateWebApiClient();

            var defaultConfig = WebApiClientConfiguration.Default;
            defaultConfig.ErrorHandler = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.InitializeAsync(defaultConfig));
        }

        [Fact]
        public async Task InitializeAsync_NullRequestFactory()
        {
            var client = CreateWebApiClient();

            var defaultConfig = WebApiClientConfiguration.Default;
            defaultConfig.RequestFactory = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.InitializeAsync(defaultConfig));
        }

        [Fact]
        public async Task InitializeAsync_NullResponseFactory()
        {
            var client = CreateWebApiClient();

            var defaultConfig = WebApiClientConfiguration.Default;
            defaultConfig.ResponseFactory = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.InitializeAsync(defaultConfig));
        }
    }
}
