using CodeAssessmentTest.Models;

namespace CodeAssessmentTest.Repositories.Interfaces
{
    public interface IBarBeerRepository
    {
        Task<Bar> GetBeersByBarIdAsync(int barId);
        Task<List<Bar>> GetAllBarsWithBeersAsync();
        Task CreateBarBeerAsync(int barId, int beerId);
    }
}
