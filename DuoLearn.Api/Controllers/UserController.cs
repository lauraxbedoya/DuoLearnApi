using System.Security.Claims;
using DuoLearn.Application;
using DuoLearn.Application.Interfaces;
using DuoLearn.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuoLearn.Api;

// [Authorize]
[ApiController]
[Route("users")]
public class UsersCrontroller : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthServices _authService;

    public UsersCrontroller(IUserService userService, IAuthServices authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<User> getUsers() => _userService.GetAllUsers();

    [SetAuthUser]
    [HttpGet]
    [Route("me")]
    public User? getMe() => HttpContext.Items["User"] as User;

    [HttpGet("{id}")]
    public ActionResult<User> getUserById([FromRoute] int id)
    {
        User? user = _userService.GetUserById(id);
        if (user is null) return NotFound();

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<object>> LoginUser(
        [FromBody] LoginDataDto LoginRequest)
    {
        string token = await _authService.AuthenticateAsync(LoginRequest.Email, LoginRequest.Password);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(token);
        }
        return new { token };
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public ActionResult<object> createUser([FromBody] UserDto user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var token = _userService.Create(user);
        return new { token };
    }
}
