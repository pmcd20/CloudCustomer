using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudCustomers.API.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CloudCustomers.UnitTests.Helpers
{
    internal static class MockHttpMessageHandler<T> 
    {

        internal static Mock<HttpMessageHandler> SetupBasicGerResourceList(List<T> expectedResponse)
        {
            var mockresponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockresponse.Content.Headers.ContentType = 
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>
                ("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockresponse);

            return handlerMock;

        }

        internal static Mock<HttpMessageHandler> SetupBasicGerResourceList(List<User> expecteResponse, string endpoint)
        {

            var mockresponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expecteResponse))
            };

            mockresponse.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(endpoint),
                Method = HttpMethod.Get
            };




            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>
                ("SendAsync",
                httpRequestMessage,
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockresponse);

            return handlerMock;
        }



        internal static Mock<HttpMessageHandler> SetupReturn404(List<User> expecteResponse)
        {
            var mockresponse = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new StringContent("")
            };

            mockresponse.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>
                ("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockresponse);

            return handlerMock;
        }

    
    }
}
