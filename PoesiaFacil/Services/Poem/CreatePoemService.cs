using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Models.InputModels.Poem;
using PoesiaFacil.Services.Contracts;

namespace PoesiaFacil.Services.Poem
{
    public class CreatePoemService : IPoemService
    {
        private readonly IPoemRepository _poemRepository;
        private readonly ICurrentUserService _userService;

        public CreatePoemService(IPoemRepository poemRepository, ICurrentUserService userService)
        {
            _poemRepository = poemRepository;
            _userService = userService;
        }

        public async Task<bool> CreatePoemAsync(CreatePoemInputModel poemInputModel)
        {
            var currentUser = _userService.GetUser();

            Entities.Poem poem = new Entities.Poem
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
