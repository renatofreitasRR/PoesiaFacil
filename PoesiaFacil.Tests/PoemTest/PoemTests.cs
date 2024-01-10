using Microsoft.AspNetCore.Http;
using Moq;
using Moq.AutoMock;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Models;
using PoesiaFacil.Services;
using PoesiaFacil.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PoesiaFacil.Tests.PoemTest
{
    [Collection(nameof(PoemCollection))]
    public class PoemTests
    {
        readonly PoemTestsFixture _poemFixture;

        public PoemTests(PoemTestsFixture poemFixture)
        {
            _poemFixture = poemFixture;
        }

        [Fact(DisplayName = "Create Poem")]
        [Trait("Category", "Poem")]
        public async Task PoemService_Poem_ShouldAddPoem()
        {
            //Arrange
            var poem = _poemFixture.GenerateValidPoemInputModel();
            var mocker = new AutoMocker();
            var poemService = mocker.CreateInstance<PoemService>();

            mocker
                .GetMock<ICurrentUserService>()
                .Setup(r => r.GetUser())
                .Returns(new ContextUser
                    {
                        Email = "UnitTest@email.com",
                        Id = "654a88d48e0d705d988ff871",
                        Name = "Unit Test"
                    });

            //Act
            var result = await poemService.CreatePoem(poem);

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Not Create Poem")]
        [Trait("Category", "Poem")]
        public async Task PoemService_Poem_ShouldNotAddPoem()
        {
            //Arrange
            var poem = _poemFixture.GenerateInvalidPoemInputModel();
            var mocker = new AutoMocker();
            var poemService = mocker.CreateInstance<PoemService>();

            mocker
                .GetMock<ICurrentUserService>()
                .Setup(r => r.GetUser())
                .Returns(new ContextUser
                {
                    Email = "UnitTest@email.com",
                    Id = "654a88d48e0d705d988ff871",
                    Name = "Unit Test"
                });

            //Act
            var result = await poemService.CreatePoem(poem);

            //Assert
            Assert.False(result);
        }

    }
}
