using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.InputModels.Poem;
using Xunit;

namespace PoesiaFacil.Tests.PoemTest
{
    [CollectionDefinition(nameof(PoemCollection))]
    public class PoemCollection : ICollectionFixture<PoemTestsFixture> { }

    public class PoemTestsFixture : IDisposable
    {
        public void Dispose()
        {
        }

        public CreatePoemInputModel GenerateValidPoemInputModel()
        {
            var poem = new Faker<CreatePoemInputModel>("pt_BR")
                .CustomInstantiator(f => new CreatePoemInputModel
                {
                    IsAnonymous = f.Random.Bool(),
                    Text = f.Lorem.Text(),
                    Title = f.Lorem.Slug()
                });

            return poem;
        }

        public CreatePoemInputModel GenerateInvalidPoemInputModel()
        {
            var poem = new Faker<CreatePoemInputModel>("pt_BR")
                .CustomInstantiator(f => new CreatePoemInputModel
                {
                    IsAnonymous = true,
                    Text = "",
                    Title = ""
                });

            return poem;
        }

    }
}
