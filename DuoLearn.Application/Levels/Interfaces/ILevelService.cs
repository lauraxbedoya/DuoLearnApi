using DuoLearn.Domain;
using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ILevelService
    {
        IEnumerable<Level> GetAllLevels();
        IList<Level> GetLevelSectionById(int sectionId);
        Level Create(CreateLevelDto level);
        Task<Result<Level>> UpdateAsync(UpdateLevelDto level, int id);
        Task<Result> RemoveAsync(int id);
    }
}