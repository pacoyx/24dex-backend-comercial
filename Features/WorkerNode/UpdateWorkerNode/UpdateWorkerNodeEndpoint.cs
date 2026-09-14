using Microsoft.EntityFrameworkCore;

public static class UpdateWorkerNodeEndpoint
{
    public static void MapUpdateWorkerNode(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (RecepcionDbContext context, int id, UpdateWorkerNodeDto request) =>
        {
            var workerNode = await context.WorkerNodes
            .FirstOrDefaultAsync(wn => wn.Id == id);

            if (workerNode == null)
            {
                return Results.NotFound();
            }

            var branch = await context.BranchSales.FirstOrDefaultAsync(bs => bs.Id == request.BranchId);
            if (branch == null)
            {
                return Results.BadRequest("No se encontró la sucursal");
            }

            workerNode.BranchId = request.BranchId;
            workerNode.WorkerKey = request.WorkerKey;
            workerNode.ComputerName = request.ComputerName;
            workerNode.IsActive = request.IsActive;

            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Worker node actualizado",
                Message = "Worker node actualizado",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
