using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.AutoMock;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.InputModels.User;
using PoesiaFacil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoesiaFacil.Tests.PoemTest
{
    [Collection(nameof(UserCollection))]
    public class UserTests
    {
        readonly UserTestsFixture _userFixture;

        public UserTests(UserTestsFixture userFixture)
        {
            _userFixture = userFixture;
        }

        [Fact(DisplayName = "Create User")]
        [Trait("Category", "User")]
        public async Task UserService_User_ShouldAddUser()
        {
            //Arrange
            var userInputModel = _userFixture.GenerateValidUserInputModelStatic();
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<UserService>();

            mocker.GetMock<IPasswordHasher<User>>()
                  .Setup(m => m.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                  .Returns("Hash value");

            //Act
            var result = await userService.CreateUser(userInputModel);

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Not Create User")]
        [Trait("Category", "User")]
        public async Task UserService_User_ShouldNotAddUser()
        {
            //Arrange
            var user = _userFixture.GenerateInvalidUserInputModel();
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<UserService>();

            //Act
            var result = await userService.CreateUser(user);

            //Assert
            Assert.False(result);
        }

    }
}
