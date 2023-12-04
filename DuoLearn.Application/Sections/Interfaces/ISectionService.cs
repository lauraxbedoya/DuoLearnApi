using DuoLearn.Domain;
using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ISectionService
    {
        IEnumerable<Section> GetAllSections();
        // Section? GetSectionById(int id);
        Section Create(CreateSectionDto section);
        Task<Result<Section>> UpdateAsync(UpdateSectionDto section, int id);
        Task<Result> RemoveAsync(int id);
        IList<Section> GetSectionLanguageById(int languageId);
    }
}