using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.WebApiClient.Request
{
    public class RequestConfiguration
    {
        public bool UseGzip { get; set; }
        public Uri BaseUri { get; set; }
    }
}
