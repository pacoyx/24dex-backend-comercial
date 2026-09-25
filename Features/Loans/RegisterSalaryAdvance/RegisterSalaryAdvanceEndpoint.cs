public static class RegisterSalaryAdvanceEndpoint
{
    public static RouteGroupBuilder MapRegisterSalaryAdvance(this RouteGroupBuilder group)
    {
        group.MapPost("/{employeeId:long}/salary-advances", async (long employeeId, IRegisterLoanService service, RegisterLoanRequestDto request) =>
        {
            var result = await service.RegisterLoanAsync(employeeId, LoanType.SalaryAdvance, request);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<EmployeeLoanResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Trabajador con ID {employeeId} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<EmployeeLoanResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status201Created,
                Message = "Adelanto registrado correctamente"
            });
        })
        .WithName("RegisterSalaryAdvance")
        .Produces<ApiResponse<EmployeeLoanResponseDto>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
