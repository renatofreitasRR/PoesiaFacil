using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.InputModels.Poem;
using PoesiaFacil.Models.ViewModels;
using PoesiaFacil.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoesiaFacil.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PoemController : ControllerBase
    {
        public readonly IPoemRepository _poemRepository;
        public readonly IPoemService _poemService;

        public PoemController(IPoemRepository poemRepository, IPoemService poemService)
        {
            _poemRepository = poemRepository;
            _poemService = poemService;
        }

        [HttpGet]
        public async Task<IEnumerable<Poem>> GetAllAsync()
        {
            return await _poemRepository.GetAllWithParamsAsync(x => true);
        }

        [HttpGet]
        public async Task<Pagination<Poem>> GetAllPaginatedAsync(int page = 1, int size = 7, string search = "")
        {
            return await _poemRepository.GeAllPaginatedAsync(page, size, search);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Poem>> GetAllByUserAsync(string id)
        {
            return await _poemRepository.GetAllWithParamsAsync(x => x.UserId == id);
        }

        [HttpGet("{id}")]
        public async Task<Poem> GetAsync(string id)
        {
            return await _poemRepository.GetWithParamsAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task PostAsync([FromBody] CreatePoemInputModel poem)
        {
            await _poemService.CreatePoem(poem);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _poemRepository.DeleteAsync(id);
        }
    }
}
