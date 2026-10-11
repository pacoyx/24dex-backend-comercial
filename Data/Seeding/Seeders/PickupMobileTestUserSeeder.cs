using Microsoft.EntityFrameworkCore;

// Seeder de datos de prueba para habilitar la PWA Laundry.PickupMobile en desarrollo:
// crea (si no existen) un Employee, un User con rol "Recolector" (permiso pickup_mobile.access)
// y una sucursal + asignación activa en BrachSalesUsers para poder crear guías.
// Cada paso es idempotente de forma independiente (no todo-o-nada), para poder completar
// pasos que falten aunque el usuario ya se haya creado en una corrida anterior.
public class PickupMobileTestUserSeeder : IApplicationSeeder
{
    public const string TestUserName = "recolector.demo";
    public const string TestPassword = "Recojo2026!";

    private readonly IEncryptService _encryptService;

    public PickupMobileTestUserSeeder(IEncryptService encryptService)
    {
        _encryptService = encryptService;
    }

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var recolectorRole = await context.Roles
            .FirstOrDefaultAsync(x => x.Name == RoleNames.Recolector, cancellationToken);
        if (recolectorRole is null)
        {
            return;
        }

        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == TestUserName, cancellationToken);

        if (user is null)
        {
            var area = await context.Areas.FirstOrDefaultAsync(x => x.Name == "Despacho", cancellationToken)
                ?? await context.Areas.FirstOrDefaultAsync(cancellationToken);
            var position = await context.Positions.FirstOrDefaultAsync(x => x.Name == "Motorizado", cancellationToken)
                ?? await context.Positions.FirstOrDefaultAsync(cancellationToken);

            if (area is null || position is null)
            {
                return;
            }

            var employee = new Employee
            {
                Code = "REC-DEMO",
                DocumentNumber = "00000001",
                FirstName = "Recolector",
                LastName = "Demo",
                PhoneNumber = "999999999",
                BirthDate = new DateTime(1995, 1, 1),
                HireDate = new DateTime(2026, 1, 1),
                IsActive = true,
                AreaId = area.Id,
                PositionId = position.Id,
                ContractType = EmployeeContractType.Monthly,
                BaseSalary = 0
            };
            context.Employees.Add(employee);
            await context.SaveChangesAsync(cancellationToken);

            var keyPwh = _encryptService.HashPassword(TestPassword);
            var hashPassword = keyPwh.Split(':')[0];
            var password = keyPwh.Split(':')[1];

            user = new User
            {
                Name = "Recolector Demo",
                UserName = TestUserName,
                Password = password,
                HashPassword = hashPassword,
                Role = RoleNames.Recolector,
                Status = "A",
                Email = "recolector.demo@laundry.local",
                EmployeeId = employee.Id
            };
            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
        }

        var hasRole = await context.UserRoles
            .AnyAsync(x => x.UserId == user.Id && x.RoleId == recolectorRole.Id, cancellationToken);
        if (!hasRole)
        {
            context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = recolectorRole.Id });
            await context.SaveChangesAsync(cancellationToken);
        }

        var hasBranchAssignment = await context.BrachSalesUsers
            .AnyAsync(x => x.UserId == user.Id && x.Status == "A", cancellationToken);
        if (!hasBranchAssignment)
        {
            var branch = await context.BranchSales.FirstOrDefaultAsync(x => x.IsActive, cancellationToken);
            if (branch is null)
            {
                // No hay ninguna sucursal activa en este entorno: se crea una mínima para que el
                // usuario de prueba pueda completar el flujo de Nueva Guía (requiere BranchSalesId).
                branch = new BranchSales
                {
                    Code = "DEMO",
                    Description = "Sucursal Demo",
                    Address = "Av. Demo 123",
                    Status = "A",
                    IsActive = true,
                    DefaultPrinterId = 0,
                    CreatedAt = new DateTime(2026, 1, 1)
                };
                context.BranchSales.Add(branch);
                await context.SaveChangesAsync(cancellationToken);
            }

            context.BrachSalesUsers.Add(new BrachSalesUser
            {
                BranchSalesId = branch.Id,
                UserId = user.Id,
                Status = "A"
            });
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
