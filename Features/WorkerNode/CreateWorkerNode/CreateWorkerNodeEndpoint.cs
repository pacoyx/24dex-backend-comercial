using Microsoft.EntityFrameworkCore;

public static class CreateWorkerNodeEndpoint
{
    public static void MapCreateWorkerNode(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateWorkerNodeDto createWorkerNodeDto, RecepcionDbContext context) =>
        {
            if (createWorkerNodeDto.WorkerKey == null)
            {
                return Results.BadRequest("WorkerKey es requerido");
            }

            var branch = await context.BranchSales.FirstOrDefaultAsync(bs => bs.Id == createWorkerNodeDto.BranchId);
            if (branch == null)
            {
                return Results.BadRequest("No se encontró la sucursal");
            }

            var workerNode = new WorkerNode
            {
                BranchId = createWorkerNodeDto.BranchId,
                WorkerKey = createWorkerNodeDto.WorkerKey,
                ComputerName = createWorkerNodeDto.ComputerName,
                IsActive = true
            };

            await context.WorkerNodes.AddAsync(workerNode);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = workerNode.Id.ToString(),
                Message = "Worker node creado",
                StatusCode = StatusCodes.Status201Created,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
