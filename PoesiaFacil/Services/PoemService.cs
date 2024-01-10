using FluentValidation;
using FluentValidation.Results;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.InputModels.Poem;
using PoesiaFacil.Services.Contracts;
using PoesiaFacil.Validators;
using System.Security.Claims;

namespace PoesiaFacil.Services
{
    public class PoemService : IPoemService
    {
        private readonly IPoemRepository _poemRepository;
        private readonly ICurrentUserService _userService;

        public PoemService(IPoemRepository poemRepository, ICurrentUserService userService)
        {
            _poemRepository = poemRepository;
            _userService = userService;
        }

        public async Task<bool> CreatePoem(CreatePoemInputModel poemInputModel)
        {
            var currentUser = _userService.GetUser();

            Poem poem = new Poem
            {
                Title = poemInputModel.Title,
                Author = currentUser.Name,
                UserId = currentUser.Id,
                PublishDate = DateTime.Now,
                Text = poemInputModel.Text,
                IsAnonymous = false,
            };

            if (poem.IsValid())
                await _poemRepository.CreateAsync(poem);
            else
                return false;

            return true;
        }
    }
}
