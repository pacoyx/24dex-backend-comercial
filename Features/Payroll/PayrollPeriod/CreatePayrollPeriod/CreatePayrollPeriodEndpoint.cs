public static class CreatePayrollPeriodEndpoint
{
    public static RouteGroupBuilder MapCreatePayrollPeriod(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (ICreatePayrollPeriodService service, CreatePayrollPeriodRequestDto request) =>
        {
            var result = await service.CreatePayrollPeriodAsync(request);
            return Results.Ok(new ApiResponse<PayrollPeriodResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status201Created,
                Message = "Periodo de planilla creado correctamente"
            });
        })
        .WithName("CreatePayrollPeriod")
        .Produces<ApiResponse<PayrollPeriodResponseDto>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        return group;
    }
}
