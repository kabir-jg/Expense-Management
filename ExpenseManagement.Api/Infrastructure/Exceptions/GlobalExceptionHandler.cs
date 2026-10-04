using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace ExpenseManagement.Api.Infrastructure.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var response = new
            {
                Message = "Validation failed",
                Errors = validationException.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(group => 
                        group.Key, group =>
                        group.Select(error => error.ErrorMessage).ToList())
            };
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            
            return true;
        }

        return false;
    }
}