using Azure.Core;
using CustomerApp.Domain.Entities;
using CustomerApp.WebAPI.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomerApp.WebAPI.Middlewares;

public class GlobalExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An exception occured {@IpAddress}, {@Exception}, {@Request}, {@DateTime}",
            httpContext?.Connection.RemoteIpAddress,
            exception?.Message,
            httpContext?.Request.GetRawLog(),
            DateTime.UtcNow);

        ProblemDetails details = new()
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "Server Error",
            Detail = "An internal server error occured.",
        };

        if (httpContext is not null)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken)
                .ConfigureAwait(false);
        }

        return true;
    }
}
