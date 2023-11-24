using MongoDB.Driver;
using PoesiaFacil.Data.Context;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities.Contracts;
using System.Linq.Expressions;

namespace PoesiaFacil.Data.Repositories
{
    public class BaseRepository<T> : DataContext, IBaseRepository<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseRepository(string collection) : base(collection)
        {
            _collection = this._mongoDatabase.GetCollection<T>(this._collectionName);
        }

        public async Task CreateAsync(T document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task UpdateAsync(T document)
        {
            await this.UpdateAsync(document);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllWithParamsAsync(Expression<Func<T, bool>> filter)
        {
            var poems = await _collection
                .Find(filter)
                .ToListAsync();

            return poems;
        }

        public async Task<T> GetWithParamsAsync(Expression<Func<T, bool>> filter)
        {
            var document = await _collection
                .Find(filter)
                .FirstOrDefaultAsync();

            if (document is null)
                throw new Exception();

            return document;
        }
    }
}
