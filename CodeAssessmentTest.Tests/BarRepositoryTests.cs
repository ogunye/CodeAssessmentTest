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
    public class BarRepositoryTests
    {
        [Fact]
        public async Task CreateBarAsync_ValidBar_CreatesBarInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BarRepository(context);

            var newBar = new Bar
            {
                Name = "Test Bar",
                Address = "123 Test St"
            };

            // Act
            await repository.CreateBarAsync(newBar);

            // Assert
            var createdBar = await context.Bars.FirstOrDefaultAsync(b => b.Name == "Test Bar");
            Assert.NotNull(createdBar);
        }

        [Fact]
        public async Task GetAllBarAsync_ReturnsAllBars()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var repository = new BarRepository(context);

            var bars = new List<Bar>
            {
                new Bar { Name = "Bar 1", Address = "Address 1" },
                new Bar { Name = "Bar 2", Address = "Address 2" }
            };

            await context.Bars.AddRangeAsync(bars);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllBarAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}

