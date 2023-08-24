using CodeAssessmentTest.Data;
using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessmentTest.Tests
{
    public class BeerRepositoryTests
    {
        [Fact]
        public async Task CreateBeerAsync_ValidBeer_CreatesBeerInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BeerRepository(context);

            var newBeer = new Beer
            {
                Name = "Test Beer",
                PercentageAlcoholVolume = 5.0m
            };

            // Act
            await repository.CreateBeerAsync(newBeer);

            // Assert
            var createdBeer = await context.Beers.FirstOrDefaultAsync(b => b.Name == "Test Beer");
            Assert.NotNull(createdBeer);
        }

        [Fact]
        public async Task GetBeerByIdAsync_ExistingId_ReturnsBeer()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BeerRepository(context);

            var beer = new Beer { Name = "Test Beer", PercentageAlcoholVolume = 4.5m };
            await context.Beers.AddAsync(beer);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetBeerByIdAsync(beer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Beer", result.Name);
        }

        [Fact]
        public async Task GetBeersByAlcoholContentAsync_ReturnsFilteredBeers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BeerRepository(context);

            var beers = new List<Beer>
            {
                new Beer { Name = "Beer 1", PercentageAlcoholVolume = 4.0m },
                new Beer { Name = "Beer 2", PercentageAlcoholVolume = 5.5m },
                new Beer { Name = "Beer 3", PercentageAlcoholVolume = 6.0m },
                new Beer { Name = "Beer 4", PercentageAlcoholVolume = 8.5m }
            };

            await context.Beers.AddRangeAsync(beers);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetBeersByAlcoholContentAsync(5.0m, 8.0m);

            // Assert
            Assert.Equal(2, result.Count); 
            Assert.Contains(result, beer => beer.Name == "Beer 2");
            Assert.Contains(result, beer => beer.Name == "Beer 3");
        }

    }
}

