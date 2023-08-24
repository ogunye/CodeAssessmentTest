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
    public class BreweryRepositoryTests
    {
        [Fact]
        public async Task CreateBrewery_ValidBrewery_CreatesBreweryInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BreweryRepository(context);

            var newBrewery = new Brewery
            {
                Name = "Test Brewery"
            };

            // Act
            await repository.CreateBrewery(newBrewery);

            // Assert
            var createdBrewery = await context.Breweries.FirstOrDefaultAsync(b => b.Name == "Test Brewery");
            Assert.NotNull(createdBrewery);
        }

        [Fact]
        public async Task GetAllBreweryAsync_ReturnsAllBreweriesWithBeers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BreweryRepository(context);

            var breweries = new List<Brewery>
            {
                new Brewery { Name = "Brewery 1", Beers = new List<Beer>() },
                new Brewery { Name = "Brewery 2", Beers = new List<Beer>() }
            };

            var beers = new List<Beer>
            {
                new Beer { Name = "Beer 1" },
                new Beer { Name = "Beer 2" }
            };

            await context.Breweries.AddRangeAsync(breweries);
            await context.Beers.AddRangeAsync(beers);
            await context.SaveChangesAsync();

            var brewery1 = breweries[0];
            var brewery2 = breweries[1];
            brewery1.Beers.Add(beers[0]);
            brewery2.Beers.Add(beers[1]);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllBreweryAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.NotNull(result[0].Beers);
            Assert.NotNull(result[1].Beers);
        }


        [Fact]
        public async Task GetBreweryAsync_ExistingId_ReturnsBrewery()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BreweryRepository(context);

            var brewery = new Brewery { Name = "Test Brewery" };
            await context.Breweries.AddAsync(brewery);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetBreweryAsync(brewery.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Brewery", result.Name);
        }

        [Fact]
        public async Task UpdateBrewery_ValidBrewery_UpdatesBreweryInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BreweryRepository(context);

            var brewery = new Brewery { Name = "Old Brewery" };
            await context.Breweries.AddAsync(brewery);
            await context.SaveChangesAsync();

            // Act
            var updatedBrewery = new Brewery { Name = "Updated Brewery" };
            await repository.UpdateBrewery(brewery.Id, updatedBrewery);

            // Assert
            var fetchedBrewery = await context.Breweries.FirstOrDefaultAsync(b => b.Id == brewery.Id);
            Assert.NotNull(fetchedBrewery);
            Assert.Equal("Updated Brewery", fetchedBrewery.Name);
        }
    }
}
