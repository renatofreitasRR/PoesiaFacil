using MongoDB.Driver;
using PoesiaFacil.Data.Context;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;

namespace PoesiaFacil.Data.Repositories
{
    public class UserRepository : DataContext, IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository() : base("Users")
        {
            _userCollection = this._mongoDatabase.GetCollection<User>(this._collectionName);
        }

        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await this.UpdateAsync(user);
        }

        public async Task DeactivateAsync(string id)
        {
            var user = await _userCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user is null)
                throw new Exception();

            user.Deactivate();

            await this.UpdateAsync(user);
        }

        public async Task ActivateAsync(string id)
        {
            var user = await _userCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user is null)
                throw new Exception();

            user.Deactivate();

            await this.UpdateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _userCollection
                .Find(_ => true)
                .ToListAsync();

            return users;
        }

        public async Task<User> GetAsync(string id)
        {
            var user = await _userCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user is null)
                throw new Exception();

            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userCollection
                .Find(x => x.Email == email)
                .FirstOrDefaultAsync();

            if (user is null)
                throw new Exception();

            return user;
        }
    }
}
