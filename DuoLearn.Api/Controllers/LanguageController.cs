using System.Security.Claims;
using DuoLearn.Application;
using DuoLearn.Application.Interfaces;
using DuoLearn.Applications;
using DuoLearn.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuoLearn.Api;

[Authorize]
[ApiController]
[Route("languages")]
public class LanguagesController : ControllerBase
{
    private readonly ILanguageService _languageService;

    public LanguagesController(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<Language> GetLanguages() => _languageService.GetAllLanguages();

    [HttpGet("{id}")]
    public ActionResult<Language> GetLanguageById([FromRoute] int id)
    {
        Language? language = _languageService.GetLanguageById(id);
        if (language is null) return NotFound();

        return Ok(language);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public ActionResult<Language> CreateLanguage([FromBody] Language language)
    {
        return Ok(_languageService.Create(language));
    }

    [AllowAnonymous]
    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<Language>> UpdateLanguage([FromBody] Language language, [FromRoute] int id)
    {
        var updatedLanguage = await _languageService.UpdateAsync(language, id);

        if (updatedLanguage is null)
        {
            return NotFound();
        }

        return Ok(updatedLanguage);
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> RemoveLanguage([FromRoute] int id)
    {
        var result = await _languageService.RemoveAsync(id);

        if (result.IsFailure)
        {
            if (result.Error.Code == LanguageErrors.NotLanguageFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(true);
    }
}
