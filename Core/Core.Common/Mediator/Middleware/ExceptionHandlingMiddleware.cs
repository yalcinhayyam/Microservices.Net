using System.Text.Json;
using Core.Common.Mediator.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


file interface IMiddleware
{
    Task InvokeAsync(HttpContext context, RequestDelegate next);
}
public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));    

    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception)
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status406NotAcceptable,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            Core.Common.Mediator.Exceptions.ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null;

        if (exception is ValidationException validationException)
        {
            errors = validationException.ErrorsDictionary;
        }

        return errors;
    }
}