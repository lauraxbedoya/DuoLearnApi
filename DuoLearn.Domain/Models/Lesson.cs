namespace DuoLearn.Domain.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public required string Experience { get; set; }
        public required int PracticeExperience { get; set; }
        public required bool isPractice { get; set; }
        public required int Order { get; set; }

        public Level Level { get; set; } = null!;
    }
}