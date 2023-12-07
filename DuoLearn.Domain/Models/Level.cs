namespace DuoLearn.Domain.Models
{
    public class Level
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string? ImageUrl { get; set; }
        public required string Type { get; set; }
        public required int Order { get; set; }
        public required bool Enabled { get; set; }

        public Section Section { get; set; } = null!;
    }
}