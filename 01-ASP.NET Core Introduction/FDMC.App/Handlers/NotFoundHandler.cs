namespace FDMC.App.Handlers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Http;

    public static class NotFoundHandler
    {
        public static RequestDelegate RequestHandler
            => async (context) =>
            {
                context.Response.StatusCode = HttpStatusCode.NotFound;
                await context.Response.WriteAsync("404 Page was not found :(");
            };
    }
}
