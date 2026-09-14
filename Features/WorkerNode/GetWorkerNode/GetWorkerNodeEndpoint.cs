public static class GetWorkerNodeEndpoint
{
    public static void MapGetWorkerNode(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, RecepcionDbContext context) =>
        {
            var workerNode = await context.WorkerNodes.FindAsync(id);
            if (workerNode == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(workerNode);
        });
    }
}
