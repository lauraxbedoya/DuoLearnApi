using DuoLearn.Domain;
using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetAllQuestions();
        IList<Question> GetQuestionLessonById(int lessonId);
        Question Create(CreateQuestionDto question);
        Task<Result<Question>> UpdateAsync(UpdateQuestionDto question, int id);
        Task<Result> RemoveAsync(int id);
    }
}