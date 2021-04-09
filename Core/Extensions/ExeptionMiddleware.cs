﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            var errorDetails = new ErrorDetails();

            if (e.GetType() == typeof(ValidationException) || e.GetType() == typeof(ApplicationException))
            {
                errorDetails.Message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorDetails.StatusCode = httpContext.Response.StatusCode;
            }

            else if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                errorDetails.Message = e.Message;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                errorDetails.StatusCode = httpContext.Response.StatusCode;
            }
            else
            {
                errorDetails.Message = message;
                errorDetails.StatusCode = httpContext.Response.StatusCode;
            }


            return httpContext.Response.WriteAsync(errorDetails.ToString());
        }
    }

}

