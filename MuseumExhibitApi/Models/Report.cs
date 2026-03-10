namespace MuseumExhibitApi.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int AlertId { get; set; }
        public string? Summary { get; set; }
        public string? Findings { get; set; }
        public string? ActionItems { get; set; }
    }
}