﻿using System.Net;
using System.Text.Json;

namespace VehicleStateCodes
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware
            (
                RequestDelegate next,
                ILogger<ExceptionHandlingMiddleware> logger
            )
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
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                Message = exception.Message,
                ErrorCode = "BAD_REQUEST",
                InnerException = exception.InnerException,
                statusCode = HttpStatusCode.InternalServerError,
                Succes = false
            });
            return context.Response.WriteAsync(result);
        }
    }
}
