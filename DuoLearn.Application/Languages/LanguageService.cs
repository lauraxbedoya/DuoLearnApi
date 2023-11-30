using DuoLearn.Domain.Models;
using System.Linq;
using DuoLearn.Infrastructure.Context;
using DuoLearn.Domain.Enums;
using DuoLearn.Application.Interfaces;
using DuoLearn.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using DuoLearn.Domain;

namespace DuoLearn.Applications
{
    public class LanguageServices : ILanguageService
    {
        private readonly AppDbContext _context;

        public LanguageServices(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Language> GetAllLanguages()
        {
            var languages = _context.Languages.ToList();
            return languages;
        }

        public Language? GetLanguageById(int id)
        {
            return _context.Languages.FirstOrDefault(x => x.Id == id);
        }

        public Language Create(CreateLanguageDto language)
        {
            var languageEntity = new Language()
            {
                Name = language.Name,
                FlagUrl = language.FlagUrl,

            };
            _context.Languages.Add(languageEntity);
            _context.SaveChanges();

            return languageEntity;
        }

        public async Task<Result<Language>> UpdateAsync(UpdateLanguageDto language, int id)
        {
            var currentLanguage = await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
            if (currentLanguage is null)
            {
                return Result.Failure<Language>(LanguageErrors.NotLanguageFound);
            }

            currentLanguage.Name = language.Name ?? currentLanguage.Name;
            currentLanguage.FlagUrl = language.FlagUrl ?? currentLanguage.FlagUrl;
            _context.SaveChanges();

            return Result.Success(currentLanguage);
        }

        public async Task<Result> RemoveAsync(int id)
        {
            var language = await _context.Languages.FirstOrDefaultAsync((lang) => lang.Id == id);
            if (language is null)
            {
                return Result.Failure(LanguageErrors.NotLanguageFound);
            }
            _context.Remove(language);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}

public static class LanguageErrors
{
    public static readonly Error NotLanguageFound = new("Language Not Found");
}