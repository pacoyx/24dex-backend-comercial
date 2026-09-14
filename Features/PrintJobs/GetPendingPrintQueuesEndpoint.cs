using Microsoft.EntityFrameworkCore;

public static class GetPendingPrintQueuesEndpoint
{
    public static void MapGetPendingPrintQueues(this IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/admin/print-queues/pending",
            async (RecepcionDbContext db) =>
            {
                var result = await db.PrintQueues
                    .Where(x =>
                        x.Status == PrintStatus.Pending ||
                        x.Status == PrintStatus.Error)
                    .OrderBy(x => x.CreatedAt)
                    .Select(x => new PendingPrintQueueResponse(
                        x.Id,
                        x.WorkGuideId,
                        x.PrinterId,
                        x.DocumentType,
                        x.CreatedAt,
                        x.RetryCount,
                        x.Status
                    ))
                    .ToListAsync();

                return Results.Ok(result);
            });
    }
}
