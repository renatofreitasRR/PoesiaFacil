using MongoDB.Driver;
using PoesiaFacil.Data.Context;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;

namespace PoesiaFacil.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository() : base("Users"){}

        public async Task DeactivateAsync(string id)
        {
            var user = await _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user is null)
                throw new Exception();

            user.Deactivate();

            await this.UpdateAsync(user);
        }

        public async Task ActivateAsync(string id)
        {
            var user = await _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user is null)
                throw new Exception();

            user.Deactivate();

            await this.UpdateAsync(user);
        }
    }
}
