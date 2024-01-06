using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Helpers;
using PoesiaFacil.Models.InputModels.User;
using PoesiaFacil.Models.ViewModels.User;
using PoesiaFacil.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoesiaFacil.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IUserService userService, IPasswordHasher<User> passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _userService = userService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await _userRepository.GetAllWithParamsAsync(x => true);

            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        [HttpGet("{id}")]
        public async Task<UserViewModel> GetAsync(string id)
        {
            var user = await _userRepository.GetWithParamsAsync(x => x.Id == id);

            return _mapper.Map<UserViewModel>(user);
        }

        [HttpPost]
        public async Task PostAsync([FromBody] CreateUserInputModel user)
        {
            await _userService.CreateUser(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> SignInAsync(string email, string password)
        {
            try
            {
                User user = await _userRepository.GetWithParamsAsync(x => x.Email == email);

                var userMapped = _mapper.Map<UserViewModel>(user);

                if (user == null)
                    throw new Exception("Usuário não encontrado");

                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (result == PasswordVerificationResult.Failed)
                    throw new Exception("Falha na verificação das esnhas");

                var token = JwtTokenHelper.GetToken(user);

                return new { token, userMapped };
            }
            catch(Exception ex)
            {
                return new { };
            }
            
        }

        [HttpPut("{id}")]
        public async Task DeactivateAsync(string id)
        {
            await _userRepository.DeactivateAsync(id);
        }

        [HttpPut("{id}")]
        public async Task ActivateAsync(string id)
        {
            await _userRepository.DeactivateAsync(id);
        }
    }
}
