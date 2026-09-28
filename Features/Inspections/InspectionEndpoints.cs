public static class InspectionEndpoints
{
    public static void MapInspections(this IEndpointRouteBuilder app)
    {
        var detailGroup = app.MapGroup("/api/workguide-details").WithTags("Inspections").RequirePermission(PermissionCodes.Reception);
        detailGroup.MapCreateItemInspection();
        detailGroup.MapGetItemInspection();

        var photoGroup = app.MapGroup("/api/inspections").WithTags("ItemPhotos").RequirePermission(PermissionCodes.Reception);
        photoGroup.MapCreateItemPhoto();
        photoGroup.MapGetItemPhotos();

        var workGuideGroup = app.MapGroup("/api/workguides").WithTags("WorkGuideInspections").RequirePermission(PermissionCodes.Reception);
        workGuideGroup.MapGetInspectionSummary();
        workGuideGroup.MapGetWorkGuidePhotos();
    }
}
