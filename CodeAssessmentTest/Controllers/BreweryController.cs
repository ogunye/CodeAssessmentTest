using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeAssessmentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private readonly IBreweryRepositroy _breweryRepository;

        public BreweryController(IBreweryRepositroy breweryRepositroy)
        {
            _breweryRepository = breweryRepositroy;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrewery(Brewery brewery)
        {
            await _breweryRepository.CreateBrewery(brewery);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrewery(int id, Brewery brewery)
        {
            await _breweryRepository.UpdateBrewery(id, brewery);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Brewery>>> GetAllBreweries()
        {
            var breweries = await _breweryRepository.GetAllBreweryAsync();
            return Ok(breweries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brewery>> GetBrewery(int id)
        {
            var brewery = await _breweryRepository.GetBreweryAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return Ok(brewery);
        }
    }
}
