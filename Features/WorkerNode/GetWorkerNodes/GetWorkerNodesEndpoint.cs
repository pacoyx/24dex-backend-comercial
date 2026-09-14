using Microsoft.EntityFrameworkCore;

public static class GetWorkerNodesEndpoint
{
    public static void MapGetWorkerNodes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var workerNodes = await context.WorkerNodes.AsNoTracking().ToListAsync();

            var response = new ApiResponse<List<WorkerNodeComboResponse>>
            {
                Data = workerNodes.Select(wn => new WorkerNodeComboResponse
                (
                    wn.Id,
                    wn.BranchId,
                    wn.WorkerKey,
                    wn.ComputerName,
                    wn.IsActive,
                    wn.LastConnectionAt
                )).ToList(),
                Message = "Lista de worker nodes",
                StatusCode = 200,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
