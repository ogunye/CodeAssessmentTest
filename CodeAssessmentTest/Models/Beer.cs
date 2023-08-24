using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeAssessmentTest.Models
{
    public class Beer
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal PercentageAlcoholVolume { get; set; }

        [ForeignKey(nameof(Brewery))]
        public int BreweryId { get; set; }
        public Brewery? Brewery { get; set; }
    }
}
