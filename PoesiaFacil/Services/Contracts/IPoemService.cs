using PoesiaFacil.Models.InputModels.Poem;

namespace PoesiaFacil.Services.Contracts
{
    public interface IPoemService
    {
        Task<bool> CreatePoemAsync(CreatePoemInputModel poemInputModel);
    }
}
