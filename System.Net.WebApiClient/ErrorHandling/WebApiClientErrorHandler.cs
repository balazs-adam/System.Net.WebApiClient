using System.Net.WebApiClient.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.ErrorHandling
{
    /// <summary>
    /// Default implemetation of the IHttpClientErrorHandler.
    /// </summary>
    public class WebApiClientErrorHandler : IWebApiClientErrorHandler
    {
        /// <summary>
        /// Handles the unsuccessfull HTTP response for every request.
        /// </summary>
        /// <param name="responseMessage">The HTTP response with a non-success status code.</param>
        /// <param name="cancellationToken">A CancellationToken that can be used to cancel this operation.</param>
        /// <returns></returns>
        public virtual Task HandleErrorsAsync(HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
        {
            responseMessage.EnsureSuccessStatusCode();

            return Task.CompletedTask;
        }
    }
}
