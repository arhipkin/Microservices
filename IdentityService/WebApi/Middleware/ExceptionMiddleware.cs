using Newtonsoft.Json;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        RequestDelegate Next { get; }
        ILogger Logger { get; }
        IHostEnvironment Environment { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var body = context.Response.Body;
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                // If context.Response.HasStarted == true, then we can't write to the response stream anymore. So we have to restore the body.
                // If we don't do that we get an exception.
                context.Response.Body = body;
                await HandleExceptionAsync(context, ex);
            }
        }

        async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = 500;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            // We can decide what the status code should return
            if (ex is KeyNotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }

            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(
                    new ErrorResponse(ex, Environment.IsDevelopment())));

            if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                Logger.LogError(ex, "Unhandled exception occurred");
            }
            else
            {
                Logger.LogDebug(ex, "Unhandled exception occurred");
            }
        }

        class ErrorResponse
        {
            public ErrorResponse(Exception ex, bool includeFullExceptionInfo)
            {
                Error = new ExceptionDescription(ex);
                if (includeFullExceptionInfo)
                {
                    Error.Exception = ex;
                }
            }

            public ExceptionDescription Error { get; set; }
        }

        class ExceptionDescription
        {
            public ExceptionDescription(Exception ex)
            {
                Message = ex.Message;
            }

            public string Message { get; set; }
            public Exception Exception { get; set; }
        }
    }
}
