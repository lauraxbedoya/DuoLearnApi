using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces;

public interface IJwtProvider
{
    string Generate(User user);
}
