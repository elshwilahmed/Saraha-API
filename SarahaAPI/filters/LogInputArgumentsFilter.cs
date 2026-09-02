using Microsoft.AspNetCore.Mvc.Filters;

namespace SarahaAPI.filters
{
    public class LogInputArgumentsFilter : IActionFilter
    {
        private readonly ILogger<LogInputArgumentsFilter> _logger;

        public LogInputArgumentsFilter(ILogger<LogInputArgumentsFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments)
            {
                _logger.LogInformation($"[ActionFilter] Argument Data: {System.Text.Json.JsonSerializer.Serialize(arg.Value)}");
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
