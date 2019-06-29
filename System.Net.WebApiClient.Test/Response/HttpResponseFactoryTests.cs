using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebApiClient.Response;
using System.Net.WebApiClient.Serialization;
using System.Net.WebApiClient.Test.Model;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Net.WebApiClient.Test.Response
{
    public class HttpResponseFactoryTest
    {
        protected IHttpContentSerializer Serializer =>
            WebApiClientConfiguration.Default.Serializer;

        protected virtual IHttpResponseFactory CreateHttpResponseFactory()
        {
            return new HttpResponseFactory();
        }

        [Fact]
        public async Task CreateResponseAsync_EmptySuccess()
        {
            var factory = CreateHttpResponseFactory();

            var response = await factory.CreateResponseAsync(Serializer, new HttpResponseMessage(HttpStatusCode.OK));

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateResponseAsync_EmptyFail()
        {
            var factory = CreateHttpResponseFactory();

            var response = await factory.CreateResponseAsync(Serializer, new HttpResponseMessage(HttpStatusCode.InternalServerError));

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task CreateResponseAsync_ParsePerson()
        {
            var factory = CreateHttpResponseFactory();

            var person = new Person
            {
                Age = 18,
                DateOfBirth = DateTime.Now.AddYears(-18),
                Name = "John Smith"
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(person))
            };

            var response = await factory.CreateResponseAsync<Person>(Serializer, responseMessage);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(response.Content);
            Assert.Equal(person.Age, response.Content.Age);
            Assert.Equal(person.DateOfBirth, response.Content.DateOfBirth);
            Assert.Equal(person.Name, response.Content.Name);
        }
    }
}
