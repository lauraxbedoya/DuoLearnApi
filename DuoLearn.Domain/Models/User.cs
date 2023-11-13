namespace DuoLearn.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ProfileImage { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Role { get; set; }
        public required bool Active { get; set; }

        public IList<UserLanguage> UserLanguages { get; } = new List<UserLanguage>();
    }
}