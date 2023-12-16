using DuoLearn.Application.Interfaces;
using DuoLearn.Domain;
using DuoLearn.Domain.Models;
using DuoLearn.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DuoLearn.Application
{
    public class LevelServices : ILevelService
    {
        private readonly AppDbContext _context;
        public LevelServices(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Level> GetAllLevels()
        {
            var levels = _context.Levels.ToList();
            return levels;
        }

        public IList<Level> GetLevelSectionById(int sectionId)
        {
            return _context.Levels.Where(x => x.SectionId == sectionId).ToList();
        }

        public Level Create(CreateLevelDto level)
        {
            var levelEntity = new Level()
            {
                SectionId = level.SectionId,
                Title = level.Title,
                Description = level.Description,
                ImageUrl = level.ImageUrl,
                Type = level.Type,
                Order = level.Order,
                Enabled = level.Enabled,

            };
            _context.Levels.Add(levelEntity);
            _context.SaveChanges();

            return levelEntity;
        }

        public async Task<Result<Level>> UpdateAsync(UpdateLevelDto level, int id)
        {
            var currentLevel = await _context.Levels.FirstOrDefaultAsync(x => x.Id == id);
            if (currentLevel == null)
            {
                return Result.Failure<Level>(LevelErrors.NotLevelFound);
            }

            currentLevel.Title = level.Title ?? currentLevel.Title;
            currentLevel.Description = level.Description ?? currentLevel.Description;
            currentLevel.ImageUrl = level.ImageUrl ?? currentLevel.ImageUrl;
            currentLevel.Type = level.Type ?? currentLevel.Type;
            currentLevel.Order = level.Order ?? currentLevel.Order;
            currentLevel.Enabled = level.Enabled ?? currentLevel.Enabled;
            _context.SaveChanges();

            return Result.Success(currentLevel);
        }

        public async Task<Result> RemoveAsync(int id)
        {
            var level = await _context.Levels.FirstOrDefaultAsync((lev) => lev.Id == id);
            if (level is null)
            {
                return Result.Failure<Level>(LevelErrors.NotLevelFound);
            }
            _context.Remove(level);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}

public static class LevelErrors
{
    public static readonly Error NotLevelFound = new("Level Not Found");
}