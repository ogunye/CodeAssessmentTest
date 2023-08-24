using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeAssessmentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerRepository _beerRepository;
        public BeerController(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBeer(Beer beer)
        {
            await _beerRepository.CreateBeerAsync(beer);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, Beer beer)
        {
            await _beerRepository.UpdateBeerAsync(id, beer);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Beer>>> GetBeers(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            var beers = await _beerRepository.GetBeersByAlcoholContentAsync(gtAlcoholByVolume, ltAlcoholByVolume);
            return Ok(beers);
        }        
    }
}
