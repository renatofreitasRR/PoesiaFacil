using PoesiaFacil.Models;

namespace PoesiaFacil.Services.Contracts
{
    public interface ICurrentUserService
    {
        ContextUser GetUser();
    }
}
