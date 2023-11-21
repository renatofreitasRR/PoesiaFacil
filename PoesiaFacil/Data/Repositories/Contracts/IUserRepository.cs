using PoesiaFacil.Entities;

namespace PoesiaFacil.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task DeactivateAsync(string id);
        Task ActivateAsync(string id);
    }
}
