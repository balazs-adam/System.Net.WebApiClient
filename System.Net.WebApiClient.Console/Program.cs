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


            var post = await client.SendPostRequestAsync<Post>(new Request.HttpPostRequest
            {
                RequestUri = new Uri("https://jsonplaceholder.typicode.com/posts"),
                Content = new Post
                {
                    Body = "Teszt",
                    Title = "Title",
                    UserId = 7
                }
            });


        }
    }
}
