using CodeAssessmentTest.Data;
using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessmentTest.Repositories
{
    public class BreweryBeerRepository : IBreweryBeerRepository
    {
        private readonly ApplicationDbContext _context;

        public BreweryBeerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateBreweryBeerAsync(int breweryId, int beerId)
        {
            var brewery = await _context.Breweries.FindAsync(breweryId);
            var beer = await _context.Beers.FindAsync(beerId);

            if(brewery != null && beer != null)
            {
                if (brewery.Beers == null)
                {
                    brewery.Beers = new List<Beer>(); 
                }

                brewery.Beers.Add(beer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Brewery>> GetAllBreweriesWithBeersAsync() => 
            await _context.Breweries
            .Include(b => b.Beers).ToListAsync();

        public async Task<List<Beer>> GetBeersByBreweryIdAsync(int breweryId) => 
            await _context.Beers
            .Include(b => b.Brewery)
                .Where(b => b.BreweryId == breweryId)
                .ToListAsync();
    }
}
