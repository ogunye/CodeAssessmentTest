namespace CodeAssessmentTest.Models
{
    public class Brewery
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Beer>? Beers { get; set; }
    }
}
