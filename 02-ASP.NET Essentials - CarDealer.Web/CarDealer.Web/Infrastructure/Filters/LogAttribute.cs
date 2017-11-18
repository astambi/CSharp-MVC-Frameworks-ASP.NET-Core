namespace CarDealer.Web.Infrastructure.Filters
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection; // NB!
    using System;
    using System.Globalization;

    public class LogAttribute : ActionFilterAttribute
    {
        private string actionName;

        public LogAttribute(string actionName = null)
        {
            this.actionName = actionName;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.User.Identity.Name;
            var path = context.HttpContext.Request.Path.Value;
            var pathTokens = path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            var controllerName = pathTokens[0];
            this.actionName = this.actionName ?? pathTokens[1];

            var db = context
                .HttpContext
                .RequestServices
                .GetService<CarDealerDbContext>(); // NB!

            // Add Log
            var log = new Log
            {
                User = user,
                Operation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.actionName),
                ModifiedTable = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(controllerName),
                Time = DateTime.UtcNow
            };

            db.Logs.Add(log);
            db.SaveChanges();
        }
    }
}
