public static class DeleteWorkGuideMainEndpoint{
    public static void MapDeleteWorkGuideMain(this IEndpointRouteBuilder app){
        app.MapDelete("/{id}", async (RecepcionDbContext db, int id) =>{
            var workGuideMain = await db.WorkGuideMains.FindAsync(id);
            if (workGuideMain == null){
                return Results.NotFound();
            }
            db.WorkGuideMains.Remove(workGuideMain);
            await db.SaveChangesAsync();
            return Results.Ok(workGuideMain);
        });
    }
}