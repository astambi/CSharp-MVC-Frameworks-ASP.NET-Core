namespace FDMC.App.Handlers
{
    using Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class HomeHandler : IHandler
    {
        public int Order => 1;

        public Func<HttpContext, bool> Condition
            => ctx => ctx.Request.Path.Value == "/" &&
                      ctx.Request.Method == HttpMethod.Get;

        public RequestDelegate RequestHandler
            => async (context) =>
            {
                await context.Response.WriteAsync($"<h1>Fluffy Duffy Munchkin Cats</h1>");
                await context.Response.WriteAsync("<ul>");

                using (var db = context.RequestServices.GetRequiredService<CatsDbContext>())
                {
                    var catsData = db
                        .Cats
                        .Select(c => new
                        {
                            c.Id,
                            c.Name
                        })
                        .ToList();

                    foreach (var cat in catsData)
                    {
                        await context.Response.WriteAsync(
                            $@"<li><a href=""/cat/{cat.Id}"">{cat.Name}</a></li>");
                    }
                }

                await context.Response.WriteAsync("</ul>");
                await context.Response.WriteAsync(
                    @"<form action=""/cat/add"">
                        <input type=""submit"" value=""Add Cat""/>
                    </form>");
            };
    }
}
