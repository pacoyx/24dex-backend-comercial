public static class DeleteEmployeeDocumentEndpoint
{
    public static RouteGroupBuilder MapDeleteEmployeeDocument(this RouteGroupBuilder group)
    {
        group.MapDelete("/{employeeId:long}/documents/{documentId:long}", async (long employeeId, long documentId, IDeleteEmployeeDocumentService service) =>
        {
            var deleted = await service.DeleteEmployeeDocumentAsync(employeeId, documentId);
            if (!deleted)
            {
                return Results.NotFound(new ApiResponse<string>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Documento con ID {documentId} no encontrado para el trabajador {employeeId}."
                });
            }

            return Results.Ok(new ApiResponse<string>
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Documento eliminado correctamente"
            });
        })
        .WithName("DeleteEmployeeDocument")
        .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
