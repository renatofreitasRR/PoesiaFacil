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
        private readonly IHttpContextAccessor _context;

        public PoemService(IPoemRepository poemRepository, IHttpContextAccessor context)
        {
            _poemRepository = poemRepository;
            _context = context;
        }

        public async Task<bool> CreatePoem(CreatePoemInputModel poemInputModel)
        {
            //var currentUser = _context.HttpContext.User;
            //var currentUserId = currentUser.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value;

            Poem poem = new Poem
            {
                Title = poemInputModel.Title,
                //Author = currentUser.Identity.Name,
                //UserId = currentUserId,
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
