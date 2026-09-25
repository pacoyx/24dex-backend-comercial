public static class CreateEmployeeDocumentEndpoint
{
    public static RouteGroupBuilder MapCreateEmployeeDocument(this RouteGroupBuilder group)
    {
        group.MapPost("/{employeeId:long}/documents", async (long employeeId, ICreateEmployeeDocumentService service, CreateEmployeeDocumentRequestDto request) =>
        {
            var result = await service.CreateEmployeeDocumentAsync(employeeId, request);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<EmployeeDocumentResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Trabajador con ID {employeeId} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<EmployeeDocumentResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status201Created,
                Message = "Documento registrado correctamente"
            });
        })
        .WithName("CreateEmployeeDocument")
        .Produces<ApiResponse<EmployeeDocumentResponseDto>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
