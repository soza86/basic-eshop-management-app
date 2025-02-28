using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace CSharpApp.Api.Middlewares
{
    public class CustomLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggingMiddleware> _logger;

        public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception occurred.");

                var problemDetails = GetProblemDetails(context, exception);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = problemDetails.Status.Value;
                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }
            finally
            {
                stopwatch.Stop();
                var elapsedMs = stopwatch.ElapsedMilliseconds;

                _logger.LogInformation("Request: {Method} {Path} executed in {ElapsedMilliseconds} ms",
                    context.Request.Method,
                    context.Request.Path,
                    elapsedMs);
            }
        }

        private static ProblemDetails GetProblemDetails(HttpContext context, Exception exception)
        {
            var statusCode = 500;
            var title = "Something went wrong";
            var detail = "Please try again later";

            var statusCodeProperty = exception.GetType().GetProperty("StatusCode", BindingFlags.Public | BindingFlags.Instance);
            if (statusCodeProperty != null)
                statusCode = (int)statusCodeProperty.GetValue(exception)!;

            switch (statusCode)
            {
                case 400:
                    title = "Bad Request";
                    detail = exception.Message;
                    break;
                case 401:
                    title = "Unauthorized";
                    detail = "You must be authenticated to access this resource.";
                    break;
                case 403:
                    title = "Forbidden";
                    detail = "You do not have permission to access this resource.";
                    break;
                case 404:
                    title = "Not Found";
                    detail = "Requested resource was not found.";
                    break;
                default:
                    break;
            }

            return new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };
        }
    }
}
