using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Models.InputModels.Poem;
using PoesiaFacil.Services.Contracts;

namespace PoesiaFacil.Services.Connection
{
    public class ConnectionRequestService: IConnectionRequestService
    {
        private readonly IConnectionRequestRepository _connectionRequestRepository;
        private readonly IUserRepository _userRepository;

        public ConnectionRequestService(IConnectionRequestRepository connectionRequestRepository, IUserRepository userRepository)
        {
            _connectionRequestRepository = connectionRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> SendRequestAsync(string userRequesterId, string userRequestedId)
        {
            var requesterExists = await _userRepository.Existsync(x => x.Id == userRequesterId);
            if (requesterExists is false)
                throw new Exception();

            var requestedExists = await _userRepository.Existsync(x => x.Id == userRequestedId);
            if(requestedExists is false) 
                throw new Exception();

            var connectionExists = await _connectionRequestRepository
                .Existsync(x => x.UserRequesterId == userRequesterId && x.UserRequestedId == userRequesterId);

            if (connectionExists is true)
                throw new Exception();


            throw new NotImplementedException();
        }
    }
}
