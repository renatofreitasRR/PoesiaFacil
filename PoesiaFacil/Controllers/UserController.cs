using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoesiaFacil.Controllers.Contracts;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Helpers;
using PoesiaFacil.Models.InputModels.User;
using PoesiaFacil.Models.ViewModels.User;
using PoesiaFacil.Services.Contracts;
using System.Net;

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
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllWithParamsAsync(x => true);
            var usersMapped = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return new CustomActionResult(HttpStatusCode.OK, usersMapped);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var user = await _userRepository.GetWithParamsAsync(x => x.Id == id);

            if (user == null)
                return new CustomActionResult(HttpStatusCode.NotFound, "Usuário não encontrado");

            var userMapped = _mapper.Map<UserViewModel>(user);

            return new CustomActionResult(HttpStatusCode.OK, userMapped);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserInputModel user)
        {
            await _userService.CreateUser(user);

            return new CustomActionResult(HttpStatusCode.Created, user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync(string email, string password)
        {
            User user = await _userRepository.GetWithParamsAsync(x => x.Email == email);

            var userMapped = _mapper.Map<UserViewModel>(user);

            if (user == null)
                return new CustomActionResult(HttpStatusCode.NotFound, "Usuário não encontrado");

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Failed)
                return new CustomActionResult(HttpStatusCode.BadRequest, "Senha inválida");

            var token = JwtTokenHelper.GetToken(user);

            return new CustomActionResult(HttpStatusCode.OK, new { Token = token, User = userMapped });
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> DeactivateAsync(string id)
        {
            await _userRepository.DeactivateAsync(id);

            return new CustomActionResult(HttpStatusCode.OK);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> ActivateAsync(string id)
        {
            await _userRepository.DeactivateAsync(id);

            return new CustomActionResult(HttpStatusCode.OK);
        }
    }
}
