using System.Security.Claims;
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
    private readonly ILanguageService _languageService;

    public SectionsController(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    [Authorize]
    [HttpGet]
    [Route("")]
    public IEnumerable<Language> getLanguages() => _languageService.GetAllLanguages();

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public ActionResult<Language> createLanguage([FromBody] Language language) => _languageService.Create(language);
}
