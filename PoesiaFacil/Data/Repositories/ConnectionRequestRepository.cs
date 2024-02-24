using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;

namespace PoesiaFacil.Data.Repositories
{
    public class ConnectionRequestRepository : BaseRepository<ConnectionRequest>, IConnectionRequestRepository
    {
        public ConnectionRequestRepository() : base("ConnectionRequest")
        {

        }
    

    }
}
