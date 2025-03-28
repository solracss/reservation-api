using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status404NotFound, notFoundException.Message);
            }
            catch (BadRequestException badRequestException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, badRequestException.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync( new { error = message });
        }
    }
}
