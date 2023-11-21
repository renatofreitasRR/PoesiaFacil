using PoesiaFacil.Entities;

namespace PoesiaFacil.Data.Repositories.Contracts
{
    public interface IPoemRepository
    {
        Task CreateAsync(Poem poem);
        Task UpdateAsync(Poem poem);
        Task DeleteAsync(string id);
        Task<IEnumerable<Poem>> GetAllByUserAsync(string id);
        Task<Poem> GetAsync(string id);
    }
}
