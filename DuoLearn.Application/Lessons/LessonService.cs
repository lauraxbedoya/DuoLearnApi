using DuoLearn.Application.Interfaces;
using DuoLearn.Domain;
using DuoLearn.Domain.Models;
using DuoLearn.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DuoLearn.Application
{
    public class LessonServices : ILessonService
    {
        private readonly AppDbContext _context;
        public LessonServices(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lesson> GetAllLessons()
        {
            var lessons = _context.Lessons.ToList();
            return lessons;
        }

        public IList<Lesson> GetLessonLevelById(int levelId)
        {
            return _context.Lessons.Where(x => x.LevelId == levelId).ToList();
        }

        public Lesson Create(CreateLessonDto level)
        {
            var lessonEntity = new Lesson()
            {
                LevelId = level.LevelId,
                Experience = level.Experience,
                PracticeExperience = level.PracticeExperience,
                isPractice = level.isPractice,
                Order = level.Order,

            };
            _context.Lessons.Add(lessonEntity);
            _context.SaveChanges();

            return lessonEntity;
        }

        public async Task<Result<Lesson>> UpdateAsync(UpdateLessonDto lesson, int id)
        {
            var currentLesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
            if (currentLesson == null)
            {
                return Result.Failure<Lesson>(LessonErrors.NotLessonFound);
            }

            currentLesson.Experience = lesson.Experience ?? currentLesson.Experience;
            currentLesson.PracticeExperience = lesson.PracticeExperience ?? currentLesson.PracticeExperience;
            currentLesson.isPractice = lesson.isPractice ?? currentLesson.isPractice;
            currentLesson.Order = lesson.Order ?? currentLesson.Order;
            _context.SaveChanges();

            return Result.Success(currentLesson);
        }

        public async Task<Result> RemoveAsync(int id)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync((less) => less.Id == id);
            if (lesson is null)
            {
                return Result.Failure<Lesson>(LessonErrors.NotLessonFound);
            }
            _context.Remove(lesson);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}

public static class LessonErrors
{
    public static readonly Error NotLessonFound = new("Lesson Not Found");
}