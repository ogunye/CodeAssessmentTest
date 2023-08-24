namespace CodeAssessmentTest.Models
{
    public class Bar
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<Beer>? Beers { get; set; }
    }
}
