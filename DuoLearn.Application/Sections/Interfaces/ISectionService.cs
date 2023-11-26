using DuoLearn.Domain;
using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ISectionService
    {
        IEnumerable<Section> GetAllSections();
        Section? GetSectionById(int id);
        Section Create(CreateSectionDto section);
        Task<Result<Section>> Update(UpdateSectionDto section, int id);
        bool Remove(int id);
    }
}