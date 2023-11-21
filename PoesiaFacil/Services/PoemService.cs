using FluentValidation;
using FluentValidation.Results;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.InputModels.Poem;
using PoesiaFacil.Services.Contracts;
using PoesiaFacil.Validators;

namespace PoesiaFacil.Services
{
    public class PoemService : IPoemService
    {
        private readonly IPoemRepository _poemRepository;

        public PoemService(IPoemRepository poemRepository)
        {
            _poemRepository = poemRepository;
        }

        public async Task<bool> CreatePoem(CreatePoemInputModel poemInputModel)
        {
            Poem poem = new Poem
            {
                Title = poemInputModel.Title,
                Author = "No One",
                PublishDate = DateTime.Now,
                Text = poemInputModel.Text,
                IsAnonymous = false,
            };

            PoemValidator validator = new PoemValidator();

            ValidationResult result = validator.Validate(poem);

            if (result.IsValid)
                await _poemRepository.CreateAsync(poem);

            return false;
        }
    }
}
