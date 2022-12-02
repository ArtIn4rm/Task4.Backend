using System.Net;
using System.Text.Json;
using FluentValidation;
using Task4.Application.Common.Exceptions;

namespace Task4.WebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next) => 
            _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch(exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest; 
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case HasTakenException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case WrongPasswordException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case UserIsBlockedException: 
                    code = HttpStatusCode.BadRequest;
                    break;
                case InvalidTokenException:
                    code = HttpStatusCode.Unauthorized;
                    break;
            }
            Console.WriteLine(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if(result == string.Empty)
            {
                result = JsonSerializer.Serialize(new {error = exception.Message});
            }
            return context.Response.WriteAsync(result);
        }
    }
}
