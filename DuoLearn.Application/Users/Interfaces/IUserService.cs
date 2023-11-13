using DuoLearn.Domain.Models;

namespace DuoLearn.Application.Interfaces;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    string Create(UserDto user);
}