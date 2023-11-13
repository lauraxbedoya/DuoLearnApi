namespace DuoLearn.Application;

public interface IAuthServices
{
    Task<string> AuthenticateAsync(string Email, string Password);

}
