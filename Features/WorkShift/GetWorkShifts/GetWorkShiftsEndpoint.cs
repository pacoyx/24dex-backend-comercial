using Microsoft.EntityFrameworkCore;

public static class GetWorkShiftsEndpoints
{
    public static void MapGetWorkShifts(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var workShifts = await db.WorkShifts.AsNoTracking().ToListAsync();
            return Results.Ok(workShifts);
        });
    }
}