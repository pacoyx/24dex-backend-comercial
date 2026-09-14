using Microsoft.EntityFrameworkCore;

public static class ErrorPrintJobEndpoint
{
    public static void MapErrorPrintJob(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/printjobs/{id:long}/error",
            async (
                long id,
                PrintErrorRequest request,
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
                    PrintStatus.Error;

                printJob.ErrorMessage =
                    request.ErrorMessage;

                await db.SaveChangesAsync();

                return Results.Ok();
            });
    }
}
