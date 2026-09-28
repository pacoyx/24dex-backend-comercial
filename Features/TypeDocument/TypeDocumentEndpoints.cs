public static class TypeDocumentEndpoints
{
    public static void MapTypeDocument(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/type-documents").WithTags("TypeDocuments").RequirePermission(PermissionCodes.Settings);
        group.MapGetTypeDocuments();
    }
}
