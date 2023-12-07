using DuoLearn.Application;
using DuoLearn.Application.Interfaces;
using DuoLearn.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuoLearn.Api;

[Authorize]
[ApiController]
[Route("levels")]

public class LevelsController : ControllerBase
{
    private readonly ILevelService _levelService;

    public LevelsController(ILevelService levelService)
    {
        _levelService = levelService;
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<Level> GetLevels() => _levelService.GetAllLevels();

    [HttpGet("{levelId}")]
    public ActionResult<IList<Level>> GetLevelSectionById([FromRoute] int levelId)
    {
        IList<Level>? levelSection = _levelService.GetLevelSectionById(levelId);
        if (levelSection is null) return NotFound();

        return Ok(levelSection);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public ActionResult<Level> CreateLevel([FromBody] CreateLevelDto level)
    {
        return Ok(_levelService.Create(level));
    }

    [AllowAnonymous]
    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<Level>> UpdateLevel([FromBody] UpdateLevelDto level, [FromRoute] int id)
    {
        var result = await _levelService.UpdateAsync(level, id);

        if (result.IsFailure)
        {
            if (result.Error.Code == LevelErrors.NotLevelFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> RemoveLevel([FromRoute] int id)
    {
        var result = await _levelService.RemoveAsync(id);

        if (result.IsFailure)
        {
            if (result.Error.Code == LevelErrors.NotLevelFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(true);
    }
}