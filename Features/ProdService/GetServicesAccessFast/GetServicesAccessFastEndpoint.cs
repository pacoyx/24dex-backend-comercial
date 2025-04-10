
public static class GetServicesAccessFast
{
    public static RouteHandlerBuilder MapGetServicesAccessFast(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/getServicesQuickAccess", async (IGetServicesAccessFastService servicesAccessFastService, IAppLogger<string> logger, HttpContext context) =>
         {

             var jwt = context.Request.Headers["Authorization"].FirstOrDefault();
             Console.WriteLine($"JWT recibido: {jwt}");

             logger.LogInformacion("consultando BD servicios de acceso rápido");
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
         .RequireAuthorization()
         .CacheOutput("JWT_Aware_Cache");
    }
}