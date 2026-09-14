using Microsoft.EntityFrameworkCore;

public static class CancelPrintQueueEndpoint
{
    public static void MapCancelPrintQueue(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/admin/print-queues/{id:long}/cancel",
            async (
                long id,
                RecepcionDbContext db) =>
            {
                var queue =
                    await db.PrintQueues
                        .FirstOrDefaultAsync(x =>
                            x.Id == id);

                if (queue is null)
                {
                    return Results.NotFound();
                }

                queue.Status =
                    PrintStatus.Cancelled;

                await db.SaveChangesAsync();

                return Results.Ok();
            })
            .RequireAuthorization();
    }
}
