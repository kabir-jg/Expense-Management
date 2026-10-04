using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Application.Common.Models;
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
        // Validation Exception Handling
        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errors = validationException.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .Select(error => error.ErrorMessage)
                        .ToList()
                );

            var validationResponse = ApiResponse<object>.FailureResponse(
                "Validation failed",
                errors
            );

            await httpContext.Response.WriteAsJsonAsync(
                validationResponse,
                cancellationToken
            );

            return true;
        }

        // Unauthorized Exception Handling
        if (exception is UnauthorizedAccessException unauthorizedException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            var unauthorizedResponse = ApiResponse<object>.FailureResponse(
                unauthorizedException.Message
            );

            await httpContext.Response.WriteAsJsonAsync(
                unauthorizedResponse,
                cancellationToken
            );

            return true;
        }
        
        // Conflict Exception Handling
        if (exception is ConflictException conflictException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            var conflictResponse = ApiResponse<object>.FailureResponse(
                conflictException.Message
            );

            await httpContext.Response.WriteAsJsonAsync(
                conflictResponse,
                cancellationToken
            );

            return true;
        }
        
        // Not Found Exception Handling
        if (exception is NotFoundException notFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            var notFoundResponse = ApiResponse<object>.FailureResponse(
                notFoundException.Message
            );

            await httpContext.Response.WriteAsJsonAsync(
                notFoundResponse,
                cancellationToken
            );

            return true;
        }

        // Anything that reaches here is an unexpected exception
        httpContext.Response.StatusCode =
            StatusCodes.Status500InternalServerError;

        var serverErrorResponse = ApiResponse<object>.FailureResponse(
            "An unexpected error occurred. Please try again later."
        );

        await httpContext.Response.WriteAsJsonAsync(
            serverErrorResponse,
            cancellationToken
        );

        return true;
    }
}