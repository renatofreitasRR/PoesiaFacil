using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.InputModels.Poem;
using PoesiaFacil.Models.InputModels.User;
using Xunit;

namespace PoesiaFacil.Tests.PoemTest
{
    [CollectionDefinition(nameof(UserCollection))]
    public class UserCollection : ICollectionFixture<UserTestsFixture> { }

    public class UserTestsFixture : IDisposable
    {
        public void Dispose()
        {
        }
        public CreateUserInputModel GenerateValidUserInputModel()
        {
            var user = new Faker<CreateUserInputModel>("pt_BR")
                .CustomInstantiator(f => new CreateUserInputModel
                {
                    Name = f.Name.FullName(),
                    ArtisticName = f.Name.FullName(),
                    Email = f.Internet.Email(f.Name.FullName(), f.Name.LastName()),
                    Description = f.Name.JobDescriptor(),
                    Password = f.Name.FullName(),
                });

            return user;
        }

        public CreateUserInputModel GenerateValidUserInputModelStatic()
        {
            var user =  new CreateUserInputModel
            {
                Name = "Renato Freitas",
                ArtisticName = "Renatinho",
                Email = "natogfreitas@gmail.com",
                Description = "Um humano qualquer",
                Password = "password",
            };

            return user;
        }

        public User GenerateValidUser()
        {
            var user = new Faker<User>("pt_BR")
                .CustomInstantiator(f => new User
                {
                    Name = f.Name.FullName(),
                    ArtisticName = f.Name.FullName(),
                    Email = f.Internet.Email(f.Name.FullName(), f.Name.LastName()),
                    Description = f.Name.JobDescriptor(),
                    Password = f.Name.FullName(),
                });

            return user;
        }

        public CreateUserInputModel GenerateInvalidUserInputModel()
        {
            var user = new Faker<CreateUserInputModel>("pt_BR")
               .CustomInstantiator(f => new CreateUserInputModel
               {
                   Name = "",
                   ArtisticName = "",
                   Email = f.Internet.Email(f.Name.FullName(), f.Name.LastName()),
                   Description = f.Name.JobDescriptor(),
                   Password = f.Internet.Password(),
               });

            return user;
        }


    }
}
