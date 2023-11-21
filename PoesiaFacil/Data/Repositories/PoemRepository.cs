using MongoDB.Driver;
using PoesiaFacil.Data.Context;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;

namespace PoesiaFacil.Data.Repositories
{
    public class PoemRepository : DataContext, IPoemRepository
    {
        private readonly IMongoCollection<Poem> _poemCollection;

        public PoemRepository() : base("Poems")
        {
            _poemCollection = this._mongoDatabase.GetCollection<Poem>(this._collectionName);
        }

        public async Task CreateAsync(Poem poem)
        {
            await _poemCollection.InsertOneAsync(poem);
        }

        public async Task UpdateAsync(Poem poem)
        {
            await this.UpdateAsync(poem);
        }

        public async Task DeleteAsync(string id)
        {
            await _poemCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Poem>> GetAllByUserAsync(string id)
        {
            var poems = await _poemCollection
                .Find(x => x.Id == id)
                .ToListAsync();

            return poems;
        }

        public async Task<Poem> GetAsync(string id)
        {
            var poem = await _poemCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (poem is null)
                throw new Exception();

            return poem;
        }
    }
}
