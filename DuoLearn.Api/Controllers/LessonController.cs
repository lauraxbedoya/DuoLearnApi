using DuoLearn.Application;
using DuoLearn.Application.Interfaces;
using DuoLearn.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuoLearn.Api;

[Authorize]
[ApiController]
[Route("lessons")]

public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<Lesson> GetLessons() => _lessonService.GetAllLessons();

    [HttpGet("{lessonId}")]
    public ActionResult<IList<Lesson>> GetLessonLevelById([FromRoute] int lessonId)
    {
        IList<Lesson>? LessonLevel = _lessonService.GetLessonLevelById(lessonId);
        if (LessonLevel is null) return NotFound();

        return Ok(LessonLevel);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public ActionResult<Lesson> CreateLesson([FromBody] CreateLessonDto lesson)
    {
        return Ok(_lessonService.Create(lesson));
    }

    [AllowAnonymous]
    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<Lesson>> UpdateLesson([FromBody] UpdateLessonDto lesson, [FromRoute] int id)
    {
        var result = await _lessonService.UpdateAsync(lesson, id);

        if (result.IsFailure)
        {
            if (result.Error.Code == LessonErrors.NotLessonFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> RemoveLesson([FromRoute] int id)
    {
        var result = await _lessonService.RemoveAsync(id);

        if (result.IsFailure)
        {
            if (result.Error.Code == LessonErrors.NotLessonFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(true);
    }
}