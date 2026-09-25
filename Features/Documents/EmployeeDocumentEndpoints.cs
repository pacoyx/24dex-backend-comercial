public static class EmployeeDocumentEndpoints
{
    public static RouteGroupBuilder MapEmployeeDocument(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employees").WithTags("HR - Documents");

        group.MapCreateEmployeeDocument();
        group.MapGetEmployeeDocuments();
        group.MapDeleteEmployeeDocument();

        app.MapGetEmployeeDocumentTypes();

        return group;
    }
}
