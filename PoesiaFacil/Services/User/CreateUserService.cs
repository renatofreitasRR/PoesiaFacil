using Microsoft.AspNetCore.Identity;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Models.InputModels.User;
using PoesiaFacil.Services.Contracts;
using PoesiaFacil.Entities;

namespace PoesiaFacil.Services.User
{
    public class CreateUserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<Entities.User> _passwordHasher;

        public CreateUserService(IUserRepository userRepository, IPasswordHasher<Entities.User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> CreateUserAsync(CreateUserInputModel userInputModel)
        {
            Entities.User user = new Entities.User
            {
                Name = userInputModel.Name,
                Email = userInputModel.Email,
                Description = userInputModel.Description,
                ArtisticName = userInputModel.ArtisticName,
                Password = userInputModel.Password,
                IsActive = true,
            };

            user.Password = _passwordHasher.HashPassword(user, userInputModel.Password);

            if (user.IsValid())
                await _userRepository.CreateAsync(user);
            else
                return false;

            return true;
        }
    }
}
