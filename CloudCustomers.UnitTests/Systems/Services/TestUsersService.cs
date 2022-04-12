using CloudCustomers.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CloudCustomers.UnitTests.Helpers;
using CloudCustomers.API.Models;
using CloudCustomers.UnitTests.Fixtures;
using System.Net.Http;
using Moq.Protected;
using System.Threading;
using FluentAssertions;
using Microsoft.Extensions.Options;
using CloudCustomers.API.Config;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeHttpRequest()
        {
            //arrage
            var expecteResponse = UserFixture.GetTestUsers();
            var handlermock = MockHttpMessageHandler<User>.SetupBasicGerResourceList(expecteResponse);
            var httpClient = new HttpClient(handlermock.Object);

            var endpoint = "http://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            //Act
            await sut.GetAllUsers();

            //Assert

            handlermock.Protected().Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetAllUsers_Whenhits404_ReturnsEmptyListOfUsers()
        {
            //arrange
            var expecteResponse = UserFixture.GetTestUsers();
            var handlermock = MockHttpMessageHandler<User>.SetupReturn404(expecteResponse);
            var httpClient = new HttpClient(handlermock.Object);
            var endpoint = "http://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            //act

            var result = await sut.GetAllUsers();

            //assert
            result.Count.Should().Be(0);
        }
        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnedListOfExpectedSize()
        {
            //arrange
            var expecteResponse = UserFixture.GetTestUsers();
            var handlermock = MockHttpMessageHandler<User>.SetupBasicGerResourceList(expecteResponse);
            var httpClient = new HttpClient(handlermock.Object);
            var endpoint = "http://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            //act

            var result = await sut.GetAllUsers();

            //assert
            result.Count.Should().Be(expecteResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvlokesConfigExternalUrl()
        {
            //arrange
            var expecteResponse = UserFixture.GetTestUsers();
            var endpoint = "https://jsonplaceholder.typicode.com/users";
            var handlermock = MockHttpMessageHandler<User>.SetupBasicGerResourceList(expecteResponse, endpoint);
            var httpClient = new HttpClient(handlermock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            //act

            var result = await sut.GetAllUsers();

            //assert
            handlermock.Protected().Verify("SendAsync",
              Times.Exactly(1),
              ItExpr.Is<HttpRequestMessage>(
                  req => req.Method == HttpMethod.Get 
                  && req.RequestUri.ToString() == endpoint),
              ItExpr.IsAny<CancellationToken>());
        }
    }
       
}
