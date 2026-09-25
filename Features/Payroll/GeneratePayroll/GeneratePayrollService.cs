using Microsoft.EntityFrameworkCore;

public class GeneratePayrollService : IGeneratePayrollService
{
    private const int StandardDaysPerMonth = 30;
    private const int StandardHoursPerDay = 8;

    private readonly RecepcionDbContext _context;

    public GeneratePayrollService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<GeneratePayrollResponseDto?> GeneratePayrollAsync(long payrollPeriodId)
    {
        var period = await _context.PayrollPeriods.FirstOrDefaultAsync(x => x.Id == payrollPeriodId);
        if (period == null)
        {
            return null;
        }

        var alreadyGeneratedEmployeeIds = await _context.Payrolls
            .Where(x => x.PayrollPeriodId == payrollPeriodId)
            .Select(x => x.EmployeeId)
            .ToListAsync();

        var employees = await _context.Employees
            .Where(x => x.IsActive
                && x.HireDate <= period.EndDate.ToDateTime(TimeOnly.MinValue)
                && (x.TerminationDate == null || x.TerminationDate >= period.StartDate.ToDateTime(TimeOnly.MinValue))
                && !alreadyGeneratedEmployeeIds.Contains(x.Id))
            .ToListAsync();

        var periodDays = period.EndDate.DayNumber - period.StartDate.DayNumber + 1;
        var summaries = new List<PayrollSummaryDto>();
        var generatedPayrolls = new List<Payroll>();

        foreach (var employee in employees)
        {
            var dailyRate = employee.BaseSalary / StandardDaysPerMonth;
            var hourlyRate = dailyRate / StandardHoursPerDay;

            var baseAmount = decimal.Round(dailyRate * periodDays, 2);

            var extraHours = await _context.EmployeeExtraHours
                .Where(x => x.EmployeeId == employee.Id
                    && x.Approved
                    && x.Date >= period.StartDate && x.Date <= period.EndDate)
                .SumAsync(x => (decimal?)x.Hours) ?? 0;
            var extraHoursAmount = decimal.Round(extraHours * hourlyRate, 2);

            var absenceHours = await _context.AttendanceIncidents
                .Where(x => x.EmployeeId == employee.Id
                    && x.Type == AttendanceIncidentType.Absence
                    && x.Date >= period.StartDate && x.Date <= period.EndDate)
                .SumAsync(x => (decimal?)x.Hours) ?? 0;
            var absenceDiscount = decimal.Round(absenceHours * hourlyRate, 2);

            var lateArrivalHours = await _context.AttendanceIncidents
                .Where(x => x.EmployeeId == employee.Id
                    && x.Type == AttendanceIncidentType.LateArrival
                    && x.Date >= period.StartDate && x.Date <= period.EndDate)
                .SumAsync(x => (decimal?)x.Hours) ?? 0;
            var lateArrivalDiscount = decimal.Round(lateArrivalHours * hourlyRate, 2);

            var openLoans = await _context.EmployeeLoans
                .Where(x => x.EmployeeId == employee.Id && !x.IsClosed && x.Balance > 0)
                .ToListAsync();

            var loanDiscount = openLoans.Where(x => x.LoanType == LoanType.Loan).Sum(x => x.Balance);
            var advanceDiscount = openLoans.Where(x => x.LoanType == LoanType.SalaryAdvance).Sum(x => x.Balance);

            foreach (var loan in openLoans)
            {
                loan.Balance = 0;
                loan.IsClosed = true;
            }

            var grossAmount = baseAmount + extraHoursAmount;
            var totalDiscounts = absenceDiscount + lateArrivalDiscount + loanDiscount + advanceDiscount;
            var netAmount = grossAmount - totalDiscounts;

            var payroll = new Payroll
            {
                PayrollPeriodId = period.Id,
                EmployeeId = employee.Id,
                GrossAmount = grossAmount,
                Discounts = totalDiscounts,
                NetAmount = netAmount,
                GeneratedAt = DateTime.UtcNow
            };

            var details = new List<PayrollDetail>
            {
                new() { ConceptType = PayrollConceptType.BaseSalary, Description = "Sueldo base", Amount = baseAmount }
            };

            if (extraHoursAmount > 0)
            {
                details.Add(new PayrollDetail { ConceptType = PayrollConceptType.ExtraHours, Description = $"Horas extras ({extraHours}h)", Amount = extraHoursAmount });
            }

            if (absenceDiscount > 0)
            {
                details.Add(new PayrollDetail { ConceptType = PayrollConceptType.AbsenceDiscount, Description = $"Descuento por faltas ({absenceHours}h)", Amount = -absenceDiscount });
            }

            if (lateArrivalDiscount > 0)
            {
                details.Add(new PayrollDetail { ConceptType = PayrollConceptType.LateArrivalDiscount, Description = $"Descuento por tardanzas ({lateArrivalHours}h)", Amount = -lateArrivalDiscount });
            }

            if (loanDiscount > 0)
            {
                details.Add(new PayrollDetail { ConceptType = PayrollConceptType.LoanDiscount, Description = "Descuento por préstamos", Amount = -loanDiscount });
            }

            if (advanceDiscount > 0)
            {
                details.Add(new PayrollDetail { ConceptType = PayrollConceptType.AdvanceDiscount, Description = "Descuento por adelantos", Amount = -advanceDiscount });
            }

            payroll.PayrollDetails = details;

            _context.Payrolls.Add(payroll);
            generatedPayrolls.Add(payroll);

            summaries.Add(new PayrollSummaryDto
            {
                EmployeeId = employee.Id,
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                GrossAmount = grossAmount,
                Discounts = totalDiscounts,
                NetAmount = netAmount,
                GeneratedAt = payroll.GeneratedAt,
                Details = details.Select(d => new PayrollDetailLineDto
                {
                    ConceptType = d.ConceptType,
                    Description = d.Description,
                    Amount = d.Amount
                }).ToList()
            });
        }

        await _context.SaveChangesAsync();

        for (var i = 0; i < summaries.Count; i++)
        {
            summaries[i] = summaries[i] with { Id = generatedPayrolls[i].Id };
        }

        return new GeneratePayrollResponseDto
        {
            PayrollPeriodId = period.Id,
            GeneratedCount = summaries.Count,
            SkippedCount = alreadyGeneratedEmployeeIds.Count,
            TotalNetAmount = summaries.Sum(x => x.NetAmount),
            Payrolls = summaries
        };
    }
}
