using Microsoft.EntityFrameworkCore;

public static class GetEmployeesByAreaEndpoint
{
    public static void MapGetEmployeesByArea(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/hr-dashboard/employees-by-area", async (RecepcionDbContext db) =>
        {
            var result = await db.Areas.AsNoTracking()
                .Where(a => a.IsActive)
                .Select(a => new EmployeesByAreaResponseDto
                {
                    AreaId = a.Id,
                    AreaName = a.Name,
                    EmployeeCount = db.Employees.Count(e => e.AreaId == a.Id && e.IsActive)
                })
                .OrderByDescending(x => x.EmployeeCount)
                .ToListAsync();

            return Results.Ok(new ApiResponse<IEnumerable<EmployeesByAreaResponseDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Personal por área obtenido correctamente"
            });
        })
        .WithName("GetEmployeesByArea")
        .WithTags("HR - Dashboard")
        .Produces<ApiResponse<IEnumerable<EmployeesByAreaResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();
    }
}
