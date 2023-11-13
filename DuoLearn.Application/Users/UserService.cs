using DuoLearn.Domain.Models;
using System.Linq;
using DuoLearn.Infrastructure.Context;
using DuoLearn.Domain.Enums;
using DuoLearn.Application.Interfaces;
using DuoLearn.Application;

namespace DuoLearn.Applications
{
    public class UserServices : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IJwtProvider _jwtProvider;

        public UserServices(AppDbContext context, IJwtProvider jwtProvider)
        {
            _context = context;
            _jwtProvider = jwtProvider;
        }


        public IEnumerable<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public string Create(UserDto user)
        {
            string password = user.Password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var userEntity = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = passwordHash,
                ProfileImage = user.ProfileImage,
                DateOfBirth = user.DateOfBirth!.Value,
                Role = RoleEnum.User.ToString(),
                Active = true,
            };
            _context.Users.Add(userEntity);
            _context.SaveChanges();

            return _jwtProvider.Generate(userEntity);
        }
    }
}