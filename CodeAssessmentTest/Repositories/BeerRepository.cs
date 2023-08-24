using CodeAssessmentTest.Data;
using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessmentTest.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly ApplicationDbContext _context;

        public BeerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateBeerAsync(Beer beer)
        {
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();
        }

        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            return beer ?? throw new DirectoryNotFoundException("Beer not found");
        }

        public async Task<List<Beer>> GetBeersByAlcoholContentAsync(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            var query = _context.Beers.AsQueryable();

            if (gtAlcoholByVolume.HasValue)
            {
                query = query.Where(b => b.PercentageAlcoholVolume > gtAlcoholByVolume.Value);
            }

            if (ltAlcoholByVolume.HasValue)
            {
                query = query.Where(b=> b.PercentageAlcoholVolume < ltAlcoholByVolume.Value);
            }

            return await query.ToListAsync();
        }

        public async Task UpdateBeerAsync(int id, Beer beer)
        {
            var getExistingBeer = await _context.Beers.FindAsync(id);
            if (getExistingBeer != null)
            {
                getExistingBeer.Name = beer.Name;
                getExistingBeer.PercentageAlcoholVolume = beer.PercentageAlcoholVolume;     
                
                await _context.SaveChangesAsync();
            }
        }
    }
}
