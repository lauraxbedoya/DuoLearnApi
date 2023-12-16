using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using DuoLearn.Domain.Enums;

namespace DuoLearn.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public required string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        [NotMapped]
        public string Type
        {
            get { return QuestionType.ToString(); }
            set { QuestionType = (QuestionType)Enum.Parse(typeof(QuestionType), value); }
        }
        public required string Feedback { get; set; }
        public required int Order { get; set; }

        public Lesson Lesson { get; set; } = null!;
        public JsonDocument Metadata { get; set; } = null!;
    }
}