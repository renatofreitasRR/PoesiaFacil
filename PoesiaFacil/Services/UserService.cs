using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using PoesiaFacil.Data.Repositories;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.InputModels.User;
using PoesiaFacil.Services.Contracts;
using PoesiaFacil.Validators;

namespace PoesiaFacil.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> CreateUser(CreateUserInputModel userInputModel)
        {
            User user = new User
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
