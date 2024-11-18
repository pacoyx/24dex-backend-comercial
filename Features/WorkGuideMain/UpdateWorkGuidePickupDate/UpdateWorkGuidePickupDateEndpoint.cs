using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

public static class UpdateWorkGuidePickupDateEndpoint{
    public static void MapUpdateWorkGuidePickupDate(this IEndpointRouteBuilder app){
        app.MapPut("/pickupDate/{id}", async (int id, RecepcionDbContext db) => {
            var workGuide = await db.WorkGuideMains.FindAsync(id);
            if (workGuide == null){
                var responseErr = new ApiResponse<string>{
                    Data = "WorkGuide not found",
                    Message = "WorkGuide not found",
                    Success = false,
                    StatusCode = 404,
                    Errors = new List<string> { "guia no se encuentra" }
                };
                return Results.NotFound(responseErr);
            }

            var workGuideDetails = await db.WorkGuideDetails.Where(wgd => wgd.WorkGuideMainId == id).ToListAsync();
            foreach (var detail in workGuideDetails)
            {
                detail.FechaRecojo = DateTime.Now;
                detail.EstadoSituacion = "E";
            }

            workGuide.FechaRecojo = DateTime.Now;
            workGuide.EstadoSituacion = "E";
            await db.SaveChangesAsync();

            var response = new ApiResponse<string>{
                Data = "WorkGuide updated",
                Message = "WorkGuide updated",
                Success = true,
                StatusCode = 200
            };

            return Results.Ok(response);
            
        });
    }
}