using PoesiaFacil.Entities;

namespace PoesiaFacil.Data.Repositories.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task DeactivateAsync(string id);
        Task ActivateAsync(string id);
    }
}
