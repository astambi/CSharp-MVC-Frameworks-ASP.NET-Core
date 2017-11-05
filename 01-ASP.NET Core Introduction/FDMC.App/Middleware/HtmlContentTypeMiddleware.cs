namespace FDMC.App.Middleware
{
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class HtmlContentTypeMiddleware
    {
        private readonly RequestDelegate next;

        public HtmlContentTypeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add(HttpHeader.ContentType, "text/html");

            return this.next(context);
        }
    }
}
