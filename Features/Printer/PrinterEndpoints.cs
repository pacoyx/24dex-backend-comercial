public static class PrinterEndpoints
{
    public static void MapPrinter(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/printer").WithTags("Printer").RequirePermission(PermissionCodes.Settings);
        group.MapGetPrinter();
        group.MapGetPrinters();
        group.MapUpdatePrinter();
        group.MapDeletePrinter();
    }
}
