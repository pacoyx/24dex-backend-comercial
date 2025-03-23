
public static class GetServicesAccessFast
{
    public static RouteHandlerBuilder MapGetServicesAccessFast(this IEndpointRouteBuilder app)
    {
       return app.MapGet("/getServicesQuickAccess", async (IGetServicesAccessFastService servicesAccessFastService) =>
        {
            Console.WriteLine("consultando BD servicios de acceso rápido");
            var responseDto = await servicesAccessFastService.GetServicesAccessFastAsync();

            var response = new ApiResponse<IEnumerable<GetServicesAccessFastResponseDto>>
            {
                Success = true,
                Data = responseDto,
                StatusCode = StatusCodes.Status200OK,
                Message = "ServicesAccessFast retrieved successfully"
            };

            return Results.Ok(response);
        })
        .CacheOutput(); // Configurar caché para 60 segundos
        // .RequireAuthorization();
    }
}