
using Microsoft.EntityFrameworkCore;

public static class LoginUserEndpoint
{
    public static void MapLoginUser(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginUserDto loginDto, RecepcionDbContext context, JwtService jwtService) =>
        {
            if (context.Users == null)
            {
                return Results.Problem("Users table is not available", statusCode: 500);
            }

            var user = await context.Users
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.UserName == loginDto.Username && u.Password == loginDto.Password);

            Console.WriteLine("User: " + user);
            if (user == null)
            {
                return Results.Unauthorized();
            }

            var branchSalesUser = await context.BrachSalesUsers
                                            .Include(bsu => bsu.BranchSales)
                                            .AsNoTracking()
                                            .Where(bsu => bsu.UserId == user.Id && bsu.Status == "A")
                                            .ToListAsync();

            if (branchSalesUser == null)
            {

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

            return Results.Ok(response);
        }).AllowAnonymous();




    app.MapPost("/refresh-token", async (RefreshTokenRequestDto refreshTokenRequest, RecepcionDbContext context, JwtService jwtService) =>
    {

        Console.WriteLine("Refresh Token Request: =====> " + Newtonsoft.Json.JsonConvert.SerializeObject(refreshTokenRequest));
        

        var principal = jwtService.GetPrincipalFromExpiredToken(refreshTokenRequest.Token);
        Console.WriteLine("Principal: ===>" + principal);
        var username = principal.Identity?.Name;
        if (username == null)
        {
            return Results.Unauthorized();
        }
        

        var user = await context.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(u => u.UserName == username);


        Console.WriteLine("User: ===>" + user);

        if (user == null || user.RefreshToken != refreshTokenRequest.RefreshToken)
        {
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