public class EmojiMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;
    public EmojiMiddleware(ILogger<EmojiMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}
public static class EmojiMiddlewareExtensions
{
    public static IApplicationBuilder UseEmojiMiddleware(
    this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<EmojiMiddleware>();
    }
}