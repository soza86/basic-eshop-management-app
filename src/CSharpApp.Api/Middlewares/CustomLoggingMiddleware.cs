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

                var statusCode = 500;
                var statusCodeProperty = exception.GetType().GetProperty("StatusCode", BindingFlags.Public | BindingFlags.Instance);
                if (statusCodeProperty != null)
                    statusCode = (int)statusCodeProperty.GetValue(exception)!;

                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = "An error occurred",
                    Detail = "Please try again later",
                    Instance = context.Request.Path
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
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
    }
}
