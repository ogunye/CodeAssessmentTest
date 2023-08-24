using CodeAssessmentTest.Models;

namespace CodeAssessmentTest.Repositories.Interfaces
{
    public interface IBreweryRepositroy
    {
        Task CreateBrewery(Brewery brewery);
        Task UpdateBrewery(int  id, Brewery brewery);
        Task<List<Brewery>> GetAllBreweryAsync();
        Task<Brewery> GetBreweryAsync(int id);
    }
}
