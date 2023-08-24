using CodeAssessmentTest.Models;

namespace CodeAssessmentTest.Repositories.Interfaces
{
    public interface IBarRepository
    {
        Task CreateBarAsync(Bar bar);
        Task UpdateBaryAsync(int id, Bar bar);
        Task<List<Bar>> GetAllBarAsync();
        Task<Bar> GetBarById(int id);
    }
}
