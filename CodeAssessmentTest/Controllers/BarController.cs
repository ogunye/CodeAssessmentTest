using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeAssessmentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarController : ControllerBase
    {
        private readonly IBarRepository _barRepository;

        public BarController(IBarRepository barRepository)
        {
            _barRepository = barRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBar(Bar bar)
        {
            await _barRepository.CreateBarAsync(bar);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBar(int id, Bar bar)
        {
            await _barRepository.UpdateBaryAsync(id, bar);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Bar>>> GetAllBars()
        {
            var bars = await _barRepository.GetAllBarAsync();
            return Ok(bars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bar>> GetBar(int id)
        {
            var bar = await _barRepository.GetBarById(id);
            if (bar == null)
            {
                return NotFound();
            }
            return Ok(bar);
        }
    }
}
