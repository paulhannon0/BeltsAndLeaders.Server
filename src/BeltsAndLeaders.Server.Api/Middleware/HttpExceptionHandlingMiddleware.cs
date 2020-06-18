using System.Threading.Tasks;
using BeltsAndLeaders.Server.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace BeltsAndLeaders.Server.Api.Middleware
{
    internal class HttpExceptionBeltsAndLeadersdlingMiddleware
    {
        private readonly RequestDelegate next;

        public HttpExceptionBeltsAndLeadersdlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (HttpException httpException)
            {
                context.Response.StatusCode = (int)httpException.StatusCode;

                var responseFeature = context.Features.Get<IHttpResponseFeature>();
                responseFeature.ReasonPhrase = $"{ReasonPhrases.GetReasonPhrase(context.Response.StatusCode)}";

                await context.Response.WriteAsync(httpException.Message);
            }
        }
    }
}
