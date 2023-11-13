using DuoLearn.Application.Interfaces;
using DuoLearn.Domain;
using DuoLearn.Domain.Models;
using DuoLearn.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DuoLearn.Application;

public class AuthService : IAuthServices
{
    private readonly AppDbContext _context;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(AppDbContext context, IJwtProvider jwtProvider)
    {
        _context = context;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> AuthenticateAsync(string Email, string Password)
    {
        User? user = await _context.Users.SingleOrDefaultAsync(user => user.Email == Email);
        if (user is null)
        {
            throw new InvalidCredentialException("Invalid Credentials");
        }

        bool verified = BCrypt.Net.BCrypt.Verify(Password, user.Password);
        if (!verified)
        {
            throw new InvalidCredentialException("Invalid Credentials");
        }

        string token = _jwtProvider.Generate(user);
        return token;
    }
}
