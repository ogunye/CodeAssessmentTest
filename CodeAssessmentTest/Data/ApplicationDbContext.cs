using CodeAssessmentTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessmentTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Bar> Bars { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>()
                .Property(b => b.PercentageAlcoholVolume)
                .HasColumnType("decimal(5, 2)");

           

            base.OnModelCreating(modelBuilder);
        }
    }
}
