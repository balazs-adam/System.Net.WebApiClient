using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.WebApiClient.Test.WebApiClient
{
    public class WebApiClientTestBase
    {
        public virtual Net.WebApiClient.WebApiClient CreateWebApiClient()
        {
            return new Net.WebApiClient.WebApiClient();
        }
    }
}
