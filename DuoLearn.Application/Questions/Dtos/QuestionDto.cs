using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using DuoLearn.Domain.Enums;

namespace DuoLearn.Application;

public class CreateQuestionDto
{
    public int LessonId { get; set; }
    public required string Text { get; set; }
    public required QuestionType Type { get; set; }
    public string? Feedback { get; set; } = null!;
    public required int Order { get; set; }
    public required JsonDocument Metadata { get; set; }
}


public class UpdateQuestionDto
{
    public string? Text { get; set; }
    public QuestionType? Type { get; set; }
    public string? Feedback { get; set; }
    public int? Order { get; set; }
}
