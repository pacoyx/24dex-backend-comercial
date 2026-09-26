using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CustomerContactEndpoints
{
    public static void MapCustomerContacts(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/contacts", async (int id, RecepcionDbContext db) =>
        {
            var customerExists = await db.Customers.AnyAsync(x => x.Id == id);
            if (!customerExists)
            {
                return Results.NotFound("Cliente no encontrado.");
            }

            var contacts = await db.CustomerContacts
                .AsNoTracking()
                .Where(x => x.CustomerId == id)
                .OrderByDescending(x => x.IsPrimary)
                .ThenBy(x => x.FullName)
                .Select(x => new CustomerContactResponseDto(
                    x.Id, x.CustomerId, x.FullName, x.Position, x.PhoneNumber, x.Email, x.IsPrimary))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<CustomerContactResponseDto>>
            {
                Success = true,
                Message = "Contactos obtenidos correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = contacts
            });
        })
        .WithName("GetCustomerContacts")
        .Produces<ApiResponse<List<CustomerContactResponseDto>>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        app.MapPost("/{id:int}/contacts", async (
            int id,
            CustomerContactRequestDto request,
            IValidator<CustomerContactRequestDto> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var customerExists = await db.Customers.AnyAsync(x => x.Id == id);
            if (!customerExists)
            {
                return Results.NotFound("Cliente no encontrado.");
            }

            var contact = new CustomerContact
            {
                CustomerId = id,
                FullName = request.FullName,
                Position = request.Position,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IsPrimary = request.IsPrimary
            };

            await db.CustomerContacts.AddAsync(contact);
            await db.SaveChangesAsync();

            var response = new ApiResponse<CustomerContactResponseDto>
            {
                Success = true,
                Message = "Contacto registrado correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = ToResponse(contact)
            };

            return Results.Created($"/api/customer-contacts/{contact.Id}", response);
        })
        .WithName("CreateCustomerContact")
        .Produces<ApiResponse<CustomerContactResponseDto>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }

    public static void MapCustomerContactCommands(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:long}", async (
            long id,
            CustomerContactRequestDto request,
            IValidator<CustomerContactRequestDto> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var contact = await db.CustomerContacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact is null)
            {
                return Results.NotFound("Contacto no encontrado.");
            }

            contact.FullName = request.FullName;
            contact.Position = request.Position;
            contact.PhoneNumber = request.PhoneNumber;
            contact.Email = request.Email;
            contact.IsPrimary = request.IsPrimary;

            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<CustomerContactResponseDto>
            {
                Success = true,
                Message = "Contacto actualizado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = ToResponse(contact)
            });
        })
        .WithName("UpdateCustomerContact")
        .Produces<ApiResponse<CustomerContactResponseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        app.MapDelete("/{id:long}", async (long id, RecepcionDbContext db) =>
        {
            var contact = await db.CustomerContacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact is null)
            {
                return Results.NotFound("Contacto no encontrado.");
            }

            db.CustomerContacts.Remove(contact);
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Contacto eliminado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { }
            });
        })
        .WithName("DeleteCustomerContact")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }

    private static CustomerContactResponseDto ToResponse(CustomerContact contact) => new(
        contact.Id,
        contact.CustomerId,
        contact.FullName,
        contact.Position,
        contact.PhoneNumber,
        contact.Email,
        contact.IsPrimary);
}
