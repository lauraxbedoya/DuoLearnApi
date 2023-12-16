using System.Text.Json;
using DuoLearn.Application.Interfaces;
using DuoLearn.Domain;
using DuoLearn.Domain.Enums;
using DuoLearn.Domain.Models;
using DuoLearn.Infrastructure.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DuoLearn.Application
{
    public class QuestionValidateService : IQuestionValidateService
    {
        private readonly AppDbContext _context;
        public QuestionValidateService(AppDbContext context)
        {
            _context = context;
        }

        public Result<bool> ValidateQuestionMetadata(CreateQuestionDto question)
        {
            QuestionMetadata? metadata = question.Metadata.Deserialize<QuestionMetadata>();
            
            if (metadata is null) {
                Result.Failure(QuestionErrors.InvalidMetadata);
            }

            switch (question.Type) {
                case QuestionType.TranslateTyping:
                    return ValidateTranslateTyping(metadata!);
                case QuestionType.TranslateWithTiles:
                    return ValidateTranslateWithTiles(metadata!);
                case QuestionType.PairWordsWithTiles:
                    return ValidatePairWordsWithTiles(metadata!);
                case QuestionType.ChooseImageAccordingToWord:
                    return ValidateChooseImageAccordingToWord(metadata!);
                case QuestionType.Conversation:
                    return ValidateConversation(metadata!);
            }

            return Result.Success(true);
        }

        private Result<bool> ValidateTranslateTyping(QuestionMetadata metadata)
        {
            if (metadata.Answers.IsNullOrEmpty())
            {
                return Result.Failure<bool>(QuestionErrors.InvalidMetadata);
            }

            return Result.Success(true);
        }

        private Result<bool> ValidateTranslateWithTiles(QuestionMetadata metadata)
        {
            if (metadata.Answer.IsNullOrEmpty() || metadata.Options.IsNullOrEmpty())
            {
                return Result.Failure<bool>(QuestionErrors.InvalidMetadata);
            }

            return Result.Success(true);
        }

        private Result<bool> ValidatePairWordsWithTiles(QuestionMetadata metadata)
        {
            if (metadata.Pairs.IsNullOrEmpty())
            {
                return Result.Failure<bool>(QuestionErrors.InvalidMetadata);
            }

            return Result.Success(true);
        }

        private Result<bool> ValidateChooseImageAccordingToWord(QuestionMetadata metadata)
        {
            if (metadata.Answer.IsNullOrEmpty() || metadata.Options.IsNullOrEmpty())
            {
                return Result.Failure<bool>(QuestionErrors.InvalidMetadata);
            }

            return Result.Success(true);
        }

        private Result<bool> ValidateConversation(QuestionMetadata metadata)
        {
            if (metadata.Answer.IsNullOrEmpty() || metadata.Options.IsNullOrEmpty())
            {
                return Result.Failure<bool>(QuestionErrors.InvalidMetadata);
            }

            return Result.Success(true);
        }
    }
}