using Microsoft.EntityFrameworkCore;

public static class GetWorkShiftEndpoints
{
    public static void MapGetWorkShift(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, RecepcionDbContext db) =>
        {
            if (id <= 0){
                return Results.BadRequest("Id must be greater than zero");
            }
            
            var workShift = await db.WorkShifts.FindAsync(id);
            if (workShift == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(workShift);
        });
    }
}