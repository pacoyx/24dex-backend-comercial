public static class GetPayrollEndpoint
{
    public static RouteGroupBuilder MapGetPayroll(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:long}/payroll", async (long id, IGetPayrollService service) =>
        {
            var result = await service.GetPayrollAsync(id);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<IEnumerable<PayrollSummaryDto>>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Periodo de planilla con ID {id} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<IEnumerable<PayrollSummaryDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Planilla obtenida correctamente"
            });
        })
        .WithName("GetPayroll")
        .Produces<ApiResponse<IEnumerable<PayrollSummaryDto>>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
