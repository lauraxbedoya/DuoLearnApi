namespace DuoLearn.Domain.Models
{
    public class Section
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public required string Description { get; set; }
        public required string Color { get; set; }
        public required int Order { get; set; }
        public required bool Enabled { get; set; } = true;

        public Language Language { get; set; } = null!;
        public IList<Level> Levels { get; } = new List<Level>();
    }
}