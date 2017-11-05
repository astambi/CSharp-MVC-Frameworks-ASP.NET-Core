namespace FDMC.App.Handlers
{
    using Microsoft.AspNetCore.Http;
    using System;

    public interface IHandler
    {
        int Order { get; }

        Func<HttpContext, bool> Condition { get; }

        RequestDelegate RequestHandler { get; }
    }
}
