using System.Linq.Expressions;

namespace PoesiaFacil.Data.Repositories.Contracts
{
    public interface IBaseRepository<T>
    {
        Task CreateAsync(T document);
        Task UpdateAsync(T document);
        Task DeleteAsync(string id);
        Task<IEnumerable<T>> GetAllWithParamsAsync(Expression<Func<T, bool>> filter);
        Task<T> GetWithParamsAsync(Expression<Func<T, bool>> filter);
        Task<bool> Existsync(Expression<Func<T, bool>> filter);
    }
}
