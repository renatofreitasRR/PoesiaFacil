using PoesiaFacil.Models;
using PoesiaFacil.Services.Contracts;
using System.Security.Claims;

namespace PoesiaFacil.Services.User
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _context;

        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public ContextUser GetUser()
        {
            var currentUser = _context.HttpContext.User;
            var currentUserId = currentUser.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value;
            var currentUserEmail = currentUser.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var currentUserName = currentUser.Claims.First(x => x.Type == ClaimTypes.Name).Value;

            return new ContextUser
            {
                Id = currentUserId,
                Name = currentUserName,
                Email = currentUserEmail,
            };
        }
    }
}
