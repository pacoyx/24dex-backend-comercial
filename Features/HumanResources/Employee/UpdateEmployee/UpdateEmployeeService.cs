using Microsoft.EntityFrameworkCore;

public class UpdateEmployeeService : IUpdateEmployeeService
{
    private readonly RecepcionDbContext _context;

    public UpdateEmployeeService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeResponseDto?> UpdateEmployeeAsync(long id, UpdateEmployeeRequestDto request)
    {
        var employee = await _context.Employees
            .Include(x => x.Area)
            .Include(x => x.Position)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            return null;
        }

        if (employee.BaseSalary != request.BaseSalary)
        {
            _context.EmployeeSalaryHistories.Add(new EmployeeSalaryHistory
            {
                EmployeeId = employee.Id,
                Salary = request.BaseSalary,
                EffectiveDate = DateTime.UtcNow
            });
        }

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.PhoneNumber = request.PhoneNumber;
        employee.Address = request.Address;
        employee.AreaId = request.AreaId;
        employee.PositionId = request.PositionId;
        employee.ContractType = request.ContractType;
        employee.BaseSalary = request.BaseSalary;

        await _context.SaveChangesAsync();

        var area = await _context.Areas.AsNoTracking().FirstAsync(x => x.Id == employee.AreaId);
        var position = await _context.Positions.AsNoTracking().FirstAsync(x => x.Id == employee.PositionId);

        return new EmployeeResponseDto
        {
            Id = employee.Id,
            Code = employee.Code,
            DocumentNumber = employee.DocumentNumber,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Address = employee.Address,
            BirthDate = employee.BirthDate,
            HireDate = employee.HireDate,
            TerminationDate = employee.TerminationDate,
            IsActive = employee.IsActive,
            AreaId = employee.AreaId,
            AreaName = area.Name,
            PositionId = employee.PositionId,
            PositionName = position.Name,
            ContractType = employee.ContractType,
            BaseSalary = employee.BaseSalary
        };
    }
}
