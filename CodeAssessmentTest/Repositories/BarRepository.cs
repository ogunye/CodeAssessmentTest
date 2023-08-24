using CodeAssessmentTest.Data;
using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessmentTest.Repositories
{
    public class BarRepository : IBarRepository
    {
        private readonly ApplicationDbContext _context;

        public BarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateBarAsync(Bar bar)
        {
            _context.Bars.Add(bar);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bar>> GetAllBarAsync() => 
            await _context.Bars.ToListAsync();

        public async Task<Bar> GetBarById(int id)
        {
            var bar = await _context.Bars.FindAsync(id);
            
            return bar;
        }

        public async Task UpdateBaryAsync(int id, Bar bar)
        {
            var existingBar = await _context.Bars.FindAsync(id);
            if(existingBar != null)
            {
                existingBar.Name = bar.Name;
                existingBar.Address = bar.Address;

                await _context.SaveChangesAsync();
            }
        }
    }
}
