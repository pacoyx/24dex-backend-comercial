public static class RegisterLocationClothesEndpoint{
    public static void MapRegisterLocationWorkGuide(this IEndpointRouteBuilder app)
    {
        app.MapPost("/RegisterWorkGuide", async (RecepcionDbContext context, CreateLocationWorkGuideRequestDto request) =>
        {
            
            var numeroGuia = request.NumeroGuia.Substring(0, request.NumeroGuia.Length - 1);
            var letraGuia = request.NumeroGuia.Substring(request.NumeroGuia.Length - 1);

            var workGuideId = context.WorkGuideMains
                .Where(wg => wg.NumeroGuia == numeroGuia)
                .Select(wg => wg.Id)
                .FirstOrDefault();

            var workGuideDetailId = context.WorkGuideDetails
                .Where(wgd => wgd.WorkGuideMainId == workGuideId && wgd.Identificador == letraGuia)
                .Select(wgd => wgd.Id)
                .FirstOrDefault();


            var location = new LocationWorkGuide
            {
                LocationClothesId = request.LocationClothesId,
                WorkGuideId = workGuideId,
                WorkGuideDetailId = workGuideDetailId,
                Comments = request.Comments,
                NumeroGuia = request.NumeroGuia
            };

            context.LocationWorkGuides.Add(location);
            await context.SaveChangesAsync();

            var response = new ApiResponse<LocationWorkGuide>
            {
                Data = location,
                Message = "Location created",
                StatusCode = StatusCodes.Status201Created,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}