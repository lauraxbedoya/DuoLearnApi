using DuoLearn.Application;
using DuoLearn.Application.Interfaces;
    using DuoLearn.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuoLearn.Api;

[Authorize]
[ApiController]
[Route("sections")]
public class SectionsController : ControllerBase
{
    private readonly ISectionService _sectionService;

    public SectionsController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<Section> GetSections() => _sectionService.GetAllSections();

    [HttpGet("{languageId}")]
    public ActionResult<IList<Section>> GetSectionLanguageById([FromRoute] int languageId)
    {
        IList<Section>? sectionLanguage = _sectionService.GetSectionLanguageById(languageId);
        if (sectionLanguage is null) return NotFound();

        return Ok(sectionLanguage);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public ActionResult<Section> CreateSection([FromBody] CreateSectionDto section)
    {
        return Ok(_sectionService.Create(section));
    }

    [AllowAnonymous]
    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<Section>> UpdateSection([FromBody] UpdateSectionDto section, [FromRoute] int id)
    {
        var result = await _sectionService.UpdateAsync(section, id);

        if (result.IsFailure)
        {
            if (result.Error.Code == SectionErrors.NotSectionFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> RemoveSection([FromRoute] int id)
    {
        var result = await _sectionService.RemoveAsync(id);

        if (result.IsFailure)
        {
            if (result.Error.Code == SectionErrors.NotSectionFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(true);
    }
}
