using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.InputModels.Poem;
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

        // GET: api/<PoemController>
        [HttpGet("{id}")]
        public async Task<IEnumerable<Poem>> GetAllByUserAsync(string id)
        {
            return await _poemRepository.GetAllByUserAsync(id);
        }

        // GET api/<PoemController>/5
        [HttpGet("{id}")]
        public async Task<Poem> GetAsync(string id)
        {
            return await _poemRepository.GetAsync(id);
        }

        // POST api/<PoemController>
        [HttpPost]
        public async Task PostAsync([FromBody] CreatePoemInputModel poem)
        {
            await _poemService.CreatePoem(poem);
        }

        // DELETE api/<PoemController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _poemRepository.DeleteAsync(id);
        }
    }
}
