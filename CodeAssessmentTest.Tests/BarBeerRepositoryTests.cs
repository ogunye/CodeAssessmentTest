using CodeAssessmentTest.Data;
using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessmentTest.Tests
{
    public class BarBeerRepositoryTests
    {
        [Fact]
        public async Task CreateBarBeerAsync_ValidBarAndBeer_CreatesAssociation()
        {
            // Arrange
            var barId = 1;
            var beerId = 2;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BarBeerRepository(context);

            var bar = new Bar { Id = barId, Beers = new List<Beer>() };
            var beer = new Beer { Id = beerId };

            context.Bars.Add(bar);
            context.Beers.Add(beer);
            await context.SaveChangesAsync();

            // Act
            await repository.CreateBarBeerAsync(barId, beerId);

            // Assert
            Assert.Contains(beer, bar.Beers);
            await context.SaveChangesAsync(); // Ensure changes are persisted

        }
    }
}
