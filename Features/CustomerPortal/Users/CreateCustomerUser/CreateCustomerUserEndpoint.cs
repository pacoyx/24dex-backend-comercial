using System.Security.Claims;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreateCustomerUserEndpoint
{
    public static void MapCreateCustomerUser(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (
            ClaimsPrincipal principal,
            CreateCustomerUserRequest request,
            IValidator<CreateCustomerUserRequest> validator,
            RecepcionDbContext db) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var roleExists = await db.CustomerRoles.AnyAsync(x => x.Id == request.CustomerRoleId && x.IsActive);
            if (!roleExists)
            {
                return Results.BadRequest(new[] { "Rol no encontrado o inactivo." });
            }

            var userNameTaken = await db.CustomerUsers.AnyAsync(x => x.UserName == request.UserName);
            if (userNameTaken)
            {
                return Results.BadRequest(new[] { "El nombre de usuario ya está en uso." });
            }

            var emailTaken = await db.CustomerUsers.AnyAsync(x => x.Email == request.Email);
            if (emailTaken)
            {
                return Results.BadRequest(new[] { "El correo ya está registrado." });
            }

            DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

            // Siempre queda asociado al CustomerId del administrador autenticado, nunca al del request.
            var user = new CustomerUser
            {
                CustomerId = customerId.Value,
                CustomerRoleId = request.CustomerRoleId,
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName,
                PasswordHash = string.Empty,
                IsActive = false,
                CreatedAt = fechaOperacion
            };

            await db.CustomerUsers.AddAsync(user);
            await db.SaveChangesAsync();

            await db.Entry(user).Reference(x => x.CustomerRole).LoadAsync();

            var response = new ApiResponse<CustomerUserResponse>
            {
                Success = true,
                Message = "Usuario registrado correctamente. Genere el enlace de activación para que pueda establecer su contraseña.",
                StatusCode = StatusCodes.Status201Created,
                Data = CustomerUsersEndpoints.ToResponse(user)
            };

            return Results.Created($"/api/customer-portal/users/{user.Id}", response);
        })
        .WithName("CreateCustomerPortalUser")
        .Produces<ApiResponse<CustomerUserResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .RequireCustomerPermission(CustomerPermissionCodes.UsersManage);
    }
}
