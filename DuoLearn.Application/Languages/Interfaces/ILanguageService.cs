using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ILanguageService
    {
        IEnumerable<Language> GetAllLanguages();
        Language? GetLanguageById(int id);
        Language Create(Language language);
        Task<Language> Update(Language language, int id);
        bool Remove(int id);
    }
}