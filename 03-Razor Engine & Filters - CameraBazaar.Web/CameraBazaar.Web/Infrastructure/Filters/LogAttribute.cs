namespace CameraBazaar.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class LogAttribute : ActionFilterAttribute
    {
        private const string LogsFileName = "logs.txt";

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Task.Run(async () =>
            //{
                using (var writer = new StreamWriter(LogsFileName, true))
                {
                    var dateTime = DateTime.UtcNow;
                    var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                    var user = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                    var controller = context.Controller.GetType().Name;
                    var action = context.RouteData.Values["action"].ToString();

                    var logMessage = $"{dateTime} - {ipAddress} - {user} - {controller}.{action}";

                    if (context.Exception != null)
                    {
                        var exceptionType = context.Exception.GetType().Name;
                        var exceptionMessage = context.Exception.Message;

                        logMessage = $"[!] {logMessage} - {exceptionType} - {exceptionMessage}";
                    }

                    /*await*/ writer.WriteLineAsync(logMessage);
                }
            //})
            //.GetAwaiter()
            //.GetResult();
        }
    }
}
