using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface ILanguageService
    {
        Language Create(Language language);
    }
}