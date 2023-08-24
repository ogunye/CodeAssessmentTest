using CodeAssessmentTest.Models;

namespace CodeAssessmentTest.Repositories.Interfaces
{
    public interface IBeerRepository
    {
        Task<Beer> GetBeerByIdAsync(int id);        
        Task<List<Beer>> GetBeersByAlcoholContentAsync(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume);
        Task CreateBeerAsync(Beer beer);
        Task UpdateBeerAsync(int id, Beer beer);
    }
}
