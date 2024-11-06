public static class CancelItemWorkGuideEndpoint{
    public static void MapCancelItemWorkGuide(this IEndpointRouteBuilder app){
        app.MapPut("/cancelItem/{id}", async (RecepcionDbContext db, int id) =>
        {
            var workGuideItem = await db.WorkGuideDetails.FindAsync(id);
            if (workGuideItem == null)
            {
                var responseValidation = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "Item no encontrado",
                    StatusCode = 404,
                    Success = false
                };
                return Results.NotFound(responseValidation);
            }

            if (workGuideItem.EstadoRegistro == "I")
            {
                var responseValidation = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "El item ya se encuentra anulado",
                    StatusCode = 400,
                    Success = false
                };
                return Results.BadRequest(responseValidation);
            }

            workGuideItem.EstadoRegistro = "I";
            await db.SaveChangesAsync();

            var response = new ApiResponse<string>()
            {
                Data = "El item fue anulado con exito",
                Message = "El item fue anulado con exito",
                Success = true,
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}