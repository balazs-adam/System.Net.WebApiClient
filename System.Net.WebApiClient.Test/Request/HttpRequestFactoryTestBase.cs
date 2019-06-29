using System;
using System.Collections.Generic;
using System.Net.WebApiClient.Request;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Net.WebApiClient.Test.Request
{
    public abstract class HttpRequestFactoryTestBase
    {
        protected virtual IHttpRequestFactory CreateHttpRequestFactory()
        {
            return new HttpRequestFactory();
        }
    }
}
