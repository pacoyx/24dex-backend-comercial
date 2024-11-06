public static class CancelWorkGuideEndpoint
{
    public static void MapCancelWorkGuide(this IEndpointRouteBuilder app)
    {
        app.MapPut("/cancelWorkGuide/{id}", async (RecepcionDbContext db, int id) =>
        {
            var workGuide = await db.WorkGuideMains.FindAsync(id);
            if (workGuide == null)
            {
                var responseValidation = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "Guia no encontrada",
                    StatusCode = 404,
                    Success = false
                };
                return Results.NotFound(responseValidation);
            }

            if (workGuide.EstadoRegistro == "I")
            {
                var responseValidation = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "La guia ya se encuentra anulada",
                    StatusCode = 400,
                    Success = false
                };
                return Results.BadRequest(responseValidation);
            }


            workGuide.EstadoRegistro = "I";
            await db.SaveChangesAsync();

            var response = new ApiResponse<string>()
            {
                Data = "la guia fue anulada con exito",
                Message = "la guia fue anulada con exito",
                Success = true,
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}