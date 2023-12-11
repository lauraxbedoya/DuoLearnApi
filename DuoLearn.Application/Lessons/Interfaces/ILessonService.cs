
using DuoLearn.Domain;
using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ILessonService
    {
        IEnumerable<Lesson> GetAllLessons();
        IList<Lesson> GetLessonLevelById(int levelId);
        Lesson Create(CreateLessonDto level);
        Task<Result<Lesson>> UpdateAsync(UpdateLessonDto lesson, int id);
        Task<Result> RemoveAsync(int id);
    }
}