using System.Net.WebApiClient.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Net.WebApiClient.Test.ErrorHandling
{
    public class WebApiClientErrorHandlerTests
    {
        [Fact]
        public async Task InternalServerError()
        {
            var errorHandler = new WebApiClientErrorHandler();

            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await errorHandler.HandleErrorsAsync(
                    new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)));
        }
    }
}
