using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ISectionService
    {
        IEnumerable<Language> GetAllLanguages();
        Language Create(Language language);
    }
}