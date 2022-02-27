using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PokeApi.Core
{
    public class ErrorHandlingMiddleWare : IMiddleware
    {
        public ErrorHandlingMiddleWare()
        {

        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            
            string result;
            var code = HttpStatusCode.InternalServerError;

            var errorMessage = exception.Message + exception.InnerException ?? "\nInner exception: " + exception.InnerException.Message;

            if (exception.Message.Contains("404"))
            {
                code = HttpStatusCode.NotFound;
                errorMessage = "Could not find the pokemon in the pokedex";
            }

            if (exception is ValidationException ||
                exception is System.ComponentModel.DataAnnotations.ValidationException)
            {
                code = HttpStatusCode.BadRequest;
            }

            if (exception is ValidationException)
            {
                errorMessage = errorMessage.Replace("Validation failed: \r\n -- ", "");

                int innerExceptionIndex = errorMessage.IndexOf("\nInner exception", StringComparison.InvariantCulture);
                if (innerExceptionIndex >= 0)
                    errorMessage = errorMessage.Remove(innerExceptionIndex);

                result = JsonConvert.SerializeObject(new { errorMessage });
            }
            else
            {
                result = errorMessage;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
