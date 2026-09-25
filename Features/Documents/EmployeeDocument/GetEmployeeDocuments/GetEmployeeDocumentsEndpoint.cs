public static class GetEmployeeDocumentsEndpoint
{
    public static RouteGroupBuilder MapGetEmployeeDocuments(this RouteGroupBuilder group)
    {
        group.MapGet("/{employeeId:long}/documents", async (long employeeId, IGetEmployeeDocumentsService service) =>
        {
            var result = await service.GetEmployeeDocumentsAsync(employeeId);
            return Results.Ok(new ApiResponse<IEnumerable<EmployeeDocumentResponseDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Documentos obtenidos correctamente"
            });
        })
        .WithName("GetEmployeeDocuments")
        .Produces<ApiResponse<IEnumerable<EmployeeDocumentResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
