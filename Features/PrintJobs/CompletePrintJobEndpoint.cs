using Microsoft.EntityFrameworkCore;

public static class CompletePrintJobEndpoint
{
    public static void MapCompletePrintJob(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/printjobs/{id:long}/completed",
            async (
                long id,
                RecepcionDbContext db) =>
            {
                var printJob =
                    await db.PrintQueues
                        .FirstOrDefaultAsync(x =>
                            x.Id == id);

                if (printJob is null)
                {
                    return Results.NotFound();
                }

                printJob.Status =
                    PrintStatus.Printed;

                printJob.ProcessedAt =
                    DateTime.UtcNow;

                await db.SaveChangesAsync();

                return Results.Ok(new
                {
                    printJob.Id,
                    printJob.Status,
                    printJob.ProcessedAt
                });
            });
    }
}
