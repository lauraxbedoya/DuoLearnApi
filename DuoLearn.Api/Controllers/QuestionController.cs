using DuoLearn.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DuoLearn.Domain.Models;
using DuoLearn.Application.Interfaces;
using System.Text.Json;

namespace DuoLearn.Api;

[Authorize]
[ApiController]
[Route("questions")]

public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;
    private readonly IQuestionValidateService _questionValidateService;

    public QuestionsController(IQuestionService questionService, IQuestionValidateService questionValidateService)
    {
        _questionService = questionService;
        _questionValidateService = questionValidateService;
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<Question> GetQuestions() => _questionService.GetAllQuestions();

    [HttpGet("{questionId}")]
    public ActionResult<IList<Question>> GetQuestionLessonById([FromRoute] int questionId)
    {
        IList<Question>? QuestionLesson = _questionService.GetQuestionLessonById(questionId);
        if (QuestionLesson is null) return NotFound();

        return Ok(QuestionLesson);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public ActionResult<Question> CreateQuestion([FromBody] CreateQuestionDto question)
    {
        var validationResult = _questionValidateService.ValidateQuestionMetadata(question);
        if (validationResult.IsFailure) {
            return NotFound(validationResult?.Error.Code);
        }

        return Ok(_questionService.Create(question));
    }

    [AllowAnonymous]
    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<Question>> UpdateQuestion([FromBody] UpdateQuestionDto question, [FromRoute] int id)
    {
        var result = await _questionService.UpdateAsync(question, id);

        if (result.IsFailure)
        {
            if (result.Error.Code == QuestionErrors.NotQuestionFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> RemoveQuestion([FromRoute] int id)
    {
        var result = await _questionService.RemoveAsync(id);

        if (result.IsFailure)
        {
            if (result.Error.Code == QuestionErrors.NotQuestionFound.Code)
            {
                return NotFound(result.Error.Description);
            }
        }

        return Ok(true);
    }
}