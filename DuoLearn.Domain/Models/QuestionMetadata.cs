namespace DuoLearn.Domain.Models
{
    public class QuestionMetadata
    {
        public string? Answer { get; set; }
        public string[]? Options { get; set; }
        public string[]? Answers { get; set; }
        public PairMetadata[]? Pairs { get; set; }
    }

    public class PairMetadata {
        public required string  Word { get; set; }
        public required string Match { get; set; }
    }
}