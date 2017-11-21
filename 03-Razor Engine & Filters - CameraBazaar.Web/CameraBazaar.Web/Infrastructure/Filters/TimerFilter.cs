namespace CameraBazaar.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Diagnostics;
    using System.IO;

    public class TimerFilter : ActionFilterAttribute
    {
        private const string TimeLogsFileName = "action-times.txt";

        private Stopwatch stopwatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.stopwatch = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            this.stopwatch.Stop();
            var dateTime = DateTime.UtcNow;

            using (var writer = new StreamWriter(TimeLogsFileName, true))
            {
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"].ToString();
                var elapsedTime = this.stopwatch.Elapsed;

                var logMessage = $"{dateTime} - {controller}.{action} - {elapsedTime}";

                writer.WriteLine(logMessage);
            }
        }
    }
}
