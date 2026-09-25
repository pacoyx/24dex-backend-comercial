using Microsoft.EntityFrameworkCore;

public class GetEmployeesService : IGetEmployeesService
{
    private readonly RecepcionDbContext _context;

    public GetEmployeesService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<GetEmployeesResponseDto> GetEmployeesAsync(int pageNumber, int pageSize, bool? onlyActive, int? areaId, string? search)
    {
        var query = _context.Employees
            .AsNoTracking()
            .Include(x => x.Area)
            .Include(x => x.Position)
            .AsQueryable();

        if (onlyActive == true)
        {
            query = query.Where(x => x.IsActive);
        }

        if (areaId.HasValue)
        {
            query = query.Where(x => x.AreaId == areaId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                x.DocumentNumber.Contains(search) ||
                x.Code.Contains(search));
        }

        var totalCount = await query.CountAsync();

        var employees = await query
            .OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new EmployeeListItemDto
            {
                Id = x.Id,
                Code = x.Code,
                DocumentNumber = x.DocumentNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                HireDate = x.HireDate,
                IsActive = x.IsActive,
                AreaId = x.AreaId,
                AreaName = x.Area.Name,
                PositionId = x.PositionId,
                PositionName = x.Position.Name,
                ContractType = x.ContractType,
                BaseSalary = x.BaseSalary
            })
            .ToListAsync();

        return new GetEmployeesResponseDto
        {
            Employees = employees,
            TotalCount = totalCount
        };
    }
}
