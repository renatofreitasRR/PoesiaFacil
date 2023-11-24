using PoesiaFacil.Entities;
using PoesiaFacil.Models.ViewModels;

namespace PoesiaFacil.Data.Repositories.Contracts
{
    public interface IPoemRepository : IBaseRepository<Poem>
    {
        Task<Pagination<Poem>> GeAllPaginatedAsync(int page = 1, int size = 10, string search = "");
    }
}
