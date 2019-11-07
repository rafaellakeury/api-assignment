using Xunit;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.TestHost;
using System.Text;
using System.Net;

namespace Assignment.API.IntegrationTests
{
    public class DiffControllerIntegrationTests
    {
        private const string GetEqualPeopleHost = "api/v1/diff/1";
        private const string GetPeopleOfDifferentSizesHost = "api/v1/diff/4";
        private const string GetDifferentPeopleHost = "api/v1/diff/5";
        private const string GetNonExistingIdHost = "api/v1/diff/3";
        private const string GetInvalidIdRoute = "api/v1/diff/0";
        private readonly HttpClient _client;
        private readonly TestServer _server;
        private CustomWebApplicationFactory<Startup> customWebApplicationFactory;
        private Person defaultPerson;

        public DiffControllerIntegrationTests()
        {
            customWebApplicationFactory = new CustomWebApplicationFactory<Startup>();
            _server = customWebApplicationFactory.CreateTestServer();
            _client = _server.CreateClient();
            defaultPerson = CreateDefaultPerson();
        }

        private Person CreateDefaultPerson()
        {
            return new Person()
            {
                Name = "Matt",
                Age = 30,
                City = "Berlin",
                Profession = "Doctor"
            };
        }

        [Fact]
        public async void CanGetDiffForEqualPeople()
        {
            var httpResponse = await _client.GetAsync(GetEqualPeopleHost);
            var expectedDiffResult = new DiffResult() { AreEqual = true, AreSameSize = true };

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var diff = JsonConvert.DeserializeObject<DiffResult>(stringResponse);

            Assert.Equal(expectedDiffResult, diff);
        }

        [Fact]
        public async void CanGetDiffForPeopleOfDifferentSizes()
        {
            var httpResponse = await _client.GetAsync(GetPeopleOfDifferentSizesHost);
            var expectedDiffResult = new DiffResult() { AreEqual = false, AreSameSize = false };

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var diff = JsonConvert.DeserializeObject<DiffResult>(stringResponse);

            Assert.Equal(expectedDiffResult, diff);
        }

        [Fact]
        public async void CanGetDiffForDifferentPeople()
        {
            var httpResponse = await _client.GetAsync(GetDifferentPeopleHost);
            var expectedDiffResult = new DiffResult()
            {
                AreEqual = false,
                AreSameSize = true,
                Differences = new List<string>() { "name", "city" }
            };

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var diff = JsonConvert.DeserializeObject<DiffResult>(stringResponse);

            Assert.Equal(expectedDiffResult, diff);
        }

        [Fact]
        public async void GetNonExistingIdReturnsNoContent()
        {
            var httpResponse = await _client.GetAsync(GetNonExistingIdHost);

            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
        }

        [Fact]
        public async void CanPostLeftPerson()
        {
            var expectedResponse = defaultPerson;
            var request = new
            {
                Url = "api/v1/diff/2/left",
                Body = new
                {
                    Name = expectedResponse.Name,
                    Age = expectedResponse.Age,
                    City = expectedResponse.City,
                    Profession = expectedResponse.Profession
                }
            };

            var httpResponse = await _client.PostAsync(request.Url,
            new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json"));

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(stringResponse);

            Assert.Equal(expectedResponse, person);
        }

        [Fact]
        public async void CanPostRightPerson()
        {
            var expectedResponse = defaultPerson;
            var request = new
            {
                Url = "api/v1/diff/2/left",
                Body = new
                {
                    Name = expectedResponse.Name,
                    Age = expectedResponse.Age,
                    City = expectedResponse.City,
                    Profession = expectedResponse.Profession
                }
            };

            var httpResponse = await _client.PostAsync(request.Url,
            new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json"));

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(stringResponse);

            Assert.Equal(expectedResponse, person);
        }

        [Fact]
        public async void EmptyBodyReturnsDefaultPersonOnPost()
        {
            var expectedResponse = new Person();
            var request = new
            {
                Url = "api/v1/diff/2/left",
                Body = new { }
            };

            var httpResponse = await _client.PostAsync(request.Url,
            new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json"));

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(stringResponse);

            Assert.Equal(expectedResponse, person);
        }

        [Fact]
        public async void NullBodyReturnsBadRequestOnPost()
        {
            var request = new
            {
                Url = "api/v1/diff/2/left",
                Body = ""
            };

            var httpResponse = await _client.PostAsync(request.Url,
            new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [Fact]
        public async void InvalidIdReturnsBadRequestOnPost()
        {
            var expectedResponse = defaultPerson;
            var request = new
            {
                Url = "api/v1/diff/0/left",
                Body = new
                {
                    Name = expectedResponse.Name,
                    Age = expectedResponse.Age,
                    City = expectedResponse.City,
                    Profession = expectedResponse.Profession
                }
            };

            var httpResponse = await _client.PostAsync(request.Url,
            new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [Fact]
        public async void InvalidIdReturnsBadRequestOnGet()
        {
            var httpResponse = await _client.GetAsync(GetInvalidIdRoute);

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [Fact]
        public async void NonJsonBodyReturnsUnsupportedMediaTypeOnPost()
        {
            var request = new
            {
                Url = "api/v1/diff/2/left",
                Body = ""
            };

            var httpResponse = await _client.PostAsync(request.Url,
            new StringContent(request.Body));

            Assert.Equal(HttpStatusCode.UnsupportedMediaType, httpResponse.StatusCode);
        }
    }
}