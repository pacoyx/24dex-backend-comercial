
using Microsoft.EntityFrameworkCore;

public static class LoginUserEndpoint
{
    public static void MapLoginUser(this IEndpointRouteBuilder app)
    {

        app.MapPost("/login", async (
                LoginUserDto loginDto, 
                RecepcionDbContext context, 
                JwtService jwtService, 
                IAppLogger<string> logger, IEncryptService HashingService) =>
        {
            if (context.Users == null)
            {
                return Results.Problem("Users table is not available", statusCode: 500);
            }


            var user = await context.Users
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.UserName == loginDto.Username );
            if (user == null)
            {
                logger.LogInformacion("Usuario no encontrado: " + loginDto.Username);
                return Results.Unauthorized();
            }
            logger.LogInformacion("Usuario encontrado: " + loginDto.Username);
            logger.LogInformacion("Usuario password: " + loginDto.Password);
            logger.LogInformacion("Usuario salt: " + user.HashPassword);

            var passwordhHash = HashingService.VerifyPassword(loginDto.Password, $"{user.HashPassword}:{user.Password}");
            if (!passwordhHash)
            {
                logger.LogWarning("Usuario fallo login password: " + loginDto.Username);
                return Results.Unauthorized();
            }


            // var user = await context.Users
            //                         .AsNoTracking()
            //                         .FirstOrDefaultAsync(u => u.UserName == loginDto.Username && u.Password == loginDto.Password);            
            // if (user == null)
            // {
            //     logger.LogWarning("Usuario fallo login: " + loginDto.Username);
            //     return Results.Unauthorized();
            // }

            var branchSalesUser = await context.BrachSalesUsers
                                            .Include(bsu => bsu.BranchSales)
                                            .AsNoTracking()
                                            .Where(bsu => bsu.UserId == user.Id && bsu.Status == "A")
                                            .ToListAsync();

            if (branchSalesUser == null)
            {
                logger.LogWarning("Usuario no tiene asignado ninguna sucursal de ventas: " + loginDto.Username);
                return Results.Problem("User does not have any branch sales assigned", statusCode: 401);
            }


            var token = jwtService.GenerateToken(user.Id.ToString(), loginDto.Username);
            var refreshToken = jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            context.Users.Update(user);
            await context.SaveChangesAsync();


            var loginUserResponseDto = new LoginUserResponseDto(
                token,
                refreshToken,
                user.Name,
                user.UserName,
                user.Role,
                user.Status,
                user.Id,
                branchSalesUser.Select(bsu => new
                BranchSalesResponseDto(
                    bsu.Id,
                    bsu.BranchSalesId,
                    bsu.BranchSales!.Description,
                    bsu.Status
                    )).ToList()
            );

            var response = new ApiResponse<LoginUserResponseDto>()
            {
                Success = true,
                Message = "Request was successful",
                Data = loginUserResponseDto,
                StatusCode = 200
            };
            
            logger.LogInformacion("User logueado correctamente: " + loginDto.Username);
            return Results.Ok(response);
        }).AllowAnonymous();


        app.MapPost("/refresh-token", async (
            RefreshTokenRequestDto refreshTokenRequest, 
            RecepcionDbContext context, 
            JwtService jwtService,
            IAppLogger<string> logger) =>
        {
            var principal = jwtService.GetPrincipalFromExpiredToken(refreshTokenRequest.Token);            
            var username = principal.Identity?.Name;
            if (username == null)
            {
                logger.LogWarning("RefreshToken: Token no autorizado: " + refreshTokenRequest.Token);
                return Results.Unauthorized();
            }

            var user = await context.Users
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.UserName == username);
            

            if (user == null || user.RefreshToken != refreshTokenRequest.RefreshToken)
            {
                // Console.WriteLine("Debugueando ERROR ======");
                // Console.WriteLine("No autorizado ======");
                // Console.WriteLine("User: ===>" + Newtonsoft.Json.JsonConvert.SerializeObject(user));
                logger.LogWarning("RefreshToken: Usuario no encontrado por username: " + refreshTokenRequest.Token);
                return Results.Unauthorized();
            }

            var newJwtToken = jwtService.GenerateToken(user.Id.ToString(), user.UserName);
            var newRefreshToken = jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            context.Users.Update(user);
            await context.SaveChangesAsync();

            var response = new
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            };

            return Results.Ok(response);
        });

    }
}