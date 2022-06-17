using Microsoft.AspNetCore.Mvc.Filters;

namespace Mentoring.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class ToBeLoggedFilter : Attribute, IActionFilter
    {
        ILogger _logger = new LoggerFactory().CreateLogger(typeof(ToBeLoggedFilter));
        private bool _need2log;

        public ToBeLoggedFilter(bool need2Log = false)
        {
            _need2log = need2Log;
        }

        public bool V = false;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_need2log)
            {
                _logger.LogInformation("Action is about to finish");
            }
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            if (_need2log)
            {
                _logger.LogInformation("Action is running");
            }
        }
    }
}