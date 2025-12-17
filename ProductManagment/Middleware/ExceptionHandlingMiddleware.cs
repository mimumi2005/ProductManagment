using ProductManagment.Application.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace ProductManagment.WebAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            var problemDetails = CreateProblemDetailsFromException(ex, context);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            if (problemDetails is ValidationProblemDetails validationProblem)
            {
                var responseBody = new
                {
                    title = validationProblem.Title,
                    status = validationProblem.Status,
                    detail = validationProblem.Detail,
                    instance = validationProblem.Instance,
                    errors = validationProblem.Errors
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody, options));
            }
            else
            {
                await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, options));
            }
        }
    }
    private static ProblemDetails CreateProblemDetailsFromException(Exception ex, HttpContext context)
    {
        return ex switch
        {
            ValidationException ve => new ValidationProblemDetails(
            ve.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                )
        )
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest,
                Detail = "One or more validation errors occurred.",
                Instance = context.Request.Path
            },

            KeyNotFoundException nf => new ProblemDetails
            {
                Title = "Resource Not Found",
                Detail = nf.Message,
                Status = StatusCodes.Status404NotFound,
                Instance = context.Request.Path
            },

            UnauthorizedAccessException ua => new ProblemDetails
            {
                Title = "Unauthorized",
                Detail = ua.Message,
                Status = StatusCodes.Status401Unauthorized,
                Instance = context.Request.Path
            },
            _ => new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path
            }
        };
    }

}
