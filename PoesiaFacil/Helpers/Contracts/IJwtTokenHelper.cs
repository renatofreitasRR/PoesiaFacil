using PoesiaFacil.Entities;

namespace PoesiaFacil.Helpers.Contracts
{
    public interface IJwtTokenHelper
    {
        string GetToken(User user);
    }
}
