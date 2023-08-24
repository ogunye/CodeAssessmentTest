using CodeAssessmentTest.Data;
using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessmentTest.Repositories
{
    public class BarBeerRepository : IBarBeerRepository
    {
        private readonly ApplicationDbContext _context;

        public BarBeerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateBarBeerAsync(int barId, int beerId)
        {
            var bar = await _context.Bars.FindAsync(barId);
            var beer = await _context.Beers.FindAsync(beerId);

            if(bar != null && beer != null)
            {
                if(bar.Beers is null)
                {
                    bar.Beers = new List<Beer>();
                }

                bar.Beers.Add(beer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Bar>> GetAllBarsWithBeersAsync() => 
            await _context.Bars.Include(b => b.Beers).ToListAsync();

        public async Task<Bar> GetBeersByBarIdAsync(int barId)
        {
            var bar = await _context.Bars.Include(b => b.Beers).FirstOrDefaultAsync(b => b.Id == barId);
            
            return bar;
        }
    }
}
