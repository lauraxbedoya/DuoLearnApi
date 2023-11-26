using DuoLearn.Application;
using DuoLearn.Application.Interfaces;
using DuoLearn.Domain;
using DuoLearn.Domain.Models;
using DuoLearn.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DuoLearn.Applications
{
    public class SectionServices : ISectionService
    {
        private readonly AppDbContext _context;

        public SectionServices(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Section> GetAllSections()
        {
            var sections = _context.Sections.ToList();
            return sections;
        }

        public Section? GetSectionById(int id)
        {
            return _context.Sections.FirstOrDefault(x => x.Id == id);
        }

        public Section Create(CreateSectionDto section)
        {
            var sectionEntity = new Section()
            {
                LanguageId = section.LanguageId,
                Description = section.Description,
                Color = section.Color,
                Order = section.Order,
                Enabled = section.Enabled,

            };
            _context.Sections.Add(sectionEntity);
            _context.SaveChanges();

            return sectionEntity;
        }

        public async Task<Result<Section>> Update(UpdateSectionDto section, int id)
        {
            var currentSection = await _context.Sections.FirstOrDefaultAsync(x => x.Id == id);
            if (currentSection == null)
            {
                return Result.Failure<Section>(SectionErrors.NotSection);
            }

            currentSection.Description = section.Description ?? currentSection.Description;
            currentSection.Color = section.Color ?? currentSection.Color;
            currentSection.Order = section.Order ?? currentSection.Order;
            currentSection.Enabled = section.Enabled ?? currentSection.Enabled;
            _context.SaveChanges();

            return Result.Success(currentSection);
        }

        public bool Remove(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();

            return true;
        }
    }
}

public static class SectionErrors
{
    public static readonly Error NotSection = new("Section Not Found");
}