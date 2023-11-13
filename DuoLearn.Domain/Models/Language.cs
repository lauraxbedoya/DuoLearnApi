namespace DuoLearn.Domain.Models
{
    public class Language
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FlagUrl { get; set; }

        public IList<UserLanguage> UserLanguages { get; } = new List<UserLanguage>();
    }
}