public static class GetPickupMobileEmployeesEndpoint
{
    public static void MapGetPickupMobileEmployees(this IEndpointRouteBuilder app)
    {
        app.MapGet("/employees", async (IGetEmployeesService service) =>
        {
            // Reutiliza el servicio existente de RR.HH.; solo se reduce el DTO para la tablet.
            var result = await service.GetEmployeesAsync(1, int.MaxValue, onlyActive: true, areaId: null, search: null);

            var response = result.Employees
                .Select(e => new PickupMobileEmployeeLookupDto(e.Id, $"{e.FirstName} {e.LastName}".Trim()))
                .ToList();

            return Results.Ok(new ApiResponse<List<PickupMobileEmployeeLookupDto>>
            {
                Success = true,
                Message = "Empleados disponibles para recojo.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .WithName("GetPickupMobileEmployees")
        .Produces<ApiResponse<List<PickupMobileEmployeeLookupDto>>>(StatusCodes.Status200OK);
    }
}
