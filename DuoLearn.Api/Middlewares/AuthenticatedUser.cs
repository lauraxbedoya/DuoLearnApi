using System.Security.Claims;
using DuoLearn.Application.Interfaces;

namespace DuoLearn.Api;

public class AuthenticatedUser : IMiddleware
{
  private readonly IUserService _userService;

  public AuthenticatedUser(IUserService userService)
  {
    _userService = userService;
  }

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    if (ShouldSetUser(context))
    {
      context = await SetAuthUser(context);
    }

    await next(context);
  }

  private bool ShouldSetUser(HttpContext context)
  {
    var endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
      var shouldSetUser = endpoint.Metadata.GetMetadata<SetAuthUser>();
      return shouldSetUser is not null;
    }
    return false;
  }

  private async Task<HttpContext> SetAuthUser(HttpContext context)
  {
    var user = context.User;
    if (user.Identity?.IsAuthenticated == true)
    {
      var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
      if (userId != null)
      {
        context.Items["User"] = await _userService.GetUserByIdAsync(int.Parse(userId));
      }
    }
    return context;
  }
}
