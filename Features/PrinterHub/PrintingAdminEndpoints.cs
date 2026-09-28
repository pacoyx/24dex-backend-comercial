namespace _24dex_backend_comercial.Features.PrinterHub;

public static class PrintingAdminEndpoints
{
    public static void MapPrintingAdmin(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/admin/printing").WithTags("PrintingAdmin").RequirePermission(PermissionCodes.Settings);
        group.MapGetPrintingStatus();
    }
}
