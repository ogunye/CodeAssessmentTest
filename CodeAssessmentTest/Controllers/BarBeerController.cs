using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeAssessmentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarBeerController : ControllerBase
    {
        private readonly IBarBeerRepository _barBeerRepository;

        public BarBeerController(IBarBeerRepository barBeerRepository)
        {
            _barBeerRepository = barBeerRepository;
        }

        [HttpPost("beer")]
        public async Task<IActionResult> CreateBarBeerLink(int barId, int beerId)
        {
            await _barBeerRepository.CreateBarBeerAsync(barId, beerId);
            return Ok();
        }

        [HttpGet("{barId}/beer")]
        public async Task<ActionResult<Bar>> GetBarWithBeers(int barId)
        {
            var bar = await _barBeerRepository.GetBeersByBarIdAsync(barId);
            if (bar == null)
            {
                return NotFound();
            }
            return Ok(bar);
        }

        [HttpGet("beer")]
        public async Task<ActionResult<List<Bar>>> GetAllBarsWithBeers()
        {
            var bars = await _barBeerRepository.GetAllBarsWithBeersAsync();
            return Ok(bars);
        }

    }
}
