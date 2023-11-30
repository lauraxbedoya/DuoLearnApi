using DuoLearn.Domain;
using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ILanguageService
    {
        IEnumerable<Language> GetAllLanguages();
        Language? GetLanguageById(int id);
        Language Create(Language language);
        Task<Result<Language>> UpdateAsync(Language language, int id);
        Task<Result> RemoveAsync(int id);
    }
}