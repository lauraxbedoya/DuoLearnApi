using DuoLearn.Domain;
using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces
{
    public interface IQuestionValidateService
    {
        Result<bool> ValidateQuestionMetadata(CreateQuestionDto question);
    }
}