using System.Security.Claims;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class MyProfileEndpoints
{
    public static void MapMyProfile(this IEndpointRouteBuilder app)
    {
        app.MapGet("/me", async (ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                return Results.NotFound();
            }

            var response = new ApiResponse<MyProfileResponseDto>
            {
                Success = true,
                Message = "Perfil obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new MyProfileResponseDto(
                    user.Id,
                    user.Name,
                    user.UserName,
                    user.Email,
                    user.Phone,
                    user.Role,
                    user.Status,
                    user.PhotoUrl)
            };

            return Results.Ok(response);
        })
        .WithName("GetMyProfile")
        .Produces<ApiResponse<MyProfileResponseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithTags("UserProfile")
        .RequireAuthorization();

        app.MapPut("/me", async (
            ClaimsPrincipal principal,
            UpdateMyProfileRequestDto request,
            IValidator<UpdateMyProfileRequestDto> validator,
            RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                return Results.NotFound();
            }

            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            await db.SaveChangesAsync();

            var response = new ApiResponse<MyProfileResponseDto>
            {
                Success = true,
                Message = "Datos actualizados correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new MyProfileResponseDto(
                    user.Id,
                    user.Name,
                    user.UserName,
                    user.Email,
                    user.Phone,
                    user.Role,
                    user.Status,
                    user.PhotoUrl)
            };

            return Results.Ok(response);
        })
        .WithName("UpdateMyProfile")
        .Produces<ApiResponse<MyProfileResponseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithTags("UserProfile")
        .RequireAuthorization();

        app.MapPost("/me/password", async (
            ClaimsPrincipal principal,
            ChangeMyPasswordRequestDto request,
            IValidator<ChangeMyPasswordRequestDto> validator,
            IEncryptService encryptService,
            RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                return Results.NotFound();
            }

            var currentPasswordValid = encryptService.VerifyPassword(
                request.CurrentPassword, $"{user.HashPassword}:{user.Password}");
            if (!currentPasswordValid)
            {
                return Results.BadRequest(new[] { "La contraseña actual es incorrecta." });
            }

            var keyPwh = encryptService.HashPassword(request.NewPassword);
            user.HashPassword = keyPwh.Split(":")[0];
            user.Password = keyPwh.Split(":")[1];
            user.RefreshToken = "";
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Contraseña actualizada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { }
            });
        })
        .WithName("ChangeMyPassword")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithTags("UserProfile")
        .RequireAuthorization();

        app.MapPost("/me/photo", async (
            ClaimsPrincipal principal,
            UpdateMyPhotoRequestDto request,
            IValidator<UpdateMyPhotoRequestDto> validator,
            RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                return Results.NotFound();
            }

            user.PhotoUrl = request.BlobUrl;
            await db.SaveChangesAsync();

            var response = new ApiResponse<MyPhotoResponseDto>
            {
                Success = true,
                Message = "Foto actualizada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new MyPhotoResponseDto(user.PhotoUrl)
            };

            return Results.Ok(response);
        })
        .WithName("UpdateMyPhoto")
        .Produces<ApiResponse<MyPhotoResponseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithTags("UserProfile")
        .RequireAuthorization();

        app.MapDelete("/me/photo", async (ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                return Results.NotFound();
            }

            user.PhotoUrl = "";
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Foto eliminada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { }
            });
        })
        .WithName("DeleteMyPhoto")
        .WithTags("UserProfile")
        .RequireAuthorization();

        app.MapPost("/me/documents", async (
            ClaimsPrincipal principal,
            CreateMyDocumentRequestDto request,
            IValidator<CreateMyDocumentRequestDto> validator,
            RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var userExists = await db.Users.AnyAsync(x => x.Id == userId);
            if (!userExists)
            {
                return Results.NotFound();
            }

            var document = new UserDocument
            {
                UserId = userId.Value,
                DocumentName = request.DocumentName,
                FileName = request.FileName,
                BlobUrl = request.BlobUrl,
                Notes = request.Notes,
                CreatedAt = DateTime.Now
            };

            await db.UserDocuments.AddAsync(document);
            await db.SaveChangesAsync();

            var response = new ApiResponse<MyDocumentResponseDto>
            {
                Success = true,
                Message = "Documento registrado correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = new MyDocumentResponseDto(
                    document.Id,
                    document.DocumentName,
                    document.FileName,
                    document.BlobUrl,
                    document.Notes,
                    document.CreatedAt)
            };

            return Results.Created($"/api/user/me/documents/{document.Id}", response);
        })
        .WithName("CreateMyDocument")
        .Produces<ApiResponse<MyDocumentResponseDto>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithTags("UserProfile")
        .RequireAuthorization();

        app.MapGet("/me/documents", async (ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var documents = await db.UserDocuments
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new MyDocumentResponseDto(
                    x.Id, x.DocumentName, x.FileName, x.BlobUrl, x.Notes, x.CreatedAt))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<MyDocumentResponseDto>>
            {
                Success = true,
                Message = "Documentos obtenidos correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = documents
            });
        })
        .WithName("GetMyDocuments")
        .Produces<ApiResponse<List<MyDocumentResponseDto>>>(StatusCodes.Status200OK)
        .WithTags("UserProfile")
        .RequireAuthorization();

        app.MapDelete("/me/documents/{id:long}", async (long id, ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var document = await db.UserDocuments.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (document is null)
            {
                return Results.NotFound();
            }

            db.UserDocuments.Remove(document);
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Documento eliminado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { }
            });
        })
        .WithName("DeleteMyDocument")
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("UserProfile")
        .RequireAuthorization();
    }
}
