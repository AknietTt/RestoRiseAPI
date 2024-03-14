using RestoRise.Domain.Common;

namespace RestoRise.Api.Middleware;

public class CustomAuthenticationFailureMiddleware
{
    
    private readonly RequestDelegate _next;

    public CustomAuthenticationFailureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
        
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            await context.Response.WriteAsJsonAsync(Result<string>.Failure("Пользователь не авторизован", 401));
        }
    }
}