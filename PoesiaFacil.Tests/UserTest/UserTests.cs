using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using NuGet.Common;
using PoesiaFacil.Controllers;
using PoesiaFacil.Controllers.Contracts;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Helpers;
using PoesiaFacil.Helpers.Contracts;
using PoesiaFacil.Models.InputModels.User;
using PoesiaFacil.Models.ViewModels.User;
using PoesiaFacil.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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

        [Fact(DisplayName = "Post - Success")]
        [Trait("Category", "User")]
        public async Task UserService_User_ShouldAddUser()
        {
            //Arrange
            var userInputModel = _userFixture.GenerateValidUserInputModelStatic();
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<CreateUserService>();

            mocker.GetMock<IPasswordHasher<User>>()
                  .Setup(m => m.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                  .Returns("Hash value");

            //Act
            var result = await userService.CreateUserAsync(userInputModel);

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Post - Failed")]
        [Trait("Category", "User")]
        public async Task UserService_User_ShouldNotAddUser()
        {
            //Arrange
            var user = _userFixture.GenerateInvalidUserInputModel();
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<CreateUserService>();

            //Act
            var result = await userService.CreateUserAsync(user);

            //Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "SignInAsync - Success")]
        [Trait("Category", "User")]
        public async Task SignInAsync_ShouldReturnTokenAndUser_WhenCredentialsAreValid()
        {
            // Arrange
            var user = _userFixture.GenerateValidUser();
            var email = user.Email;
            var password = "teste12345";

            var mocker = new AutoMocker();
            mocker
                .GetMock<IUserRepository>()
                .Setup(r => r.GetWithParamsAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            mocker
                .GetMock<IPasswordHasher<User>>()
                .Setup(p => p.VerifyHashedPassword(user, user.Password, password))
                .Returns(PasswordVerificationResult.Success);

            mocker
                .GetMock<IMapper>()
                .Setup(m => m.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(new UserViewModel());

            mocker
              .GetMock<IJwtTokenHelper>()
              .Setup(m => m.GetToken(It.IsAny<User>()))
              .Returns("jwtbearer");

            var userController = mocker.CreateInstance<UserController>();

            // Act
            var result = await userController.SignInAsync(email, password);

            // Assert
            var okResult = Assert.IsType<CustomActionResult>(result);

            Assert.Equal(HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact(DisplayName = "SignInAsync - User Not Found")]
        [Trait("Category", "User")]
        public async Task SignInAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var mocker = new AutoMocker();

            mocker
                .GetMock<IUserRepository>()
                .Setup(r => r.GetWithParamsAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null);

            mocker
                .GetMock<IMapper>()
                .Setup(m => m.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(new UserViewModel()); // Substitua pelo seu ViewModel

            var userController = mocker.CreateInstance<UserController>();

            // Act
            var result = await userController.SignInAsync("user", "user");

            // Assert
            var okResult = Assert.IsType<CustomActionResult>(result);

            Assert.Equal(HttpStatusCode.NotFound, okResult.StatusCode);
            Assert.True(okResult.Errors.Any(x => x == "Usuário não encontrado"));
        }

        [Fact(DisplayName = "SignInAsync - Invalid Password")]
        [Trait("Category", "User")]
        public async Task SignInAsync_ShouldReturnBadRequest_WhenPasswordIsInvalid()
        {
            // Arrange
            var user = _userFixture.GenerateValidUser();
            var email = user.Email;
            var password = "user1234";

            var mocker = new AutoMocker();
            mocker
                .GetMock<IUserRepository>()
                .Setup(r => r.GetWithParamsAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            mocker
                .GetMock<IPasswordHasher<User>>()
                .Setup(p => p.VerifyHashedPassword(user, user.Password, password))
                .Returns(PasswordVerificationResult.Failed);

            var userController = mocker.CreateInstance<UserController>();

            // Act
            var result = await userController.SignInAsync(email, password);

            // Assert
            var okResult = Assert.IsType<CustomActionResult>(result);

            Assert.Equal(HttpStatusCode.BadRequest, okResult.StatusCode);
            Assert.True(okResult.Errors.Any(x => x == "Senha inválida"));
        }

    }
}
