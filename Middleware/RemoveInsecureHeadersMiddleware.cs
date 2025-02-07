using Microsoft.Extensions.Primitives;

public class RemoveInsecureHeadersMiddleware
{
    private readonly RequestDelegate _next;
    public RemoveInsecureHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Response.OnStarting((state) =>
        {
            httpContext.Response.Headers.Remove("Server");
            httpContext.Response.Headers.Remove("X-Powered-By");
            httpContext.Response.Headers.Remove("X-Aspnet-version");
            httpContext.Response.Headers.Remove("X-AspnetMvc-version");
            httpContext.Response.Headers.Append("X-Content-Type-Options", new StringValues("nosniff"));
            httpContext.Response.Headers.Append("X-Frame-Options", new StringValues("DENY"));
            return Task.CompletedTask;
        }, null!);
        await _next(httpContext);
    }
}