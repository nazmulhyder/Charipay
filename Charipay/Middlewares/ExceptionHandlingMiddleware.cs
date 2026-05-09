using Charipay.Application.Common.Exceptions;
using Charipay.Application.Common.Models;
using FluentValidation;

namespace Charipay.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next , ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // default to 500
            var statusCode = StatusCodes.Status500InternalServerError;
            string message = "An unexpected error occurred.";
            var errors = new List<string>();

            // Example: map specific exception types to proper status codes
            // (replace with your own custom exceptions if you have them)
            if (ex is FluentValidation.ValidationException validationEx)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = "Validation failed.";
                errors = validationEx.Errors
                                     .Select(e => e.ErrorMessage)
                                     .Distinct()
                                     .ToList();

                _logger.LogWarning(ex, "Validation exception occured. Path: {Path}, Method: {Method}, TraceId: {TraceId}"
                    , context.Request.Path, context.Request.Method, context.TraceIdentifier
                    );
                
            }
            else if (ex is NotFoundException notFoundEx)
            {
                statusCode = StatusCodes.Status404NotFound;
                message = notFoundEx.Message;

                _logger.LogWarning(ex, "Resource not found. Path: {Path}, Method: {Method}, TraceId: {TraceId}"
                  , context.Request.Path, context.Request.Method, context.TraceIdentifier
                  );
            }
            else
            {

                _logger.LogWarning(ex, "Unhandled exception occured. Path: {Path}, Method: {Method}, TraceId: {TraceId}"
                  , context.Request.Path, context.Request.Method, context.TraceIdentifier
                  );

                // log unexpected errors
                // _logger.LogError(ex, "Unhandled exception");
                errors.Add(ex.Message); // in production you might hide this
            }

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var apiResponse = ApiResponse<object>.FailedResponse(message, errors);

            return context.Response.WriteAsJsonAsync(apiResponse);
        }
    }

}
