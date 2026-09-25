public static class GetLoansEndpoint
{
    public static RouteGroupBuilder MapGetLoans(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetLoansService service, long? employeeId, LoanType? loanType, bool? isClosed) =>
        {
            var result = await service.GetLoansAsync(employeeId, loanType, isClosed);
            return Results.Ok(new ApiResponse<IEnumerable<LoanListItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Préstamos y adelantos obtenidos correctamente"
            });
        })
        .WithName("GetLoans")
        .Produces<ApiResponse<IEnumerable<LoanListItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
