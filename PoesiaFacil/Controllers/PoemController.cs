using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoesiaFacil.Controllers.Contracts;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Models.InputModels.Poem;
using PoesiaFacil.Services.Contracts;
using System.Net;

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
        public async Task<IActionResult> GetAllAsync()
        {
            var poems = await _poemRepository.GetAllWithParamsAsync(x => true);

            return new CustomActionResult(HttpStatusCode.OK, poems);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaginatedAsync(int page = 1, int size = 7, string search = "")
        {
            var poems = await _poemRepository.GeAllPaginatedAsync(page, size, search);

            return new CustomActionResult(HttpStatusCode.OK, poems);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetAllByUserAsync(string id)
        {
            var poems = await _poemRepository.GetAllWithParamsAsync(x => x.UserId == id);

            return new CustomActionResult(HttpStatusCode.OK, poems);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var poem = await _poemRepository.GetWithParamsAsync(x => x.Id == id);

            return new CustomActionResult(HttpStatusCode.OK, poem);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreatePoemInputModel poem)
        {
            await _poemService.CreatePoemAsync(poem);

            return new CustomActionResult(HttpStatusCode.Created, poem);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _poemRepository.DeleteAsync(id);

            return new CustomActionResult(HttpStatusCode.OK);
        }
    }
}
