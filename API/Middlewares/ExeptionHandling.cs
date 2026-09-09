namespace API.Middlewares;

public class ExeptionHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExeptionHandling> _logger;
    private readonly IHostEnvironment _env;

    public ExeptionHandling(RequestDelegate next, ILogger<ExeptionHandling> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new ProblemDetails
            {
                Status = 500,
                Title = "Internal server error",
                Detail = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null;
                Title = ex.Message;
            };
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
