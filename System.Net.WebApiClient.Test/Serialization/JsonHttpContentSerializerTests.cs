using System.Net.WebApiClient.Serialization;
using System.Net.WebApiClient.Test.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Net.WebApiClient.Test.Serialization
{
    public class JsonHttpContentSerializerTests
    {
        private JsonHttpContentSerializer CreateSerializer()
        {
            return new JsonHttpContentSerializer();
        }


        [Fact]
        public async Task CreateAcceptHeaderValuesAsync()
        {
            var serializer = CreateSerializer();

            var acceptHeaders = await serializer.CreateAcceptHeaderValuesAsync();

            Assert.NotNull(acceptHeaders);
            Assert.NotEmpty(acceptHeaders);
            Assert.Contains("application/json", acceptHeaders.Select(k => k.MediaType));
        }

        [Fact]
        public async Task CreateAcceptCharsetHeaderValuesAsync()
        {
            var serializer = CreateSerializer();

            var acceptCharsetHeaders = await serializer.CreateAcceptCharsetHeaderValuesAsync();

            Assert.NotNull(acceptCharsetHeaders);
            Assert.NotEmpty(acceptCharsetHeaders);
            Assert.Contains("utf-8", acceptCharsetHeaders.Select(k => k.Value));
        }

        [Fact]
        public async Task SerializeRequestContentAsync_Class()
        {
            var serializer = CreateSerializer();

            var request = await serializer.SerializeRequestContentAsync(new Person
            {
                Name = "John Smith",
                Age = 18,
                DateOfBirth = DateTime.Now.AddYears(-18),
            });

            var content = await request.ReadAsStringAsync();

            Assert.NotNull(request);
            Assert.NotNull(content);
            Assert.NotEmpty(content);

            Assert.Contains("Name", content);
            Assert.Contains("Age", content);
            Assert.Contains("DateOfBirth", content);
        }

        [Fact]
        public async Task SerializeRequestContentAsync_Primitive()
        {
            var serializer = CreateSerializer();

            var request = await serializer.SerializeRequestContentAsync(10);
            var content = await request.ReadAsStringAsync();

            Assert.Equal("10", content);
        }

        [Fact]
        public async Task SerializeRequestContentAsync_Null()
        {
            var serializer = CreateSerializer();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await serializer.SerializeRequestContentAsync(null));
        }

        [Fact]
        public async Task DeserializeResponseContentAsync_Class()
        {
            var serializer = CreateSerializer();

            var person = new Person
            {
                Name = "John Smith",
                Age = 18,
                DateOfBirth = DateTime.Now.AddYears(-18),
            };

            var content = new StringContent(JsonConvert.SerializeObject(person));

            var deserializedPerson = await serializer.DeserializeResponseContentAsync<Person>(content);

            Assert.NotNull(deserializedPerson);
            Assert.Equal(person.Name, deserializedPerson.Name);
            Assert.Equal(person.Age, deserializedPerson.Age);
            Assert.Equal(person.DateOfBirth, deserializedPerson.DateOfBirth);
        }

        [Fact]
        public async Task DeserializeResponseContentAsync_ClassToString()
        {
            var serializer = CreateSerializer();

            var person = new Person
            {
                Name = "John Smith",
                Age = 18,
                DateOfBirth = DateTime.Now.AddYears(-18),
            };

            var content = new StringContent(JsonConvert.SerializeObject(person));

            var deserializedPerson = await serializer.DeserializeResponseContentAsync<string>(content);

            Assert.NotNull(deserializedPerson);

            Assert.Contains(person.Name, deserializedPerson);
            Assert.Contains(person.Age.ToString(), deserializedPerson);
            Assert.Contains(person.DateOfBirth.ToString("yyyy-MM-dd"), deserializedPerson);
        }

        [Fact]
        public async Task DeserializeResponseContentAsync_Primitive()
        {
            var serializer = CreateSerializer();

            var number = 10;

            var content = new StringContent(number.ToString());

            var deserializedNumber = await serializer.DeserializeResponseContentAsync<int>(content);

            Assert.Equal(number, deserializedNumber);
        }

        [Fact]
        public async Task DeserializeResponseContentAsync_Null()
        {
            var serializer = CreateSerializer();

            var deserializedPerson = await serializer.DeserializeResponseContentAsync<Person>(null);

            Assert.Null(deserializedPerson);
        }
    }
}
