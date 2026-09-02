using System.Diagnostics;

namespace SarahaAPI.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            await _next(context);
            watch.Stop();

            _logger.LogInformation($"[Performance] the request {context.Request.Method} at {context.Request.Path} took {watch.ElapsedMilliseconds} ms");
        }
    }
}
