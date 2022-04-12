using CloudCustomers.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CloudCustomers.API.Services;
using System.Collections.Generic;
using CloudCustomers.API.Models;
using CloudCustomers.UnitTests.Fixtures;

namespace CloudCustomers.UnitTests.Systems.Controllers
{
    public class TestingUsersController
    {
        [Fact]
        public async Task Get_Onsuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockUserService = new Mock<IUsersService>();
            mockUserService
              .Setup(service => service.GetAllUsers())
               .ReturnsAsync(UserFixture.GetTestUsers());
              
            var sut = new UsersController(mockUserService.Object);

            //act
            var result = (OkObjectResult)await sut.Get();


            //assert
            result.StatusCode.Should().Be(200);
        }


        [Fact]
        public async void Get_Onsuccess_InvokeuserServiceExactlyOnce()
        {
            var mockUserService = new Mock<IUsersService>();
            mockUserService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUserService.Object);

            var result = await sut.Get();

            mockUserService.Verify(
                service => service.GetAllUsers(),
                Times.Once());

        }

        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {
            //Arrange

            var mockUserService = new Mock<IUsersService>();
            mockUserService
                .Setup(service => service.GetAllUsers())
                 .ReturnsAsync(UserFixture.GetTestUsers());

            var sut = new UsersController(mockUserService.Object);


            //Act

            var result = await sut.Get();

            //Assert

            result.Should().BeOfType<OkObjectResult>();

            var objectResult = (OkObjectResult)result;

            objectResult.Value.Should().BeOfType<List<User>>();




        }

        [Fact]
        public async Task Get_OnusersFound_Return404()
        {
            //Arrange

            var mockUserService = new Mock<IUsersService>();
            mockUserService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUserService.Object);


            //Act

            var result = await sut.Get();

            //Assert

            result.Should().BeOfType<NotFoundResult>();
            var statusResult = (NotFoundResult)result;

            statusResult.StatusCode.Should().Be(404);

        }
    }
}
