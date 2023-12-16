using System.Text.Json;
using DuoLearn.Application.Interfaces;
using DuoLearn.Domain;
using DuoLearn.Domain.Enums;
using DuoLearn.Domain.Models;
using DuoLearn.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DuoLearn.Application
{
    public class QuestionServices : IQuestionService
    {
        private readonly AppDbContext _context;
        public QuestionServices(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            var questions = _context.Questions.ToList();
            return questions;
        }

        public IList<Question> GetQuestionLessonById(int lessonId)
        {
            return _context.Questions.Where(x => x.LessonId == lessonId).ToList();
        }

        public Question Create(CreateQuestionDto question)
        {
            var questionEntity = new Question()
            {
                LessonId = question.LessonId,
                Text = question.Text,
                Type = question.Type.ToString(),
                Feedback = question.Feedback,
                Order = question.Order,
                Metadata = question.Metadata,
            };
            _context.Questions.Add(questionEntity);
            _context.SaveChanges();

            return questionEntity;
        }

        public async Task<Result<Question>> UpdateAsync(UpdateQuestionDto question, int id)
        {
            var currentQuestion = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (currentQuestion == null)
            {
                return Result.Failure<Question>(QuestionErrors.NotQuestionFound);
            }

            currentQuestion.Text = question.Text ?? currentQuestion.Text;
            currentQuestion.Type = question.Type.ToString() ?? currentQuestion.Type.ToString();
            currentQuestion.Feedback = question.Feedback ?? currentQuestion.Feedback;
            currentQuestion.Order = question.Order ?? currentQuestion.Order;
            _context.SaveChanges();

            return Result.Success(currentQuestion);
        }

        public async Task<Result> RemoveAsync(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync((ques) => ques.Id == id);
            if (question is null)
            {
                return Result.Failure(QuestionErrors.NotQuestionFound);
            }
            _context.Remove(question);
            await _context.SaveChangesAsync();

            return Result.Success();
        }   
    }
}

public static class QuestionErrors
{
    public static readonly Error NotQuestionFound = new("Question Not Found");
    public static readonly Error InvalidMetadata = new("Invalid Metadata");
}