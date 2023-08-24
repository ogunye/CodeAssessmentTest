using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeAssessmentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryBeerController : ControllerBase
    {
        private readonly IBreweryBeerRepository _breweryBeerRepository;

        public BreweryBeerController(IBreweryBeerRepository breweryBeerRepository)
        {
            _breweryBeerRepository = breweryBeerRepository;
        }

        [HttpPost("beer")]
        public async Task<IActionResult> CreateBreweryBeerLink(int breweryId, int beerId)
        {
            await _breweryBeerRepository.CreateBreweryBeerAsync(breweryId, beerId);
            return Ok();
        }

        [HttpGet("{breweryId}/beer")]
        public async Task<ActionResult<Brewery>> GetBreweryWithBeers(int breweryId)
        {
            var brewery = await _breweryBeerRepository.GetBeersByBreweryIdAsync(breweryId);
            if (brewery == null)
            {
                return NotFound();
            }
            return Ok(brewery);
        }

        [HttpGet("beer")]
        public async Task<ActionResult<List<Brewery>>> GetAllBreweriesWithBeers()
        {
            var breweries = await _breweryBeerRepository.GetAllBreweriesWithBeersAsync();
            return Ok(breweries);
        }
    }
}
