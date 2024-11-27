public static class RegisterLocationClothesEndpoint{
    public static void MapRegisterLocationWorkGuide(this IEndpointRouteBuilder app)
    {
        app.MapPost("/RegisterWorkGuide", async (RecepcionDbContext context, CreateLocationWorkGuideRequestDto request) =>
        {
            
            var numeroGuia = request.NumeroGuia.Substring(0, request.NumeroGuia.Length - 1);
            var letraGuia = request.NumeroGuia.Substring(request.NumeroGuia.Length - 1);

            if(string.IsNullOrEmpty(numeroGuia) || string.IsNullOrEmpty(letraGuia)){
                var responseVal = new ApiResponse<string>
                {
                    Data = "",
                    Message = "NumeroGuia ó LetraGuia esta vacio",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Success = false
                };
                return Results.BadRequest(responseVal);
            }


            var workGuideId = context.WorkGuideMains
                .Where(wg => wg.NumeroGuia == numeroGuia)
                .Select(wg => wg.Id)
                .FirstOrDefault();

            if (workGuideId == 0 ){
                var responseValMain = new ApiResponse<string>
                {
                    Data = "",
                    Message = "Numero de Guia no encontrado",
                    StatusCode = StatusCodes.Status404NotFound,
                    Success = false
                };
                return Results.NotFound(responseValMain);
            }


            var workGuideDetailId = context.WorkGuideDetails
                .Where(wgd => wgd.WorkGuideMainId == workGuideId && wgd.Identificador == letraGuia)
                // .Select(wgd => wgd.Id)
                .FirstOrDefault();

            if (workGuideDetailId == null){
                var responseValDetail = new ApiResponse<string>
                {
                    Data = "",
                    Message = "WorkGuideDetail not found",
                    StatusCode = StatusCodes.Status404NotFound,
                    Success = false
                };
                return Results.NotFound(responseValDetail);
            }


            var location = new LocationWorkGuide
            {
                LocationClothesId = request.LocationClothesId,
                WorkGuideId = workGuideId,
                WorkGuideDetailId = workGuideDetailId.Id,
                Comments = request.Comments,
                NumeroGuia = request.NumeroGuia
            };

            var ubicacion = context.LocationClothes
                .Where(lc => lc.Id == request.LocationClothesId)
                .Select(lc => lc.Name)
                .FirstOrDefault();

            if (ubicacion == null){
                var responseValLocation = new ApiResponse<string>
                {
                    Data = "",
                    Message = "Location not found",
                    StatusCode = StatusCodes.Status404NotFound,
                    Success = false
                };
                return Results.NotFound(responseValLocation);
            }

            workGuideDetailId.Ubicacion = ubicacion;

            

            context.LocationWorkGuides.Add(location);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "",
                Message = "Location created",
                StatusCode = StatusCodes.Status201Created,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}