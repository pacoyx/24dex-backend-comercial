using Microsoft.EntityFrameworkCore;

public static class DeleteWorkerNodeEndpoint
{
    public static void MapDeleteWorkerNode(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
        {
            var workerNode = await context.WorkerNodes
            .FirstOrDefaultAsync(wn => wn.Id == id);

            if (workerNode == null)
            {
                return Results.NotFound();
            }

            context.WorkerNodes.Remove(workerNode);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Worker node eliminado",
                Message = "Worker node eliminado",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
