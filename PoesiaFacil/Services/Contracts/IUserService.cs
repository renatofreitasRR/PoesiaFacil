using PoesiaFacil.Models.InputModels.User;

namespace PoesiaFacil.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(CreateUserInputModel userInputModel);
    }
}
