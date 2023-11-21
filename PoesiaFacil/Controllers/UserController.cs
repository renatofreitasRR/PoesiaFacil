using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Helpers;
using PoesiaFacil.Models.InputModels.User;
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
        private readonly IHttpContextAccessor _context;
        public UserController(IUserRepository userRepository, IUserService userService, IPasswordHasher<User> passwordHasher, IHttpContextAccessor context)
        {
            _userRepository = userRepository;
            _userService = userService;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        // GET: api/<UserController>
        [HttpGet()]
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        // GET: api/<UserController>
        [HttpGet("{id}")]
        public async Task<User> GetAsync(string id)
        {
            return await _userRepository.GetAsync(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task PostAsync([FromBody] CreateUserInputModel user)
        {
            await _userService.CreateUser(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> SignInAsync(string email, string password)
        {
            User user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if(result == PasswordVerificationResult.Failed)
                throw new Exception("Falha na verificação das esnhas");

            var token = JwtTokenHelper.GetToken(user);

            return new { token, user };
        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task DeactivateAsync(string id)
        {
            await _userRepository.DeactivateAsync(id);
        }


        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task ActivateAsync(string id)
        {
            await _userRepository.DeactivateAsync(id);
        }
    }
}
