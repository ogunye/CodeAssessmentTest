using CodeAssessmentTest.Models;

namespace CodeAssessmentTest.Repositories.Interfaces
{
    public interface IBreweryBeerRepository
    {
        Task<List<Beer>> GetBeersByBreweryIdAsync(int breweryId);
        Task<List<Brewery>> GetAllBreweriesWithBeersAsync();
        Task CreateBreweryBeerAsync(int breweryId, int beerId);
    }
}
