using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static Author_API.Middlewares.Exceptions;

namespace Author_API.Middlewares
{

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BadRequestException ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = 400;

                var errorResponse = new
                {
                    message = ex.Message,
                    statusCode = response.StatusCode
                };

                _logger.LogError(ex, ex.Message);
                var errorJson = JsonSerializer.Serialize(errorResponse);

                await response.WriteAsync(errorJson);
            }
            catch (NotFoundException ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = 400;

                var errorResponse = new
                {
                    message = ex.Message,
                    statusCode = response.StatusCode
                };

                _logger.LogError(ex, ex.Message);
                var errorJson = JsonSerializer.Serialize(errorResponse);

                await response.WriteAsync(errorJson);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new
                {
                    message = ex.Message,
                    statusCode = response.StatusCode
                };

                _logger.LogError(ex, ex.Message);
                var errorJson = JsonSerializer.Serialize(errorResponse);

                await response.WriteAsync(errorJson);
            }
        }
    }
}
