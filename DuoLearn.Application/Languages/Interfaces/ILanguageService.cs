using DuoLearn.Domain;
using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ILanguageService
    {
        IEnumerable<Language> GetAllLanguages();
        Language? GetLanguageById(int id);
        Language Create(CreateLanguageDto language);
        Task<Result<Language>> UpdateAsync(UpdateLanguageDto language, int id);
        Task<Result> RemoveAsync(int id);
    }
}