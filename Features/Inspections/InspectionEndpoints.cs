public static class InspectionEndpoints
{
    public static void MapInspections(this IEndpointRouteBuilder app)
    {
        var detailGroup = app.MapGroup("/api/workguide-details").WithTags("Inspections");
        detailGroup.MapCreateItemInspection();
        detailGroup.MapGetItemInspection();

        var photoGroup = app.MapGroup("/api/inspections").WithTags("ItemPhotos");
        photoGroup.MapCreateItemPhoto();
        photoGroup.MapGetItemPhotos();

        var workGuideGroup = app.MapGroup("/api/workguides").WithTags("WorkGuideInspections");
        workGuideGroup.MapGetInspectionSummary();
        workGuideGroup.MapGetWorkGuidePhotos();
    }
}
