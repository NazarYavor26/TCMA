﻿using System.Net;
using System.Text.Json;
using TCMA.BLL.Models;

namespace TCMA.API.Infrastructure.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ErrorDetails errorDetails = new ErrorDetails();

            switch (exception)
            {
                case KeyNotFoundException ex:
                    errorDetails.StatusCode = (int)HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorDetails.Message = exception.Message;
                    break;

                case InvalidOperationException ex:
                    errorDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorDetails.Message = exception.Message;
                    break;

                default:
                    errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorDetails.Message = "Internal server error. Please retry after some time.";
                    break;
            }

            var exceptionResult = JsonSerializer.Serialize(errorDetails);
            await context.Response.WriteAsync(exceptionResult);
        }
    }
}
