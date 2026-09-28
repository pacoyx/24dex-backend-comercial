public static class WorkerNodeEndpoints
{
    public static void MapWorkerNode(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/workerNode").WithTags("WorkerNode").RequirePermission(PermissionCodes.Settings);
        group.MapGetWorkerNode();
        group.MapGetWorkerNodes();
        group.MapCreateWorkerNode();
        group.MapUpdateWorkerNode();
        group.MapDeleteWorkerNode();
    }
}
