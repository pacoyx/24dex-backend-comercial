public static class RemoveInsecureHeadersMiddlewareExtensions
{
    public static IApplicationBuilder RemoveInsecureHeaders(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RemoveInsecureHeadersMiddleware>();
    }
}