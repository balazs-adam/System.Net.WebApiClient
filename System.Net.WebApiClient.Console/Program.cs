using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.WebApiClient.Console
{

    class Program
    {

        static async Task Main(string[] args)
        {
            var client = new WebApiClient();


            var post = await client.SendGetRequestAsync<List<Post>>(new Request.HttpGetRequest
            {
                RequestUri = new Uri("https://jsonplaceholder.typicode.com/posts"),
            });


        }
    }
}
