public static class GetEmployeeDocumentTypesEndpoint
{
    public static void MapGetEmployeeDocumentTypes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/hr/document-types", async (IGetEmployeeDocumentTypesService service) =>
        {
            var result = await service.GetEmployeeDocumentTypesAsync();
            return Results.Ok(new ApiResponse<IEnumerable<EmployeeDocumentTypeResponseDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Tipos de documento obtenidos correctamente"
            });
        })
        .WithName("GetEmployeeDocumentTypes")
        .WithTags("HR - Documents")
        .Produces<ApiResponse<IEnumerable<EmployeeDocumentTypeResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();
    }
}
