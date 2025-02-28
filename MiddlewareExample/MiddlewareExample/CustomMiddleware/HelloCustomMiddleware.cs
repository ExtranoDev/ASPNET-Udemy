using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddlewareExample.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HelloCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            IQueryCollection queryResult = httpContext.Request.Query;
            if (queryResult.ContainsKey("firstname") && queryResult.ContainsKey("lastname"))
            {
                string fullName = queryResult["firstname"] + " " + queryResult["lastname"] + '\n';
                await httpContext.Response.WriteAsync(fullName);
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HelloCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HelloCustomMiddleware>();
        }
    }
}
