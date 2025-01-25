
public static class GetServicesAccessFast
{
    public static void MapGetServicesAccessFast(this IEndpointRouteBuilder app)
    {
        app.MapGet("/getServicesQuickAccess", async (IGetServicesAccessFastService servicesAccessFastService) =>
        {
            var responseDto = await servicesAccessFastService.GetServicesAccessFastAsync();
            
            var response = new ApiResponse<IEnumerable<GetServicesAccessFastResponseDto>>
            {
                Success = true,
                Data = responseDto,
                StatusCode = StatusCodes.Status200OK,
                Message = "ServicesAccessFast retrieved successfully"
            };

            return Results.Ok(response);
        }).RequireAuthorization();
    }
}