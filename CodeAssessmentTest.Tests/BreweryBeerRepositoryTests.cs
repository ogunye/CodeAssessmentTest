using CodeAssessmentTest.Data;
using CodeAssessmentTest.Models;
using CodeAssessmentTest.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAssessmentTest.Tests
{
    public class BreweryBeerRepositoryTests
    {
        [Fact]
        public async Task CreateBreweryBeerAsync_ValidBreweryAndBeer_CreatesAssociation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BreweryBeerRepository(context);

            var brewery = new Brewery { Name = "Test Brewery" };
            var beer = new Beer { Name = "Test Beer" };

            await context.Breweries.AddAsync(brewery);
            await context.Beers.AddAsync(beer);
            await context.SaveChangesAsync();

            // Act
            await repository.CreateBreweryBeerAsync(brewery.Id, beer.Id);

            // Assert
            var updatedBrewery = await context.Breweries
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(b => b.Id == brewery.Id);

            Assert.Contains(beer, updatedBrewery.Beers);
        }

        [Fact]
        public async Task GetAllBreweriesWithBeersAsync_ReturnsBreweriesWithBeers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BreweryBeerRepository(context);

            var brewery = new Brewery { Name = "Test Brewery" };
            var beer = new Beer { Name = "Test Beer" };

            brewery.Beers = new List<Beer> { beer }; 

            await context.Breweries.AddAsync(brewery);
            await context.Beers.AddAsync(beer);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllBreweriesWithBeersAsync();

            // Assert
            Assert.Single(result); 
            Assert.Single(result[0].Beers); 
            Assert.Equal("Test Beer", result[0].Beers[0].Name);
        }

        [Fact]
        public async Task GetBeersByBreweryIdAsync_ReturnsBeersByBreweryId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BreweryBeerRepository(context);

            var brewery = new Brewery { Name = "Test Brewery" };
            var beer1 = new Beer { Name = "Beer 1" };
            var beer2 = new Beer { Name = "Beer 2" };

            brewery.Beers = new List<Beer> { beer1, beer2 }; // Initialize the Beers property

            await context.Breweries.AddAsync(brewery);
            await context.Beers.AddRangeAsync(beer1, beer2);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetBeersByBreweryIdAsync(brewery.Id);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, beer => beer.Name == "Beer 1" || beer.Name == "Beer 2");
        }

    }
}
