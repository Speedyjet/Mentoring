using Microsoft.AspNetCore.Mvc.Filters;

namespace Mentoring.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class ToBeLoggedFilter : Attribute, IActionFilter
    {
        ILogger _logger = new LoggerFactory().CreateLogger(typeof(ToBeLoggedFilter));

        public ToBeLoggedFilter(bool need2Log = false)
        {
        }

        public bool V = false;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Action is about to finish");
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Action is running");
        }
    }
}