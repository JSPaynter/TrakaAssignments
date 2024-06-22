using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NUnit.Framework;

using Core.BaseClasses;
using RichardSzalay.MockHttp;

namespace CoreUnitTests.BaseClasses
{
    [TestFixture, Parallelizable]
    public class BaseAPITests
    {
        [TestCase("http://localhost/api", "", "", HttpStatusCode.OK)]
        [TestCase("http://localhost/api", "", "test response", HttpStatusCode.OK)]
        [TestCase("http://localhost/api", "Id=2&Version=Dev", "test response", HttpStatusCode.OK)]
        [TestCase("http://localhost/api", "", "", HttpStatusCode.BadRequest)]
        [TestCase("http://localhost/api", "Id=2&Version=Dev", "test response", HttpStatusCode.BadRequest)]
        public void GetRequestReturnsCorrectResponseStringParameters(string baseUrl, string parameters, string expectedResponse, HttpStatusCode statusCode)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(baseUrl)
                .WithExactQueryString(parameters)
                .Respond(statusCode, "text/plain", expectedResponse);
            BaseAPI api = new(new HttpClient(mockHttp));
            var response = api.Get(baseUrl, parameters);
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code was incorrect");
                Assert.That(result, Is.EqualTo(expectedResponse), "Response does not match");
            });
        }

        [TestCaseSource(nameof(GetRequestDictionaryParameters))]
        public void GetRequestWithDictionaryParameters(string baseUrl, Dictionary<string, object> parameters, string parameterString,
            string expectedResponse, HttpStatusCode statusCode)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(baseUrl)
                .WithExactQueryString(parameterString)
                .Respond(statusCode, "text/plain", expectedResponse);
            BaseAPI api = new(new HttpClient(mockHttp));
            var response = api.Get(baseUrl, parameters);
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code was incorrect");
                Assert.That(result, Is.EqualTo(expectedResponse), "Response does not match");
            });
        }

        static readonly object[] GetRequestDictionaryParameters =
        [
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {}, "", "test response", HttpStatusCode.OK
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {}, "", "test response", HttpStatusCode.BadRequest
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {
                    { "param1", "param1value" }
                }, "param1=param1value", "test response", HttpStatusCode.OK
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {
                    { "param1", "param1value" },
                    { "param2", "param2value" }
                }, "param1=param1value&param2=param2value", "test response", HttpStatusCode.OK
            }
        ];

        [TestCase("http://localhost/api", "", "", HttpStatusCode.OK, "")]
        [TestCase("http://localhost/api", "", "test response", HttpStatusCode.OK, "")]
        [TestCase("http://localhost/api", "", "", HttpStatusCode.OK, "test content")]
        [TestCase("http://localhost/api", "Id=2&Version=Dev", "test response", HttpStatusCode.OK, "test content")]
        [TestCase("http://localhost/api", "", "", HttpStatusCode.BadRequest, "")]
        public void PostRequestReturnsCorrectResponseStringParameters(string baseUrl, string parameters, string expectedResponse,
            HttpStatusCode statusCode, string stringContent)
        {
            var sc = new StringContent(stringContent);
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(baseUrl)
                .WithContent(stringContent)
                .WithExactQueryString(parameters)
                .Respond(statusCode, "text/plain", expectedResponse);
            BaseAPI api = new(new HttpClient(mockHttp) { BaseAddress = new Uri(baseUrl) });
            var response = api.Post(parameters, sc);
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code was incorrect");
                Assert.That(result, Is.EqualTo(expectedResponse), "Response does not match");
            });
        }

        [TestCaseSource(nameof(PostRequestDictionaryParameters))]
        public void PostRequestReturnsCorrectResponseDictionaryParameters(string baseUrl, Dictionary<string, object> parameters,
            string parameterString, string expectedResponse, HttpStatusCode statusCode, string stringContent)
        {
            var sc = new StringContent(stringContent);
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(baseUrl)
                .WithContent(stringContent)
                .WithExactQueryString(parameterString)
                .Respond(statusCode, "text/plain", expectedResponse);
            BaseAPI api = new(new HttpClient(mockHttp) { BaseAddress = new Uri(baseUrl) });
            var response = api.Post(parameters, sc);
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code was incorrect");
                Assert.That(result, Is.EqualTo(expectedResponse), "Response does not match");
            });
        }

        static readonly object[] PostRequestDictionaryParameters =
        [
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {}, "", "test response", HttpStatusCode.OK, "test content"
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {
                    { "param1", "param1value" }
                }, "param1=param1value", "test response", HttpStatusCode.OK, "test content"
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {
                    { "param1", "param1value" },
                    { "param2", "param2value" }
                }, "param1=param1value&param2=param2value", "test response", HttpStatusCode.OK, "test content"
            }
        ];

        [Test]
        public void PostRequestReturnsCorrectResponseDefaults()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost/api")
                .WithExactQueryString("")
                .Respond(HttpStatusCode.OK, "text/plain", "test response");
            BaseAPI api = new(new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost/api") });
            var response = api.Post();
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code was incorrect");
                Assert.That(result, Is.EqualTo("test response"), "Response does not match");
            });
        }

        [TestCase("http://localhost/api", "", "", HttpStatusCode.OK, "")]
        [TestCase("http://localhost/api", "", "test response", HttpStatusCode.OK, "")]
        [TestCase("http://localhost/api", "", "", HttpStatusCode.OK, "test content")]
        [TestCase("http://localhost/api", "Id=2&Version=Dev", "test response", HttpStatusCode.OK, "test content")]
        [TestCase("http://localhost/api", "", "", HttpStatusCode.BadRequest, "")]
        public void PutRequestReturnsCorrectResponseStringParameters(string baseUrl, string parameters, string expectedResponse,
            HttpStatusCode statusCode, string stringContent)
        {
            var sc = new StringContent(stringContent);
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(baseUrl)
                .WithContent(stringContent)
                .WithExactQueryString(parameters)
                .Respond(statusCode, "text/plain", expectedResponse);
            BaseAPI api = new(new HttpClient(mockHttp));
            var response = api.Put(baseUrl, parameters, sc);
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code was incorrect");
                Assert.That(result, Is.EqualTo(expectedResponse), "Response does not match");
            });
        }

        [TestCaseSource(nameof(PutRequestDictionaryParameters))]
        public void PutRequestReturnsCorrectResponseDictionaryParameters(string baseUrl, Dictionary<string, object> parameters,
            string parameterString, string expectedResponse, HttpStatusCode statusCode, string stringContent)
        {
            var sc = new StringContent(stringContent);
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(baseUrl)
                .WithContent(stringContent)
                .WithExactQueryString(parameterString)
                .Respond(statusCode, "text/plain", expectedResponse);
            BaseAPI api = new(new HttpClient(mockHttp) { BaseAddress = new Uri(baseUrl) });
            var response = api.Put(baseUrl, parameters, sc);
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code was incorrect");
                Assert.That(result, Is.EqualTo(expectedResponse), "Response does not match");
            });
        }

        static readonly object[] PutRequestDictionaryParameters =
        [
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {}, "", "test response", HttpStatusCode.OK, "test content"
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {
                    { "param1", "param1value" }
                }, "param1=param1value", "test response", HttpStatusCode.OK, "test content"
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {
                    { "param1", "param1value" },
                    { "param2", "param2value" }
                }, "param1=param1value&param2=param2value", "test response", HttpStatusCode.OK, "test content"
            }
        ];

        [Test]
        public void PutRequestReturnsCorrectResponseDefaults()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost/api")
                .WithExactQueryString("")
                .Respond(HttpStatusCode.OK, "text/plain", "test response");
            BaseAPI api = new(new HttpClient(mockHttp));
            var response = api.Put("http://localhost/api");
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code was incorrect");
                Assert.That(result, Is.EqualTo("test response"), "Response does not match");
            });
        }

        [TestCase("http://localhost/api", "", "", HttpStatusCode.OK)]
        [TestCase("http://localhost/api", "", "test response", HttpStatusCode.OK)]
        [TestCase("http://localhost/api", "", "", HttpStatusCode.OK)]
        [TestCase("http://localhost/api", "Id=2&Version=Dev", "test response", HttpStatusCode.OK)]
        [TestCase("http://localhost/api", "", "", HttpStatusCode.BadRequest)]
        public void DeleteRequestReturnsCorrectResponseStringParameters(string baseUrl, string parameters, string expectedResponse,
            HttpStatusCode statusCode)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(baseUrl)
                .WithExactQueryString(parameters)
                .Respond(statusCode, "text/plain", expectedResponse);
            BaseAPI api = new(new HttpClient(mockHttp));
            var response = api.Delete(baseUrl, parameters);
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code was incorrect");
                Assert.That(result, Is.EqualTo(expectedResponse), "Response does not match");
            });
        }

        [TestCaseSource(nameof(DeleteRequestDictionaryParameters))]
        public void DeleteRequestReturnsCorrectResponseDictionaryParameters(string baseUrl, Dictionary<string, object> parameters,
            string parameterString, string expectedResponse, HttpStatusCode statusCode)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(baseUrl)
                .WithExactQueryString(parameterString)
                .Respond(statusCode, "text/plain", expectedResponse);
            BaseAPI api = new(new HttpClient(mockHttp) { BaseAddress = new Uri(baseUrl) });
            var response = api.Delete(baseUrl, parameters);
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code was incorrect");
                Assert.That(result, Is.EqualTo(expectedResponse), "Response does not match");
            });
        }

        static readonly object[] DeleteRequestDictionaryParameters =
        [
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {}, "", "test response", HttpStatusCode.OK
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {
                    { "param1", "param1value" }
                }, "param1=param1value", "test response", HttpStatusCode.OK
                    
            },
            new object[] { "http://localhost/api", new Dictionary<string, object>
                {
                    { "param1", "param1value" },
                    { "param2", "param2value" }
                }, "param1=param1value&param2=param2value", "test response", HttpStatusCode.OK
            }
        ];

        [Test]
        public void DeleteRequestReturnsCorrectResponseDefaults()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost/api")
                .WithExactQueryString("")
                .Respond(HttpStatusCode.OK, "text/plain", "test response");
            BaseAPI api = new(new HttpClient(mockHttp));
            var response = api.Delete("http://localhost/api");
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code was incorrect");
                Assert.That(result, Is.EqualTo("test response"), "Response does not match");
            });
        }
    }
}
