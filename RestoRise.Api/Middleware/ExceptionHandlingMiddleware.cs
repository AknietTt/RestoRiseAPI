using RestoRise.Domain.Common;

namespace RestoRise.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    
    
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExeptionAsync(context, e);
        }
    }

    private async Task HandleExeptionAsync(HttpContext context, Exception exception)
    {
        var response = exception switch
        {
            UnauthorizedAccessException _ => Result<string>.Failure(exception.Message, 401),
            _ => Result<string>.Failure(exception.Message, 500),
        };

        if (exception is UnauthorizedAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
    }
}