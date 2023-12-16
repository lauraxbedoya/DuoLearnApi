using System.ComponentModel.DataAnnotations;

namespace DuoLearn.Application;

public class CreateLessonDto
{
    public int LevelId { get; set; }
    public required string Experience { get; set; }
    public required int PracticeExperience { get; set; }
    public required bool isPractice { get; set; }
    public required int Order { get; set; }
}  


public class UpdateLessonDto
{
    public string? Experience { get; set; }
    public int? PracticeExperience { get; set; }
    public bool? isPractice { get; set; }
    public int? Order { get; set; }

}
