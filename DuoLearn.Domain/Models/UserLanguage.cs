namespace DuoLearn.Domain.Models
{
    public class UserLanguage
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int LanguageId { get; set; }

        public User User { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}