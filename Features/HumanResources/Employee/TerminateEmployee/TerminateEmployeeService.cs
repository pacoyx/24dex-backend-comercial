using Microsoft.EntityFrameworkCore;

public class TerminateEmployeeService : ITerminateEmployeeService
{
    private readonly RecepcionDbContext _context;

    public TerminateEmployeeService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeResponseDto?> TerminateEmployeeAsync(long id, TerminateEmployeeRequestDto request)
    {
        var employee = await _context.Employees
            .Include(x => x.Area)
            .Include(x => x.Position)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            return null;
        }

        employee.IsActive = false;
        employee.TerminationDate = request.TerminationDate;

        await _context.SaveChangesAsync();

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
            AreaName = employee.Area.Name,
            PositionId = employee.PositionId,
            PositionName = employee.Position.Name,
            ContractType = employee.ContractType,
            BaseSalary = employee.BaseSalary
        };
    }
}
