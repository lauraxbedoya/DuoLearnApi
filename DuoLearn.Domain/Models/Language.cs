namespace DuoLearn.Domain.Models
{
    public class Language
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? FlagUrl { get; set; }

        public IList<UserLanguage> UserLanguages { get; } = new List<UserLanguage>();
        public IList<User> Users { get; } = new List<User>();
        public IList<Section> Sections { get; } = new List<Section>();
    }
}