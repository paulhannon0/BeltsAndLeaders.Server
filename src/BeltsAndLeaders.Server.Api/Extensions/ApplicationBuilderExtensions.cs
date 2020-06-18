using BeltsAndLeaders.Server.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace BeltsAndLeaders.Server.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHttpExceptionHandling(this IApplicationBuilder application)
        {
            return application.UseMiddleware<HttpExceptionHandlingMiddleware>();
        }
    }
}
