using _24dex_backend_comercial.Features.WorkGuideMain.CreateWorkGuideMain;
using Microsoft.EntityFrameworkCore;

public static class ReprintPrintQueueEndpoint
{
    public static void MapReprintPrintQueue(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/admin/print-queues/{id:long}/reprint",
            async (
                long id,
                RecepcionDbContext db,
                IPrintNotifier printNotifier) =>
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
                    PrintStatus.Pending;

                queue.RetryCount++;

                queue.ErrorMessage = null;

                await db.SaveChangesAsync();

                await printNotifier.NotifyAsync(
                    queue.PrinterId,
                    queue.Id);

                return Results.Ok();
            })
            .RequireAuthorization();
    }
}
