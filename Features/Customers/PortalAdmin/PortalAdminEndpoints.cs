public static class PortalAdminEndpoints
{
    public static void MapPortalAdmin(this IEndpointRouteBuilder app)
    {
        app.MapEnablePortal();
        app.MapGetPortalStatus();
        app.MapRegenerateActivationLink();
    }
}
