using CodeAssessmentTest.Data;
using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessmentTest.Repositories
{
    public class BreweryRepository : IBreweryRepositroy
    {
        private readonly ApplicationDbContext _context;

        public BreweryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateBrewery(Brewery brewery)
        {
            _context.Breweries.Add(brewery);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Brewery>> GetAllBreweryAsync()
        {
            return await _context.Breweries.Include(b => b.Beers).ToListAsync();
        }

        public async Task<Brewery> GetBreweryAsync(int id)
        {
            var getBerwery = await _context.Breweries.FindAsync(id);
            return getBerwery;
        }

        public async Task UpdateBrewery(int id, Brewery brewery)
        {
            var existingBrewery = await _context.Breweries.FindAsync(id);

            if (existingBrewery != null)
            {
                existingBrewery.Name = brewery.Name;

                await _context.SaveChangesAsync();
            }
        }
    }
}
