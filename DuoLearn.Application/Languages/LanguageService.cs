using DuoLearn.Domain.Models;
using System.Linq;
using DuoLearn.Infrastructure.Context;
using DuoLearn.Domain.Enums;
using DuoLearn.Application.Interfaces;
using DuoLearn.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

        public Language Create(Language language)
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

        public async Task<Language> Update(Language language, int id)
        {
            var currentLanguage = await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
            if (currentLanguage == null)
            {
                return null;
            }
            currentLanguage.Name = language.Name;
            currentLanguage.FlagUrl = language.FlagUrl;
            _context.SaveChanges();
            return currentLanguage;
        }

        public bool Remove(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
            return true;
        }
    }
}