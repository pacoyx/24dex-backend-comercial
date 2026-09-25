using Microsoft.EntityFrameworkCore;

public static class GetUpcomingBirthdaysEndpoint
{
    public static void MapGetUpcomingBirthdays(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/hr-dashboard/upcoming-birthdays", async (RecepcionDbContext db, int? days) =>
        {
            var rangeDays = days is > 0 ? days.Value : 30;
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var employees = await db.Employees.AsNoTracking()
                .Where(x => x.IsActive)
                .Select(x => new { x.Id, x.FirstName, x.LastName, x.BirthDate })
                .ToListAsync();

            var result = employees
                .Select(x =>
                {
                    var nextBirthday = new DateOnly(today.Year, x.BirthDate.Month, x.BirthDate.Day == 29 && x.BirthDate.Month == 2 && !DateTime.IsLeapYear(today.Year) ? 28 : x.BirthDate.Day);
                    if (nextBirthday < today)
                    {
                        nextBirthday = nextBirthday.AddYears(1);
                    }

                    return new UpcomingBirthdayResponseDto
                    {
                        EmployeeId = x.Id,
                        EmployeeName = $"{x.FirstName} {x.LastName}",
                        BirthDate = x.BirthDate,
                        DaysUntilBirthday = nextBirthday.DayNumber - today.DayNumber
                    };
                })
                .Where(x => x.DaysUntilBirthday <= rangeDays)
                .OrderBy(x => x.DaysUntilBirthday)
                .ToList();

            return Results.Ok(new ApiResponse<IEnumerable<UpcomingBirthdayResponseDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Próximos cumpleaños obtenidos correctamente"
            });
        })
        .WithName("GetUpcomingBirthdays")
        .WithTags("HR - Dashboard")
        .Produces<ApiResponse<IEnumerable<UpcomingBirthdayResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();
    }
}
