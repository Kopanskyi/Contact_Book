using ContactBook.API.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContactBook.API.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            var request = context.Request;

            if (request.RouteValues.TryGetValue("action", out var value) && 
                value.ToString().ToLower() == Constants.allowedHttpRequest)
            {
                await _next(context);
                return;
            }

            if (!request.Headers.TryGetValue(Constants.userTokenHeader, out var header) || 
                string.IsNullOrEmpty(header))
            {
                throw new UnauthorizedAccessException("Access denied for unauthorized user.");
            }

            authService.SetCurrentUser(header);
            await _next(context);
        }
    }
}
