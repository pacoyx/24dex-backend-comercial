public static class DeleteWorkShiftEndpoint
{
    public static void MapDeleteWorkShift(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext db, int id) =>
        {
            if (id <= 0)
            {
                return Results.BadRequest("Id must be greater than zero");
            }

            var workShift = await db.WorkShifts.FindAsync(id);
            if (workShift == null)
            {
                return Results.NotFound();
            }
            db.WorkShifts.Remove(workShift);
            await db.SaveChangesAsync();
            return Results.Ok(workShift);
        });

    }
}