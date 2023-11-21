using Microsoft.AspNetCore.Authentication.Cookies;
using PoesiaFacil.Entities;
using System.Security.Claims;

namespace PoesiaFacil.Helpers
{
    public static class UserClaimsHelper
    {
        public static ClaimsPrincipal Convert(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("name", user.Name),
                new Claim("email", user.Email),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}
